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
                var label = Application.OpenForms["Form1"].Controls["updateNoticeLinkLabel"] as LinkLabel;
                label.Text = $"New update available (v{latestVersion})";
                label.LinkArea = new LinkArea(0, label.Text.Length);
                label.Tag = latest["html_url"].ToString();
                label.LinkVisited = false;
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

        public static bool FortniteFocused()
        {
            if (GetActiveWindow().ProcessName == Program.fortniteProcess)
            {
                return true;
            }
            return false;
        }

        public static bool FortniteOpen()
        {
            var allProcesses = Process.GetProcesses();
            if (allProcesses.Any(x => x.ProcessName == Program.fortniteProcess))
            {
                return true;
            }
            return false;
        }

        public static Rectangle GetWindowPosition(string processName)
        {
            return GetWindowPosition(GetWindowHandle(processName));
        }

        public static IntPtr GetWindowHandle(string processName)
        {
            var bruh = Process.GetProcessesByName(processName);
            var proc = Process.GetProcessesByName(processName).FirstOrDefault(x => x.MainWindowHandle != default(IntPtr));
            if (proc == null) { return IntPtr.Zero;           }
            else              { return proc.MainWindowHandle; }
        }

        public static Rectangle GetWindowPosition(IntPtr ptr)
        {
            Rect procRect = new Rect();
            GetWindowRect(ptr, ref procRect);
            return new Rectangle(procRect.Left, procRect.Top, procRect.Right - procRect.Left, procRect.Bottom - procRect.Top);
        }

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private static Process GetActiveWindow()
        {
            var handle = GetForegroundWindow();
            GetWindowThreadProcessId(handle, out uint pID);
            return Process.GetProcessById((Int32)pID);
        }
    }
}
