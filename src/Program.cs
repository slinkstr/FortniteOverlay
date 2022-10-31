using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Threading;
using System.ComponentModel;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace FortniteOverlay
{
    internal static class Program
    {
        public static System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer();
        public static Form1 form;
        public static DateTime lastUp;
        public static DateTime lastDown;
        public static Dictionary<string, string> logRegex = new Dictionary<string, string>();
        public static string hostName;
        public static string hostId;
        public static List<Fortniter> fortniters = new List<Fortniter>();
        public static ProgramConfig config;
        public static HttpClient httpClient = new HttpClient();

        public static List<PixelPositions> pixelPositions = RegisterPixelPositions();

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

            if (!pixelPositions.Any(x => x.Resolution[0] == Screen.GetBounds(Point.Empty).Width &&
                                         x.Resolution[1] == Screen.GetBounds(Point.Empty).Height))
            {
                MessageBox.Show("Screen resolution not supported - program may not function as expected.", "FortniteOverlay", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pixelPositions.Add(InterpolateResolution(Screen.GetBounds(Point.Empty).Width, Screen.GetBounds(Point.Empty).Height));
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            form = new Form1();
            updateTimer.Tick += new EventHandler(UpdateEvent);
            updateTimer.Interval = 1000;
            updateTimer.Start();

            BackgroundWorker logReader = new BackgroundWorker();
            logReader.DoWork += new DoWorkEventHandler(ReadLogFile);
            logReader.RunWorkerAsync();

            Application.Run(form);
        }

        public static async void UpdateEvent(Object obj, EventArgs evtargs)
        {
            updateTimer.Stop();

            var tasks = new List<Task>();
            if (lastUp == null || lastUp.AddSeconds(form.GetUpFreq()) - DateTime.Now <= TimeSpan.FromSeconds(0.5))
            {
                tasks.Add(UploadGear());
            }

            if (lastDown == null || lastDown.AddSeconds(form.GetDownFreq()) - DateTime.Now <= TimeSpan.FromSeconds(0.5))
            {
                tasks.Add(DownloadGear());
            }
            await Task.WhenAll(tasks);

            updateTimer.Start();
        }

        public static async Task UploadGear()
        {
            if(!FortniteFocused()) { return; }
            form.Log($"Uploading gear...");

            var screen = TakeScreenshot();
            if (!InGame(screen)) { return; }
            var gearBitmap = RenderGear(screen, 0, 0);

            var base64Name = Convert.ToBase64String(Encoding.UTF8.GetBytes(hostName));
            var stream = new MemoryStream();
            gearBitmap.Save(stream, ImageFormat.Bmp);
            stream.Seek(0, SeekOrigin.Begin);
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(config.SecretKey), "secret");
            formData.Add(new StringContent(base64Name), "filename");
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
            if (!FortniteFocused()) { return; }
            //form.Log($"DownloadGear");

            var response = await httpClient.GetAsync(config.ImageLocation);
            string jsonString = await response.Content.ReadAsStringAsync();
            var data = (JArray)JsonConvert.DeserializeObject(jsonString);

            foreach (var fort in fortniters)
            {
                var match = data.FirstOrDefault(x => x["name"].ToString() == fort.NameBase64 + ".png");
                if (match == null) { continue; }

                DateTime lastMod = DateTime.Parse(match["mtime"].ToString().Substring(5));
                if (lastMod != fort.GearModified)
                {
                    form.Log("Downloading gear for " + fort.Name);
                    string gearUrl = config.ImageLocation + "/" + fort.NameBase64 + ".png";
                    response = await httpClient.GetAsync(gearUrl);
                    var stream = await response.Content.ReadAsStreamAsync();
                    fort.GearImage = new Bitmap(stream);
                    fort.GearModified = lastMod;
                }
            }

            fortniters = fortniters.OrderBy(x => x.Name).ToList();
            for (int i = 0; i < 3; i++)
            {
                if (fortniters.Count - 1 >= i)
                {
                    form.SetSquadGear(i, fortniters[i].GearImage);
                    form.SetSquadName(i, fortniters[i].Name);
                }
                else
                {
                    form.SetSquadGear(i, null);
                    form.SetSquadName(i, "");
                }
            }

            lastDown = DateTime.Now;
        }

        public static Bitmap TakeScreenshot()
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
            }
            return bitmap;
        }

        public static bool InGame(Bitmap screenshot)
        {
            Color pureWhite = Color.FromArgb(255, 255, 255);
            var pos = pixelPositions.FirstOrDefault(x => x.Resolution[0] == screenshot.Width && x.Resolution[1] == screenshot.Height);
            if (pos == null) { throw new Exception("No dictionary found for a resolution of " + screenshot.Width + "x" + screenshot.Height + "."); }

            if (screenshot.GetPixel(pos.Map[0][0], pos.Map[0][1]) != pureWhite) { return false; }
            if (screenshot.GetPixel(pos.Map[1][0], pos.Map[1][1]) != pureWhite) { return false; }
            if (screenshot.GetPixel(pos.Map[2][0], pos.Map[2][1]) != pureWhite) { return false; }
            return true;
        }

        public static Bitmap RenderGear(Bitmap screenshot, int screenWidth, int screenHeight)
        {
            //if (!IsAlive(screenshot))
            //{
            //    Bitmap skullBitmap = null;
            //    using (var image = new Bitmap("imdead.png"))
            //    {
            //        skullBitmap = new Bitmap(image);
            //    }
            //
            //    return skullBitmap;
            //}

            Color pureWhite = Color.FromArgb(255, 255, 255);
            Color fadedWhite = Color.FromArgb(127, 127, 127);

            var pos = pixelPositions.FirstOrDefault(x => x.Resolution[0] == screenshot.Width && x.Resolution[1] == screenshot.Height);
            if (pos == null) { throw new Exception("No dictionary found for a resolution of " + screenshot.Width + "x" + screenshot.Height + "."); }

            int slotSelected = 0;
            for (int i = 0; i < pos.Slots.Length; i++)
            {
                var pix = screenshot.GetPixel(pos.Slots[i][0], pos.Slots[i][1]);
                if (pix == pureWhite || pix == fadedWhite)
                {
                    slotSelected = i + 1;
                }
            }

            Bitmap bitmap = new Bitmap(520 + 169, 104);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < pos.Slots.Length; i++)
                {
                    var ofs = (slotSelected == i + 1) ? pos.SelectedSlotOffset : 0;
                    g.DrawImage(screenshot, new Rectangle(i * 104, 0, 104, 104), new Rectangle(pos.Slots[i][0], pos.Slots[i][1] + ofs, 104, 104), GraphicsUnit.Pixel);
                }

                // keys
                g.DrawImage(screenshot, new Rectangle(520, 0, 169, 104), new Rectangle(2365, 945, 169, 104), GraphicsUnit.Pixel);
            }

            return bitmap;
        }

        public static bool FortniteFocused()
        {
            if(GetActiveWindow().ProcessName == "FortniteClient-Win64-Shipping")
            {
                return true;
            }
            return false;
        }

        public static void ReadLogFile(object sender, DoWorkEventArgs e)
        {
            string logDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FortniteGame\\Saved\\Logs";
            string logFile = "FortniteGame.log";
            
            var fs = new FileStream(logDir + "\\" + logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var sr = new StreamReader(fs))
            {
                var s = "";
                while (true)
                {
                    s = sr.ReadLine();
                    if (s != null)
                    {
                        ProcessLine(s);
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        public static async Task ProcessLine(string line)
        {
            var match = Regex.Match(line, FortniteLogRegex.LoggedIn);
            if (match.Success)
            {
                hostId = match.Groups[1].ToString();
                form.SetHostId(hostId);
                hostName = match.Groups[2].ToString();
                form.SetHostName(hostName);
                form.Log("[LoggedIn] User ID: " + match.Groups[1] + ", Display name: " + match.Groups[2]);
                return;
            }

            match = Regex.Match(line, FortniteLogRegex.PartyMemberJoined);
            if (match.Success)
            {
                if (fortniters.Any(x => x.Name == match.Groups[1].ToString())) { return; }
                if (match.Groups[1].ToString() == hostName) { return; }

                Fortniter newJoin = new Fortniter();
                newJoin.Name = match.Groups[1].ToString();
                //newJoin.Id = match.Groups[2].ToString();
                fortniters.Add(newJoin);

                form.SetSquadGear(string.Join(", ", fortniters.Select(x => x.Name)));

                form.Log("[PartyMemberJoined] Name: " + match.Groups[1] + ", ID: " + match.Groups[2]);
                return;
            }

            match = Regex.Match(line, FortniteLogRegex.PartyMemberLeft);
            if (match.Success)
            {
                var leaver = fortniters.Find(x => x.Name == match.Groups[1].ToString());
                if (leaver != null) { fortniters.Remove(leaver); };

                form.SetSquadGear(string.Join(", ", fortniters.Select(x => x.Name)));

                form.Log("[PartyMemberLeft] Name: " + match.Groups[1] + ", ID: " + match.Groups[2] + ", Host: " + match.Groups[3]);
                return;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetForegroundWindow();

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private static Process GetActiveWindow()
        {
            var handle = GetForegroundWindow();
            GetWindowThreadProcessId(handle, out uint pID);
            return Process.GetProcessById((Int32)pID);
        }

        private static PixelPositions InterpolateResolution(int width, int height)
        {
            PixelPositions reference = pixelPositions[0];
            return new PixelPositions
            {
                Resolution = new int[2] { width, height },
                SelectedSlotOffset = (int)((double)reference.SelectedSlotOffset / (double)reference.Resolution[1] * height),
                Slots = new int[5][]
                {
                    new int[] { (int)((double)reference.Slots[0][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[0][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.Slots[1][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[1][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.Slots[2][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[2][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.Slots[3][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[3][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.Slots[4][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[4][1] / (double)reference.Resolution[1] * height) },
                },
                Map = new int[3][]
                {
                    new int[] { (int)((double)reference.Map[0][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Map[0][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.Map[1][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Map[1][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.Map[2][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Map[2][1] / (double)reference.Resolution[1] * height) },
                },
            };
        }

        private static List<PixelPositions> RegisterPixelPositions()
        {
            List<PixelPositions> positions = new List<PixelPositions>();
            positions.Add(new PixelPositions
            {
                Resolution = new int[2] { 2560, 1440 },
                SelectedSlotOffset = -13,
                Slots = new int[5][]
                {
                    new int[] { 2009, 1227 },
                    new int[] { 2118, 1227 },
                    new int[] { 2227, 1227 },
                    new int[] { 2336, 1227 },
                    new int[] { 2444, 1227 }
                },
                Map = new int[3][]
                {
                    new int[] { 2543, 18 },
                    new int[] { 2538, 31 },
                    new int[] { 2511, 45 }
                },
            });
            
            positions.Add(new PixelPositions
            {
                Resolution = new int[2] { 1920, 1080 },
                SelectedSlotOffset = -11,
                Slots = new int[5][]
                {
                    new int[] { 1507, 920 },
                    new int[] { 1589, 920 },
                    new int[] { 1670, 920 },
                    new int[] { 1752, 920 },
                    new int[] { 1833, 920 }
                },
                Map = new int[3][]
                {
                    new int[] { 1907, 13 },
                    new int[] { 1904, 23 },
                    new int[] { 1883, 33 }
                },
            });

            return positions;
        }
    }

    public class Fortniter
    {
        public string Name { get; set; }
        public string NameBase64 => Convert.ToBase64String(Encoding.UTF8.GetBytes(Name));
        public Bitmap GearImage { get; set; }
        public DateTime GearModified { get; set; }
    }

    public class ProgramConfig
    {
        public string UploadEndpoint { get; set; }
        public string SecretKey { get; set; }
        public string ImageLocation { get; set; }
    }

    public class PixelPositions
    {
        public int[] Resolution { get; set; }
        public int SelectedSlotOffset { get; set; }
        public int[][] Slots { get; set; }
        public int[][] Map { get; set; }
    }

    public static class FortniteLogRegex
    {
        private static string Timestamp = @"\[\d{4}\.\d{2}\.\d{2}\-\d{2}\.\d{2}\.\d{2}:\d{3}]";
        private static string UnknownId = @"\[[\d ]{3}\]";
        public static string LoggedIn = "^" + Timestamp + UnknownId + @"LogOnlineAccount: Display: \[UOnlineAccountCommon::ProcessUserLogin\] Successfully logged in user\. UserId=\[([0-9a-fA-F]{32})\] DisplayName=\[(.{1,32})\] EpicAccountId=\[MCP:[0-9a-fA-F]{32}\] AuthTicket=\[<Redacted>\]$";
        public static string PartyMemberJoined = "^" + Timestamp + UnknownId + @"LogParty: Display: New party member state for \[(.{1,32})\] Id \[MCP:([0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5})\] added to the local player's party \[V2:[0-9a-fA-F]{32}\]\.$";
        public static string PartyMemberLeft = "^" + Timestamp + UnknownId + @"LogParty: Display: Party member state for \[(.{1,32})\] Id \[MCP:([0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5})\] removed from \[(.{1,32})\]'s party\.$";
    }
}
