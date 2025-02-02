using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static FortniteSquadOverlayClient.MiscUtil;
using static FortniteSquadOverlayClient.ImageUtil;

namespace FortniteSquadOverlayClient
{
    internal static class Program
    {
        public static Form1 form;
        public static OverlayForm overlayForm;
        public static ProgramConfig config;

        public static FortnitePlayer LocalPlayer = null;
        public static List<FortnitePlayer> CurrentSquad = new List<FortnitePlayer>();
        public static List<string> UserIdOrder = new List<string>();

        private static readonly string    _logFile    = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FortniteGame\\Saved\\Logs", "FortniteGame.log");
        private static readonly LogReader _logReader  = new LogReader(_logFile, LogParser.ProcessLine, ResetProgramState);
        private static readonly ProcMon   _procMon    = new ProcMon("FortniteClient-Win64-Shipping");
        private static HttpClient         _httpClient = new HttpClient();

        private static Timer      _updateTimer        = new Timer();
        private static PixelPositions _pixelPositions = null;
        private static Bitmap     _screenBuffer       = null;
        private static DateTime   _lastDownload       = DateTime.MinValue;
        private static DateTime   _lastUpload         = DateTime.MinValue;

        private static Bitmap     _debugBuffer        = null;

        [STAThread]
        static void Main()
        {
            // Initialize
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            form = new Form1();
            overlayForm = new OverlayForm();

            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"FortniteSquadOverlay {CurrentVersion()} (+https://github.com/slinkstr/FortniteOverlay)");

            if (!ConfigFileExists())
            {
                config = new ProgramConfig();
                ConfigSave(config);
                new ConfigForm().ShowDialog();
            }

            config = ConfigLoad();

            // Squadmate order
            Task.Run(async () =>
            {
                UserIdOrder = await GetOrder(_httpClient);
            });

            // Background tasks
            _logReader.Start();
            _procMon.Start();

            // Update checking
            Task.Run(async () =>
            {
                await CheckForUpdates(_httpClient);
            });

            // Rendering and upload/download events
            _updateTimer.Tick += new EventHandler(UpdateEvent);
            _updateTimer.Interval = 500;
            _updateTimer.Start();

            Application.Run(form);
        }

        public static void ResetProgramState()
        {
            form.Log("Fortnite restarted, resetting log file.");
            LocalPlayer = null;
            CurrentSquad.Clear();
        }

        public static async void UpdateEvent(Object obj, EventArgs evtargs)
        {
            _updateTimer.Stop();
            _updateTimer.Interval = 500 * (_procMon.ValidHandle() ? 1 : 20);

            if(_procMon.ValidHandle())
            {
                _pixelPositions = PixelPositions.GetMatchingPositions(_procMon.WindowSize.Width, _procMon.WindowSize.Height, config.HUDScale);
            }

            var tasks = new List<Task>();
            if (_lastUpload.AddSeconds(config.UploadInterval) - DateTime.UtcNow <= TimeSpan.FromSeconds(0.2))
            {
                tasks.Add(UploadGear());
            }
            if (_lastDownload.AddSeconds(config.DownloadInterval) - DateTime.UtcNow <= TimeSpan.FromSeconds(0.2))
            {
                tasks.Add(DownloadGear());
            }
            await Task.WhenAll(tasks);

            if (config.EnableOverlay && _procMon.Focused)
            {
                ShowOverlay();
                overlayForm.SetOverlayOpacity(config.OverlayOpacity);
                if (form.CurrentProgramOptions().DebugOverlay)
                {
                    ShowDebugOverlay();
                }
                else
                {
                    overlayForm.SetDebugOverlay(null);
                }
            }
            else
            {
                overlayForm.Hide();
                _debugBuffer = null;
            }

            if (!_procMon.ValidHandle())
            {
                // will eventually get GC'd
                _screenBuffer = null;
            }

            UpdateFormElements();

            _updateTimer.Start();
        }

