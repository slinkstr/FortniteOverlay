using FortniteOverlay.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FortniteOverlay.Util.ImageUtil;
using static FortniteOverlay.Util.MiscUtil;

namespace FortniteOverlay
{
    internal static class Program
    {
        // debugging
        public static bool enableInOtherWindows = false;

        public static DateTime lastDown;
        public static DateTime lastUp;
        public static Fortniter localPlayer;
        public static Form1 form;
        public static HttpClient httpClient = new HttpClient();
        public static List<Fortniter> fortniters = new List<Fortniter>();
        public static List<PixelPositions> pixelPositions = KnownPositions();
        public static LogReader logReader = new LogReader(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FortniteGame\\Saved\\Logs", "FortniteGame.log");
        public static OverlayForm overlayForm;
        public static ProcMon procMon = new ProcMon("FortniteClient-Win64-Shipping");
        public static ProgramConfig config;
        public static System.Windows.Forms.Timer getNewestVersionTimer = new System.Windows.Forms.Timer();
        public static System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer();
        public static string[] order = new string[0];

        private static Bitmap screenBitmap;
        private static int debugOverlayLastWidth;
        private static int debugOverlayLastHeight;
        private static int debugOverlayLastScale;

        [STAThread]
        static void Main()
        {
            // Initialize
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            form = new Form1();
            overlayForm = new OverlayForm();

            httpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("FortniteOverlay", Application.ProductVersion));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("(+https://github.com/slinkstr/FortniteOverlay)"));

            if (!ConfigFileExists())
            {
                ConfigSave(new ProgramConfig());
                new ConfigForm().ShowDialog();
            }
            
            config = ConfigLoad();

            // Hardcoded order
            _ = GetOrder();

            // Rendering and upload/download events
            updateTimer.Tick += new EventHandler(UpdateEvent);
            updateTimer.Interval = 250;
            updateTimer.Start();

            // Log reader
            BackgroundWorker logReaderWorker = new BackgroundWorker();
            logReaderWorker.DoWork += new DoWorkEventHandler(logReader.ReadLogFile);
            logReaderWorker.RunWorkerAsync();

            // Process Monitor
            BackgroundWorker procMonWorker = new BackgroundWorker();
            procMonWorker.DoWork += new DoWorkEventHandler(procMon.UpdateProcessStatus);
            procMonWorker.RunWorkerAsync();

            // Update checking
            _ = CheckForUpdates();
            getNewestVersionTimer.Tick += new EventHandler(GetNewestVersionEvent);
            getNewestVersionTimer.Interval = (12 * 60 * 60 * 1000);
            getNewestVersionTimer.Start();

            Application.Run(form);
        }

        public static async void GetNewestVersionEvent(Object obj, EventArgs evtargs)
        {
            getNewestVersionTimer.Stop();
            // short delay avoids DNS errors when PC wakes up
            await Task.Delay(10000);
            await CheckForUpdates();
            getNewestVersionTimer.Start();
        }

        public static async void UpdateEvent(Object obj, EventArgs evtargs)
        {
            updateTimer.Stop();
            updateTimer.Interval = 500 * (procMon.ValidHandle ? 1 : 20);

            var tasks = new List<Task>();
            if (lastUp.AddSeconds(config.UploadInterval) - DateTime.Now <= TimeSpan.FromSeconds(0.2))
            {
                tasks.Add(UploadGear());
            }
            if (lastDown.AddSeconds(config.DownloadInterval) - DateTime.Now <= TimeSpan.FromSeconds(0.2))
            {
                tasks.Add(DownloadGear());
            }
            await Task.WhenAll(tasks);

            if (config.EnableOverlay && (procMon.Focused || enableInOtherWindows))
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
            }

            UpdateFormElements();

