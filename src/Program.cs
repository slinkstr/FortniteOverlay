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
using System.Runtime.InteropServices;
using System.Text;
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
        public static Form1 form;
        public static HttpClient httpClient = new HttpClient();
        public static List<Fortniter> fortniters = new List<Fortniter>();
        public static List<PixelPositions> pixelPositions = KnownPositions();
        public static LogReader logReader = new LogReader(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FortniteGame\\Saved\\Logs", "FortniteGame.log");
        public static OverlayForm overlayForm;
        public static ProcMon procMon = new ProcMon("FortniteClient-Win64-Shipping");
        public static ProgramConfig config;
        public static string hostName;
        public static System.Windows.Forms.Timer checkForUpdatesTimer = new System.Windows.Forms.Timer();
        public static System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer();

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
                SaveConfig(new ProgramConfig());
                new ConfigForm().ShowDialog();
            }

            try
            {
                config = GetConfig();
                VerifyConfig(config);
            }
            catch (Exception exc)
            {
                new Thread(() => MessageBox.Show(exc.Message, "FortniteOverlay", MessageBoxButtons.OK, MessageBoxIcon.Error)).Start();
            }

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
#if !DEBUG
            _ = CheckForUpdates();
#endif
            checkForUpdatesTimer.Tick += new EventHandler(GetNewestVersionEvent);
            checkForUpdatesTimer.Interval = (6 * 60 * 60 * 1000);
            checkForUpdatesTimer.Start();

            Application.Run(form);
        }

        public static async void GetNewestVersionEvent(Object obj, EventArgs evtargs)
        {
            checkForUpdatesTimer.Stop();
            await CheckForUpdates();
            checkForUpdatesTimer.Start();
        }

        public static async void UpdateEvent(Object obj, EventArgs evtargs)
        {
            updateTimer.Stop();
            var opts = form.ProgramOptions();
            updateTimer.Interval = 500 * (procMon.ValidHandle ? 1 : 10);

            var tasks = new List<Task>();
            if (lastUp.AddSeconds(opts.UploadFrequency) - DateTime.Now <= TimeSpan.FromSeconds(0.2))
            {
                tasks.Add(UploadGear());
            }
            if (lastDown.AddSeconds(opts.DownloadFrequency) - DateTime.Now <= TimeSpan.FromSeconds(0.2))
            {
                tasks.Add(DownloadGear());
            }
            await Task.WhenAll(tasks);

            if (procMon.Focused || enableInOtherWindows)
            {
                ShowOverlay();
                if (opts.DebugOverlay)
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

            var screen = TakeScreenshot(procMon.WindowSize);
            if (!IsMapVisible(screen, pixelPositions, config.HUDScale))
            {
                return;
            }
            var gearBitmap = RenderGear(screen, pixelPositions, config.HUDScale);

            var stream = new MemoryStream();
            gearBitmap.Save(stream, ImageFormat.Jpeg);
            stream.Seek(0, SeekOrigin.Begin);

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(config.SecretKey), "secret");
            formData.Add(new StringContent(hostName), "filename");
            formData.Add(new ByteArrayContent(stream.ToArray()), "gear", "image.jpg");

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
                    string gearUrl = config.ImageLocation + "/" + match["name"];
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
            var screen = TakeScreenshot(procMon.WindowSize);
            var debugBitmap = RenderGearDebug(screen, pixelPositions, config.HUDScale);
            overlayForm.SetDebugOverlay(debugBitmap);
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
        public Fortniter(string name = null)
        {
            Name = name;
        }

        public string Name { get; set; }
        public int PartyIndex { get; set; }
        public Bitmap GearImage { get; set; }
        public DateTime GearModified { get; set; }
        public bool IsFaded { get; set; } = false;
    }

    public class ProgramConfig
    {
        public string UploadEndpoint { get; set; } = "https://example.com/fortnitegear/upload.php";
        public string SecretKey { get; set; } = "SECRET_KEY_HERE";
        public string ImageLocation { get; set; } = "http://example.com/fortnitegear/images/";
        public int HUDScale { get; set; } = 100;
        public bool MinimizeToTray { get; set; } = true;
    }
}