        public static async Task UploadGear()
        {
            if (LocalPlayer == null)     { return; }
            if (!_procMon.Focused)       { return; }
            if (CurrentSquad.Count < 1)  { return; }

            TakeScreenshot(ref _screenBuffer, _procMon.WindowSize);

            if (!ImageProcessing.IsPlaying   (_screenBuffer, _pixelPositions)) { return; }
            if ( ImageProcessing.IsSpectating(_screenBuffer, _pixelPositions)) { return; }
            if ( ImageProcessing.IsDriving   (_screenBuffer, _pixelPositions)) { return; }

            var gearBitmap  = ImageProcessing.CropGear(_screenBuffer, _pixelPositions);
            var gearResized = new Bitmap(gearBitmap, new System.Drawing.Size(312, 52));
            var stream      = new MemoryStream();
            gearBitmap.Save(stream, ImageFormat.Jpeg);
            stream.Seek(0, SeekOrigin.Begin);

            var formData = new MultipartFormDataContent
            {
                { new StringContent(config.SecretKey), "secret" },
                { new StringContent(LocalPlayer.Name), "filename" },
                { new ByteArrayContent(stream.ToArray()), "gear", "image.jpg" }
            };

            _lastUpload = DateTime.UtcNow;
            HttpResponseMessage response = null;
            string responseString = "";
            try
            {
                response = await _httpClient.PostAsync(config.UploadEndpoint, formData);
                responseString = response.Content.ReadAsStringAsync().Result;
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exc)
            {
                form.Log("Error uploading data to server.\n" +
                         "-------------------------\n" +
                         exc.ToString() + "\n" +
                         (!string.IsNullOrWhiteSpace(responseString) ? "-------------------------\nServer response:\n" + responseString : ""));
                return;
            }

            LocalPlayer.GearImage = new Bitmap(stream);
            LocalPlayer.GearModified = DateTime.UtcNow;
            LocalPlayer.IsFaded = false;
        }

        public static async Task DownloadGear()
        {
            if (!_procMon.ValidHandle()) { return; }
            if (CurrentSquad.Count < 1)  { return; }

            _lastDownload = DateTime.UtcNow;

            // get list of users
            HttpResponseMessage response = null;
            string responseString = "";
            JArray availImages = null;
            try
            {
                response = await _httpClient.GetAsync(config.ImageLocation);
                responseString = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                availImages = (JArray)JsonConvert.DeserializeObject(responseString);
                if(availImages == null) { throw new Exception("Couldn't process json (availImages was null)."); }
            }
            catch (Exception exc)
            {
                form.Log("Error downloading data from server.\n" +
                         "-------------------------\n" +
                         exc.ToString() + "\n" +
                         (!string.IsNullOrWhiteSpace(responseString) ? "-------------------------\nServer response:\n" + responseString : ""));
                return;
            }

            // get images for current squad
            foreach (var fort in CurrentSquad.ToList())
            {
                var match = availImages.FirstOrDefault(x => x["name"].ToString() == fort.Name + ".jpg");
                if (match == null) { continue; }

                DateTime lastMod = DateTime.Parse(match["mtime"].ToString().Substring(5)).ToUniversalTime();
                if (lastMod != fort.GearModified)
                {
                    string gearUrl = config.ImageLocation.TrimEnd('/') + "/" + match["name"];
                    try
                    {
                        response = await _httpClient.GetAsync(gearUrl);
                        response.EnsureSuccessStatusCode();
                    }
                    catch (Exception exc)
                    {
                        form.Log($"Error downloading gear image for {fort.Name}\n" +
                                  "-------------------------\n" +
                                  exc.ToString() + "\n" +
                                  "-------------------------\n");
                        continue;
                    }

                    var stream = await response.Content.ReadAsStreamAsync();

                    // squad could change while we're fetching gear, hence the ToList() and this
                    var ftn = CurrentSquad.FirstOrDefault(x => x.Name == fort.Name);
                    if(ftn != null)
                    {
                        ftn.GearImage = new Bitmap(stream);
                        ftn.GearModified = lastMod;
                        ftn.IsFaded = false;
                    }
                }
            }
        }

