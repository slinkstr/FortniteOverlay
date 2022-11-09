using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using static FortniteOverlay.Util.ImageUtil;
using static FortniteOverlay.Util.LogReadUtil;
using static FortniteOverlay.Util.MiscUtil;
using System.Web;
using System.Reflection;

namespace FortniteOverlay
{
    internal static class Program
    {
        public static bool inGame = false;
        public static DateTime lastDown;
        public static DateTime lastUp;
        public static Dictionary<string, string> logRegex = new Dictionary<string, string>();
        public static Form1 form;
        public static OverlayForm overlayForm;
        public static HttpClient httpClient = new HttpClient();
        public static List<Fortniter> fortniters = new List<Fortniter>();
        public static List<PixelPositions> pixelPositions = KnownPositions();
        public static ProgramConfig config;
        public static string fortniteProcess = "FortniteClient-Win64-Shipping";
        public static string hostId;
        public static string hostName;

        public static Timer updateTimer = new Timer();
        public static Timer checkForUpdatesTimer = new Timer();

        [STAThread]
        static void Main()
        {
            if(!File.Exists("config.json"))
            {
                MessageBox.Show("No config.json found.", "FortniteOverlay", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            var configText = string.Join("\n", File.ReadAllLines("config.json"));
            try { config = JsonConvert.DeserializeObject<ProgramConfig>(configText); }
            catch (Exception e)
            {
                MessageBox.Show("Error processing config.json:\n" + e.Message, "FortniteOverlay", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            var curWidth = Screen.GetBounds(Point.Empty).Width;
            var curHeight = Screen.GetBounds(Point.Empty).Height;
            if (!pixelPositions.Any(x => x.Resolution[0] == curWidth && x.Resolution[1] == curHeight))
            {
                MessageBox.Show($"Screen resolution {Screen.GetBounds(Point.Empty).Size} not supported - program may not function as expected.", "FortniteOverlay", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pixelPositions.Add(InterpolateResolution(pixelPositions.First(), curWidth, curHeight));
            }

            httpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("FortniteGearOverlay", Assembly.GetExecutingAssembly().GetName().Version.ToString()));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("(+https://github.com/slinkstr/FortniteOverlay)"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            form = new Form1();
            overlayForm = new OverlayForm();

            // Rendering and upload/download events
            updateTimer.Tick += new EventHandler(UpdateEvent);
            updateTimer.Interval = 250;
            updateTimer.Start();

            // Log reader
            BackgroundWorker logReader = new BackgroundWorker();
            logReader.DoWork += new DoWorkEventHandler(ReadLogFile);
            logReader.RunWorkerAsync();

            // Update checking
            _ = CheckForUpdates();
            checkForUpdatesTimer.Tick += new EventHandler(GetNewestVersionEvent);
            checkForUpdatesTimer.Interval = (6 * 60 * 60);
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

            var tasks = new List<Task>();
            if (lastUp == null || lastUp.AddSeconds(form.ProgramOptions().UploadFrequency) - DateTime.Now <= TimeSpan.FromSeconds(0.5))
            {
                tasks.Add(UploadGear());
            }

            if (lastDown == null || lastDown.AddSeconds(form.ProgramOptions().DownloadFrequency) - DateTime.Now <= TimeSpan.FromSeconds(0.5))
            {
                tasks.Add(DownloadGear());
            }

            await Task.WhenAll(tasks);

            // Update form elements
            fortniters = fortniters.OrderBy(x => x.Name).ToList();
            for (int i = 0; i < 3; i++)
            {
                if (fortniters.Count - 1 >= i)
                {
                    overlayForm.SetSquadGear(i, fortniters[i].GearImage);
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

            // Grey out stale pics
            for (int i = 0; i < 3; i++)
            {
                if (fortniters.Count - 1 >= i)
                {
                    if (fortniters[i].GearModified.AddSeconds(10) > DateTime.Now) { continue; }
                    if (fortniters[i].GearImage == null) { continue; }
                    if (fortniters[i].IsFaded) { continue; }

                    fortniters[i].GearImage = MarkStaleImage(fortniters[i].GearImage);
                    fortniters[i].IsFaded = true;
                    overlayForm.SetSquadGear(i, fortniters[i].GearImage);
                    form.SetSquadGear(i, fortniters[i].GearImage);
                }
            }

            // Show or hide overlay
            if (form.ProgramOptions().EnableOverlay)
            {
                if (FortniteFocused() && inGame)
                {
                    var rect = GetWindowPosition(Program.fortniteProcess);
                    overlayForm.Location = new Point(rect.Top, rect.Left);
                    overlayForm.Size = new Size(rect.Width, rect.Height);
                    overlayForm.Show();
                }
                else
                {
                    overlayForm.Hide();
                }
            }
            else
            {
                overlayForm.Hide();
            }

            // *******************************************************************************
            // Remove this later
            if (FortniteOpen())
            {
                var screen = TakeScreenshot();
                var debugBitmap = RenderGearDebug(screen, pixelPositions);
                if (form.ProgramOptions().DebugOverlay) { overlayForm.SetDebugOverlay(debugBitmap); }
                else                                    { overlayForm.SetDebugOverlay(null); }
            }
            // *******************************************************************************

            updateTimer.Start();
        }

        public static async Task UploadGear()
        {
            if (!FortniteFocused())    { return; }
            if (!inGame)               { return; }
            if (fortniters.Count == 0) { return; }

            var screen = TakeScreenshot();
            if (!IsMapVisible(screen, pixelPositions)) { return; }
            var gearBitmap = RenderGear(screen, pixelPositions);

            var stream = new MemoryStream();
            gearBitmap.Save(stream, ImageFormat.Bmp);
            stream.Seek(0, SeekOrigin.Begin);
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(config.SecretKey), "secret");
            formData.Add(new StringContent(hostName), "filename");
            formData.Add(new ByteArrayContent(stream.ToArray()), "gear", "image.png");
            HttpResponseMessage response = await httpClient.PostAsync(config.UploadEndpoint, formData);
            var responseString = response.Content.ReadAsStringAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                form.Log("Error uploading image to server. " + responseString);
            }
            else
            {
                form.SetSelfGear(gearBitmap);
            }

            lastUp = DateTime.Now;
        }

        public static async Task DownloadGear()
        {
            if (!FortniteOpen())       { return; }
            if (!inGame)               { return; }
            if (fortniters.Count == 0) { return; }
            lastDown = DateTime.Now;

            var response = await httpClient.GetAsync(config.ImageLocation);
            string jsonString = await response.Content.ReadAsStringAsync();
            JArray data = null;
            try
            {
                data = (JArray)JsonConvert.DeserializeObject(jsonString);
            }
            catch (Exception e)
            {
                form.Log("Error downloading data from server.\n" +
                         "-------------------------\n" +
                         e.ToString() + "\n" +
                         "-------------------------\n" +
                         "Server response:\n" + jsonString);
                return;
            }

            if(data == null)
            {
                form.Log("Error downloading data from server.\n" +
                         "Server response:\n" + jsonString);
                return;
            }

            foreach (var fort in fortniters.ToList())
            {
                var match = data.FirstOrDefault(x => x["name"].ToString() == fort.Name + ".png");
                if (match == null) { continue; }

                DateTime lastMod = DateTime.Parse(match["mtime"].ToString().Substring(5));
                if (lastMod != fort.GearModified)
                {
                    //form.Log("Downloading gear for " + fort.Name);
                    string gearUrl = config.ImageLocation + "/" + fort.Name + ".png";
                    response = await httpClient.GetAsync(gearUrl);
                    if (!response.IsSuccessStatusCode)
                    {
                        form.Log("Error downloading gear image for " + fort.Name);
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
    }

    public class Fortniter
    {
        public Fortniter(string name = null)
        {
            Name = name;
        }

        public string Name { get; set; }
        //public string NameEncoded => HttpUtility.UrlEncode(Name);
        public Bitmap GearImage { get; set; }
        public DateTime GearModified { get; set; }
        public bool IsFaded { get; set; } = false;
    }

    public class ProgramConfig
    {
        public string UploadEndpoint { get; set; }
        public string SecretKey { get; set; }
        public string ImageLocation { get; set; }
    }
}
