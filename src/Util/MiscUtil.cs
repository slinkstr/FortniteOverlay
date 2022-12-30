using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FortniteOverlay.Util
{
    internal class MiscUtil
    {
        public static Regex uploadEndpointRegex = new Regex(@"^(https?://)?(.+\..+|localhost)(:\d+)?(\/.*)*\.php$", RegexOptions.Compiled);
        public static Regex imageLocationRegex = new Regex(@"^(https?://)?(.+\..+|localhost)(:\d+)?(\/.*)*\/$", RegexOptions.Compiled);

        private static string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string configFolder = Path.Combine(localAppData, "FortniteOverlay");
        private static string fullConfigPath = Path.Combine(configFolder, "config.json");

        public static async Task CheckForUpdates()
        {
            string host = "https://api.github.com";
            string path = "/repos/slinkstr/FortniteOverlay/releases";
            try
            {
                var response = await Program.httpClient.GetAsync(host + path);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                var latest = JArray.Parse(content)[0];
                Version latestVersion = Version.Parse(latest["tag_name"].ToString().Substring(1));
                Version currentVersion = Version.Parse(Application.ProductVersion);

                if (latestVersion.CompareTo(currentVersion) > 0)
                {
                    Program.form.SetUpdateNotice($"New update available (v{latestVersion})", latest["html_url"].ToString());
                }
            }
            catch (Exception exc)
            {
                Program.form.Log("Unable to check for updates. Error:\n" + exc.ToString());
                Program.form.SetUpdateNotice("Unable to check for updates.");
            }
        }

        public static async Task GetOrder()
        {
            string url = "https://raw.githubusercontent.com/slinkstr/FortniteOverlay/master/order.json";
            try
            {
                var response = await Program.httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                var jarr = JArray.Parse(content);
                Program.order = jarr.ToObject<string[]>();
                foreach (var fortniter in Program.fortniters)
                {
                    fortniter.Index = Array.IndexOf(Program.order, fortniter.Name);
                }
                Program.fortniters.Sort(MiscUtil.SortFortniters);
            }
            catch (Exception exc)
            {
                Program.form.Log("Unable to get squad order. Error:\n" + exc.ToString());
            }
        }

        public static int SortFortniters(Fortniter first, Fortniter second)
        {
            if (first.Index == -1)
            {
                return 1;
            }
            else if (second.Index == -1)
            {
                return -1;
            }
            else
            {
                return first.Index.CompareTo(second.Index);
            }
        }

        public static int SettingsFullscreenMode()
        {
            string configDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FortniteGame\\Saved\\Config\\WindowsClient";
            string configFile = "GameUserSettings.ini";
            if (!File.Exists(Path.Combine(configDir, configFile))) { return -1; }
            string configText = File.ReadAllText(Path.Combine(configDir, configFile));
            int index = configText.IndexOf("PreferredFullscreenMode=");
            if (index == -1) { return -1; }
            if (!int.TryParse(configText.Substring(index + 24, 1), out var mode))
            {
                return -1;
            }
            return mode;
        }

        // Don't know if it's possible to check if replays are enabled, GameUserSettings.ini doesn't have any options that mention "replay" or "demo"
        // Same with HUD scale...

        public static bool ConfigFileExists()
        {
            if (!File.Exists(fullConfigPath))
            {
                return false;
            }

            return true;
        }

        public static ProgramConfig ConfigLoad()
        {
            var configText = string.Join("\n", File.ReadAllText(fullConfigPath));
            ProgramConfig cfg = JsonConvert.DeserializeObject<ProgramConfig>(configText);
            if (cfg == null)
            {
                throw new Exception($"Error deserializing {fullConfigPath}.");
            }

            return cfg;
        }

        public static void ConfigSave(ProgramConfig config)
        {
            Directory.CreateDirectory(configFolder);
            using (var stream = File.Create(fullConfigPath))
            {
                string cfgString = JsonConvert.SerializeObject(config, Formatting.Indented);
                var cfgBytes = Encoding.UTF8.GetBytes(cfgString);
                var cfgBytesLen = Encoding.UTF8.GetByteCount(cfgString);
                stream.Write(cfgBytes, 0, cfgBytesLen);
            }
        }

        public static void ConfigVerify(ProgramConfig cfg)
        {
            // verify all properties
            string invalidProperties = "";
            if (string.IsNullOrWhiteSpace(cfg.SecretKey))
            {
                invalidProperties += "SecretKey cannot be empty.\n";
            }
            if (cfg.HUDScale < 25 || cfg.HUDScale > 150)
            {
                invalidProperties += "Invalid HUD scale (must be between 25 and 150).\n";
            }
            if (string.IsNullOrWhiteSpace(cfg.ImageLocation))
            {
                invalidProperties += "ImageLocation cannot be empty.\n";
            }
            if (!imageLocationRegex.Match(cfg.ImageLocation).Success)
            {
                invalidProperties += "ImageLocation URL is invalid.\n";
            }
            if (string.IsNullOrWhiteSpace(cfg.UploadEndpoint))
            {
                invalidProperties += "UploadEndpoint cannot be empty.\n";
            }
            if (!uploadEndpointRegex.Match(cfg.UploadEndpoint).Success)
            {
                invalidProperties += "UploadEndpoint URL is invalid.\n";
            }

            if (!string.IsNullOrWhiteSpace(invalidProperties))
            {
                throw new Exception(invalidProperties);
            }
        }

        public static void ConfigOpenFileLocation()
        {
            System.Diagnostics.Process.Start(configFolder);
        }

        public static int MinMax(int min, int value, int max)
        {
            return Math.Min(Math.Max(min, value), max);
        }
    }
}
