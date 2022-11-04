using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FortniteOverlay.Util
{
    internal class MiscUtil
    {
        public static bool BorderlessFullscreen()
        {
            string configDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FortniteGame\\Saved\\Config\\WindowsClient";
            string configFile = "GameUserSettings.ini";
            if (!File.Exists(Path.Combine(configDir, configFile))) { return false; }
            string configText = File.ReadAllText(Path.Combine(configDir, configFile));
            int index = configText.IndexOf("PreferredFullscreenMode=");
            if (index == -1) { return false; }
            if (configText.Substring(index + 24, 1) == "1")
            {
                return true;
            }
            return false;
        }

        public static bool FortniteFocused()
        {
            if (GetActiveWindow().ProcessName == "FortniteClient-Win64-Shipping")
            {
                return true;
            }
            return false;
        }

        public static bool FortniteOpen()
        {
            var allProcesses = Process.GetProcesses();
            if (allProcesses.Any(x => x.ProcessName == "FortniteClient-Win64-Shipping"))
            {
                return true;
            }
            return false;
        }
        public static string StringToHex(string input)
        {
            byte[] bytes = Encoding.Default.GetBytes(input);
            string hexString = BitConverter.ToString(bytes)
                .Replace("-", "")
                .ToLower();
            return hexString;
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