            updateTimer.Start();
        }

        public static async Task UploadGear()
        {
            if (!procMon.Focused && !enableInOtherWindows) { return; }
            if (fortniters.Count == 0)                     { return; }

            TakeScreenshot(ref screenBitmap, procMon.WindowSize);

            if (!IsGoldBarsVisible(screenBitmap, pixelPositions, config.HUDScale))
            {
                return;
            }
            if (IsSpectatingTextVisible(screenBitmap, pixelPositions, config.HUDScale))
            {
                return;
            }

            var gearBitmap = RenderGear(screenBitmap, pixelPositions, config.HUDScale);

            var stream = new MemoryStream();
            gearBitmap.Save(stream, ImageFormat.Jpeg);
            stream.Seek(0, SeekOrigin.Begin);

            var formData = new MultipartFormDataContent
            {
                { new StringContent(config.SecretKey), "secret" },
                { new StringContent(localPlayer.Name), "filename" },
                { new ByteArrayContent(stream.ToArray()), "gear", "image.jpg" }
            };

            HttpResponseMessage response = null;
            string responseString = "";
            try
            {
                response = await httpClient.PostAsync(config.UploadEndpoint, formData);
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

            form.SetSelfGear(new Bitmap(stream));
            lastUp = DateTime.Now;
        }

        public static async Task DownloadGear()
        {
            if (!procMon.ValidHandle && !enableInOtherWindows)  { return; }
            if (fortniters.Count == 0)                          { return; }
            lastDown = DateTime.Now;

            // get list of all users
            HttpResponseMessage response = null;
            string responseString = "";
            JArray availImages = null;
            try
            {
                response = await httpClient.GetAsync(config.ImageLocation);
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

            // get specific individuals
            foreach (var fort in fortniters.ToList())
            {
                var match = availImages.FirstOrDefault(x => x["name"].ToString() == fort.Name + ".jpg");
                if (match == null) { continue; }

                DateTime lastMod = DateTime.Parse(match["mtime"].ToString().Substring(5)).ToUniversalTime();
                if (lastMod != fort.GearModified)
                {
                    string gearUrl = config.ImageLocation.TrimEnd('/') + "/" + match["name"];
                    try
                    {
                        response = await httpClient.GetAsync(gearUrl);
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
                    var ftn = fortniters.FirstOrDefault(x => x.Name == fort.Name);
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
            Rectangle bounds = procMon.WindowSize;
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                bounds = Screen.GetBounds(Point.Empty);
            }
            overlayForm.Location = new Point(bounds.Left, bounds.Top);
            overlayForm.Size = new Size(bounds.Width, bounds.Height);
            overlayForm.Show();
        }

        private static void ShowDebugOverlay()
        {
            Rectangle bounds = procMon.WindowSize;
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                bounds = Screen.GetBounds(Point.Empty);
            }

            if (overlayForm.GetDebugOverlay() == null ||
                debugOverlayLastWidth != bounds.Width ||
                debugOverlayLastHeight != bounds.Height ||
                debugOverlayLastScale != config.HUDScale)
            {
                debugOverlayLastWidth = bounds.Width;
                debugOverlayLastHeight = bounds.Height;
                debugOverlayLastScale = config.HUDScale;

                var debugBitmap = new Bitmap(bounds.Width, bounds.Height);
                RenderGearDebug(ref debugBitmap, pixelPositions, config.HUDScale);
                overlayForm.SetDebugOverlay(debugBitmap);
            }
        }

        public static void UpdateFormElements()
        {
            // Gray out stale pics
            for (int i = 0; i < 3; i++)
            {
                if (fortniters.Count > i)
                {
                    if (fortniters[i].GearModified.AddSeconds(20) > DateTime.UtcNow) { continue; }
                    if (fortniters[i].GearImage == null) { continue; }
                    if (!fortniters[i].IsFaded)
                    {
                        fortniters[i].GearImage = MarkStaleImage(fortniters[i].GearImage);
                        fortniters[i].IsFaded = true;
                    }
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (fortniters.Count > i)
                {
                    if (fortniters[i].IsFaded)
                    {
                        overlayForm.SetSquadGear(i, null);
                    }
                    else
                    {
                        overlayForm.SetSquadGear(i, fortniters[i].GearImage);
                    }
                    form.SetSquadGear(i, fortniters[i].GearImage);
                    form.SetSquadName(i, fortniters[i].Name);
                }
                else
                {
                    overlayForm.SetSquadGear(i, null);
                    form.SetSquadGear(i, null);
                    form.SetSquadName(i, "");
                }
            }

            form.ShowHideSortButtons(fortniters.Count);
        }
    }

    public class Fortniter
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string UserIdTruncated => UserId.Substring(0, 5) + "..." + UserId.Substring(UserId.Length - 5, 5);
        public int Index { get; set; }
        public Bitmap GearImage { get; set; }
        public DateTime GearModified { get; set; }
        public bool IsFaded { get; set; } = false;
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