        private static void ShowOverlay()
        {
            Rectangle bounds = _procMon.WindowSize;
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                bounds = Screen.GetBounds(Point.Empty);
            }
            overlayForm.Location = new Point(bounds.Left, bounds.Top);
            overlayForm.Size = new System.Drawing.Size(bounds.Width, bounds.Height);
            overlayForm.Show();
        }

        private static void ShowDebugOverlay()
        {
            var bounds = _procMon.WindowSize;

            if (bounds.Width != _debugBuffer?.Width || bounds.Height != _debugBuffer?.Height)
            {
                _debugBuffer = new Bitmap(bounds.Width, bounds.Height);
            }

            ImageProcessing.RenderDebugMarkers(ref _debugBuffer, _pixelPositions);
            overlayForm.SetDebugOverlay(_debugBuffer);
        }

        public static void UpdateFormElements()
        {
            var gearFadeTargets = new List<FortnitePlayer>() { LocalPlayer }.Concat(CurrentSquad);
            foreach (var fortniter in gearFadeTargets)
            {
                if (fortniter == null) { continue; }

                if (fortniter.GearImage == null)                             { continue; }
                if (fortniter.IsFaded)                                       { continue; }
                if (fortniter.GearModified.AddSeconds(20) > DateTime.UtcNow) { continue; }

                fortniter.GearImage = ImageProcessing.MarkStaleImage(fortniter.GearImage);
                fortniter.IsFaded   = true;
            }

            var activePlayers = CurrentSquad.Where(x => x.State != FortnitePlayer.ReadyState.SittingOut);
            for (int i = 0; i < 3; i++)
            {
                if (CurrentSquad.Count > i)
                {
                    if (CurrentSquad[i].IsFaded)
                    {
                        overlayForm.SetSquadGear(i, null);
                    }
                    else
                    {
                        overlayForm.SetSquadGear(i, CurrentSquad[i].GearImage);
                    }
                    form.SetSquadGear(i, CurrentSquad[i].GearImage);
                    form.SetSquadName(i, CurrentSquad[i].Name);
                }
                else
                {
                    overlayForm.SetSquadGear(i, null);
                    form.SetSquadGear(i, null);
                    form.SetSquadName(i, "");
                }
            }

            form.SetSelfName(LocalPlayer?.Name);
            form.SetSelfGear(LocalPlayer?.GearImage);

            form.ShowHideSortButtons(CurrentSquad.Count);
        }
    }

    public class ProgramConfig
    {
        private string _secretKey = "SECRET_KEY_HERE";
        public string SecretKey
        {
            get => _secretKey;
            set
            {
                if(string.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Secret key cannot be blank."); }
                _secretKey = value;
            }
        }

        private string _uploadEndpoint = "https://example.com/fortnitegear/upload.php";
        public string UploadEndpoint
        {
            get => _uploadEndpoint;
            set
            {
                if(string.IsNullOrWhiteSpace(value))             { throw new ArgumentException("Upload endpoint cannot be blank."); }
                if(!MiscUtil.uploadEndpointRegex.IsMatch(value)) { throw new ArgumentException("Upload endpoint invalid."); }
                _uploadEndpoint = value;
            }
        }

        private string _imageLocation = "http://example.com/fortnitegear/images/";
        public string ImageLocation
        {
            get => _imageLocation;
            set
            {
                if (string.IsNullOrWhiteSpace(value))            { throw new ArgumentException("Image location cannot be blank."); }
                if (!MiscUtil.imageLocationRegex.IsMatch(value)) { throw new ArgumentException("Image location invalid."); }
                _imageLocation = value;
            }
        }

        private int _uploadInterval = 5;
        public int UploadInterval
        {
            get => _uploadInterval;
            set
            {
                if (value < 1) { throw new ArgumentException("Upload interval must be positive."); }
                _uploadInterval = value;
            }
        }

        private int _downloadInterval = 5;
        public int DownloadInterval
        {
            get => _downloadInterval;
            set
            {
                if (value < 1) { throw new ArgumentException("Download interval must be positive."); }
                _downloadInterval = value;
            }
        }

        private int _HUDScale = 100;
        public int HUDScale
        {
            get => _HUDScale;
            set
            {
                if (value < 25 || value > 150) { throw new ArgumentException("HUD scale must be between 25 and 150 inclusive."); }
                _HUDScale = value;
            }
        }

        private int _overlayOpacity = 85;
        public int OverlayOpacity
        {
            get => _overlayOpacity;
            set
            {
                if (value < 0 || value > 100) { throw new ArgumentException("Overlay opacity must be between 0 and 100 inclusive."); }
                _overlayOpacity = value;
            }
        }

        public bool ShowConsole { get; set; } = true;
        public bool EnableOverlay { get; set; } = true;
        public bool MinimizeToTray { get; set; } = true;
        public bool StartMinimized { get; set; } = false;
        public bool AlwaysOnTop { get; set; } = false;
        // run at startup is handled by the config form
    }
}
