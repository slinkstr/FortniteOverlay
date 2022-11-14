using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace FortniteOverlay.Util
{
    internal class MiscUtil
    {
        public static async Task CheckForUpdates()
        {
            string host = "https://api.github.com";
            string path = "/repos/slinkstr/FortniteOverlay/releases";
            var response = await Program.httpClient.GetAsync(host + path);
            if (!response.IsSuccessStatusCode)
            {
                Program.form.Log("Unable to check for updates on GitHub. Error:\n" + response.Content.ReadAsStringAsync().Result);
                return;
            }

            string content = await response.Content.ReadAsStringAsync();
            var latest = JArray.Parse(content)[0];
            Version latestVersion = Version.Parse(latest["tag_name"].ToString().Substring(1));
            Version currentVersion = Version.Parse(Application.ProductVersion);

            if (latestVersion.CompareTo(currentVersion) > 0)
            {
                Program.form.SetUpdateNotice($"New update available (v{latestVersion})", latest["html_url"].ToString());
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
    }
}
