using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FortniteSquadOverlayClient
{
    internal static class MiscUtil
    {
        public static Regex uploadEndpointRegex = new Regex(@"^(https?://)?(.+\..+|localhost)(:\d+)?(\/.*)*\.php$", RegexOptions.Compiled);
        public static Regex imageLocationRegex = new Regex(@"^(https?://)?(.+\..+|localhost)(:\d+)?(\/.*)*\/$", RegexOptions.Compiled);

        private static string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string configFolder = Path.Combine(localAppData, "FortniteOverlay");
        private static string fullConfigPath = Path.Combine(configFolder, "config.json");

        public static async Task CheckForUpdates(HttpClient httpClient = null)
        {
            if (httpClient == null) { httpClient = new HttpClient(); }

            string host = "https://api.github.com";
            string path = "/repos/slinkstr/FortniteSquadOverlay/releases";
            try
            {
                var response = await httpClient.GetAsync(host + path);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                var latest = JArray.Parse(content)[0];
                Version latestVersion = Version.Parse(latest["tag_name"].ToString().Substring(1));
                Version currentVersion = Version.Parse(CurrentVersion());

                if (latestVersion.CompareTo(currentVersion) > 0)
                {
                    Program.form.SetUpdateNotice($"New update available (v{latestVersion})", latest["html_url"].ToString());
                }
                else
                {
                    Program.form.SetUpdateNotice("");
                }
            }
            catch (Exception exc)
            {
                Program.form.Log("Unable to check for updates. Error:\n" + exc.ToString());
                Program.form.SetUpdateNotice("Unable to check for updates.");
            }
        }

        public static string CurrentVersion()
        {
            Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return $"{ver.Major}.{ver.Minor}.{ver.Build}";
        }

        public static async Task<List<string>> GetOrder(HttpClient httpClient = null)
        {
            if (httpClient == null) { httpClient = new HttpClient(); }

            List<string> order = new List<string>();

            string url = "https://raw.githubusercontent.com/slinkstr/FortniteSquadOverlay/master/order-id.json";
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                var jarr = JArray.Parse(content);
                order = jarr.ToObject<List<string>>();
            }
            catch (Exception exc)
            {
                Program.form.Log("Unable to get squad order. Error:\n" + exc.ToString());
            }

            return order;
        }

        public static int SortFortniters(FortnitePlayer first, FortnitePlayer second)
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
            ProgramConfig cfg = JsonConvert.DeserializeObject<ProgramConfig>(configText, new JsonSerializerSettings
            {
                Error = (sender, args) =>
                {
                    MessageBox.Show(args.ErrorContext.Error.GetBaseException().Message, "FortniteSquadOverlay", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    args.ErrorContext.Handled = true;
                }
            });

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
