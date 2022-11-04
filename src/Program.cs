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
using Microsoft.Win32.SafeHandles;
using System.Web;
using System.Web.UI;

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
        public static bool inGame = false;
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
                MessageBox.Show($"Screen resolution {Screen.GetBounds(Point.Empty).Size} not supported - program may not function as expected.", "FortniteOverlay", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            MarkStaleImages();

            await Task.WhenAll(tasks);

            updateTimer.Start();
        }

        public static async Task UploadGear()
        {
            if(!FortniteFocused()) { return; }
            var screen = TakeScreenshot();
            if (!InGame(screen)) { return; }
            form.Log($"Uploading gear...");
            var gearBitmap = RenderGear(screen);

            var urlFriendlyName = HttpUtility.UrlEncode(hostName);
            var stream = new MemoryStream();
            gearBitmap.Save(stream, ImageFormat.Bmp);
            stream.Seek(0, SeekOrigin.Begin);
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(config.SecretKey), "secret");
            formData.Add(new StringContent(urlFriendlyName), "filename");
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
            if (!FortniteOpen()) { return; }
            if (!inGame) { return; }
            if (fortniters.Count == 0) { return; }
            lastDown = DateTime.Now;
            form.Log($"Downloading gear...");

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

            foreach (var fort in fortniters)
            {
                var match = data.FirstOrDefault(x => x["name"].ToString() == fort.NameEncoded + ".png");
                if (match == null) { continue; }

                DateTime lastMod = DateTime.Parse(match["mtime"].ToString().Substring(5));
                if (lastMod != fort.GearModified)
                {
                    //form.Log("Downloading gear for " + fort.Name);
                    string gearUrl = config.ImageLocation + "/" + fort.NameEncoded + ".png";
                    response = await httpClient.GetAsync(gearUrl);
                    var stream = await response.Content.ReadAsStreamAsync();
                    var test = await response.Content.ReadAsStringAsync();
                    fort.GearImage = new Bitmap(stream);
                    fort.GearModified = lastMod;
                    fort.IsFaded = false;
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
        }

        public static void MarkStaleImages()
        {
            for (int i = 0; i < 3; i++)
            {
                if (i > fortniters.Count - 1) { return; }
                if (fortniters[i].GearImage == null) { continue; }
                if (fortniters[i].GearModified.AddSeconds(15) > DateTime.Now) { continue; }
                if (fortniters[i].IsFaded) { continue; }

                using (Graphics g = Graphics.FromImage(fortniters[i].GearImage))
                {
                    int width = fortniters[i].GearImage.Width;
                    int height = fortniters[i].GearImage.Height;
                    Image outdated = Image.FromFile("outdated.png");
                    Rectangle rect = new Rectangle(0, 0, width, height);
                    using (Brush darken = new SolidBrush(Color.FromArgb(128, Color.Black)))
                    {
                        g.FillRectangle(darken, rect);
                    }
                    g.DrawImage(outdated, new Rectangle(width / 2 - height / 2, height - (int)(height * 0.95), (int)(height*0.90), (int)(height * 0.90)));
                }
                form.SetSquadGear(i, fortniters[i].GearImage);
                fortniters[i].IsFaded = true;
            }
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

        public static Bitmap RenderGear(Bitmap screenshot)
        {
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

            Bitmap bitmap = new Bitmap((pos.SlotSize * 6), pos.SlotSize);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < pos.Slots.Length; i++)
                {
                    var ofs = (slotSelected == i + 1) ? pos.SelectedSlotOffset : 0;
                    g.DrawImage(screenshot, new Rectangle(i * pos.SlotSize, 0, pos.SlotSize, pos.SlotSize), new Rectangle(pos.Slots[i][0], pos.Slots[i][1] + ofs, pos.SlotSize, pos.SlotSize), GraphicsUnit.Pixel);
                }

                // keys
                Color yellowish = Color.FromArgb(244, 219, 93);
                var pix = screenshot.GetPixel(pos.CrownPos[0], pos.CrownPos[1]);
                if (yellowish.GetHue() - 10 < pix.GetHue() && yellowish.GetHue() + 10 > pix.GetHue())
                {
                    g.DrawImage(screenshot, new Rectangle(pos.SlotSize * 5, 0, pos.SlotSize, pos.SlotSize), new Rectangle(pos.KeyPosCrown[0], pos.KeyPosCrown[1], pos.SlotSize, pos.SlotSize), GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(screenshot, new Rectangle(pos.SlotSize * 5, 0, pos.SlotSize, pos.SlotSize), new Rectangle(pos.KeyPos[0], pos.KeyPos[1], pos.SlotSize, pos.SlotSize), GraphicsUnit.Pixel);
                }
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

        private static bool FortniteOpen()
        {
            var allProcesses = Process.GetProcesses();
            if (allProcesses.Any(x => x.ProcessName == "FortniteClient-Win64-Shipping"))
            {
                return true;
            }
            return false;
        }

        //public static bool IsBorderlessFullscreen()
        //{
        //    string configDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FortniteGame\\Saved\\Config\\WindowsClient";
        //    string configFile = "GameUserSettings.ini";
        //    if(!File.Exists(Path.Combine(configDir, configFile))) { return false; }
        //    string configText = File.ReadAllText(Path.Combine(configDir, configFile));
        //    int index = configText.IndexOf("PreferredFullscreenMode=");
        //    if (index == -1) { return false; }
        //    if(configText.Substring(index + 24, 1) == "1")
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public static void ReadLogFile(object sender, DoWorkEventArgs e)
        {
            string logDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FortniteGame\\Saved\\Logs";
            string logFile = "FortniteGame.log";
            
            var fs = new FileStream(logDir + "\\" + logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            long totalLenCached = 0;
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
                        if (fs.Length < totalLenCached)
                        {
                            form.Log("Fortnite restarted, resetting log file.");
                            fs.Seek(0, SeekOrigin.Begin);
                            sr.DiscardBufferedData();
                        }
                        totalLenCached = fs.Length;
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        public static void ProcessLine(string line)
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

            match = Regex.Match(line, FortniteLogRegex.StartedGame);
            if (match.Success)
            {
                form.Log("[StartedGame]");
                inGame = true;
                return;
            }
            match = Regex.Match(line, FortniteLogRegex.LeftGame);
            if (match.Success)
            {
                form.Log("[LeftGame]");
                inGame = false;
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

        public static string StringToHex(string input)
        {
            byte[] bytes = Encoding.Default.GetBytes(input);
            string hexString = BitConverter.ToString(bytes)
                .Replace("-", "")
                .ToLower();
            return hexString;
        }

        private static PixelPositions InterpolateResolution(int width, int height)
        {
            PixelPositions reference = pixelPositions[0];
            return new PixelPositions
            {
                Resolution = new int[2] { width, height },
                SelectedSlotOffset = (int)((double)reference.SelectedSlotOffset / (double)reference.Resolution[1] * height),
                SlotSize = (int)((double)reference.SlotSize / (double)reference.Resolution[0] * width),
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
                KeyPos = new int[2] {
                    (int)((double)reference.KeyPos[0] / (double)reference.Resolution[0] * width),
                    (int)((double)reference.KeyPos[1] / (double)reference.Resolution[1] * height),
                },
                KeyPosCrown = new int[2]
                {
                    (int)((double)reference.KeyPosCrown[0] / (double)reference.Resolution[0] * width),
                    (int)((double)reference.KeyPosCrown[1] / (double)reference.Resolution[1] * height),
                },
                CrownPos = new int[2]
                {
                    (int)((double)reference.CrownPos[0] / (double)reference.Resolution[0] * width),
                    (int)((double)reference.CrownPos[1] / (double)reference.Resolution[1] * height),
                }
            };
        }

        private static List<PixelPositions> RegisterPixelPositions()
        {
            List<PixelPositions> positions = new List<PixelPositions>();
            positions.Add(new PixelPositions
            {
                Resolution = new int[2] { 2560, 1440 },
                SelectedSlotOffset = -13,
                SlotSize = 104,
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
                KeyPos = new int[2] { 2456, 927  },
                KeyPosCrown = new int[2] { 2350, 944 },
                CrownPos = new int[2] { 2490, 1000 },
            });
            
            positions.Add(new PixelPositions
            {
                Resolution = new int[2] { 1920, 1080 },
                SelectedSlotOffset = -11,
                SlotSize = 78,
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
                KeyPos = new int[2] { 1842, 694 },
                KeyPosCrown = new int[2] { 1762, 707 },
                CrownPos = new int[2] { 1866, 750 },
            });

            return positions;
        }
    }

    public class Fortniter
    {
        public string Name { get; set; }
        public string NameEncoded => Program.StringToHex(Name);
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

    public class PixelPositions
    {
        public int[] Resolution { get; set; }
        public int SelectedSlotOffset { get; set; }
        public int SlotSize { get; set; }
        public int[][] Slots { get; set; }
        public int[][] Map { get; set; }
        public int[] KeyPos { get; set; }
        public int[] KeyPosCrown { get; set; }
        public int[] CrownPos { get; set; }
    }

    public static class FortniteLogRegex
    {
        private static string Timestamp = @"\[\d{4}\.\d{2}\.\d{2}\-\d{2}\.\d{2}\.\d{2}:\d{3}]";
        private static string UnknownId = @"\[[\d ]{3}\]";
        public static string LoggedIn = "^" + Timestamp + UnknownId + @"LogOnlineAccount: Display: \[UOnlineAccountCommon::ProcessUserLogin\] Successfully logged in user\. UserId=\[([0-9a-fA-F]{32})\] DisplayName=\[(.{1,32})\] EpicAccountId=\[MCP:[0-9a-fA-F]{32}\] AuthTicket=\[<Redacted>\]$";
        public static string PartyMemberJoined = "^" + Timestamp + UnknownId + @"LogParty: Display: New party member state for \[(.{1,32})\] Id \[MCP:([0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5})\] added to the local player's party \[V2:[0-9a-fA-F]{32}\]\.$";
        public static string PartyMemberLeft = "^" + Timestamp + UnknownId + @"LogParty: Display: Party member state for \[(.{1,32})\] Id \[MCP:([0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5})\] removed from \[(.{1,32})\]'s party\.$";
        public static string StartedGame = "^" + Timestamp + UnknownId + @"LogDemo: UReplaySubsystem::RecordReplay: Starting recording with demo driver\.  Name:  FriendlyName: Unsaved Replay$";
        //public static string StartedGameAlt = "^" + Timestamp + UnknownId + @"LogLocalFileReplay: Writing replay to '.*' with \d+\.\d{2}MB free$";
        public static string LeftGame = "^" + Timestamp + UnknownId + @"LogDemo: StopDemo: Demo  stopped at frame \d+$";
    }
}
