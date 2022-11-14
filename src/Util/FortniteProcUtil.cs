using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FortniteOverlay.Util
{
    internal class FortniteProcUtil
    {
        public static bool      Open         => !Handle.Equals(IntPtr.Zero);
        public static bool      Focused       = false;
        public static IntPtr    Handle        = IntPtr.Zero;
        public static Rectangle WindowSize    = new Rectangle();

        public static void UpdateProcessStatus(object sender, DoWorkEventArgs e)
        {
            int openCheckDelay = 10_000;
            int focusCheckDelay = 250;

            while (true)
            {
                Handle = GetFortniteHandle();
                if (Open)
                {
                    for (int i = 0; i < openCheckDelay / focusCheckDelay; i++)
                    {
                        Focused = FortniteFocused();
                        Rect procRect = new Rect();
                        GetWindowRect(Handle, ref procRect);
                        WindowSize = new Rectangle(procRect.Left, procRect.Top, procRect.Right - procRect.Left, procRect.Bottom - procRect.Top);
                        Thread.Sleep(focusCheckDelay);
                    }
                }
                else
                {
                    Focused = false;
                    Thread.Sleep(openCheckDelay);
                }
            }
        }

        private static bool FortniteFocused()
        {
            var handle = GetForegroundWindow();
            return (handle == Handle);
        }

        private static IntPtr GetFortniteHandle()
        {
            var allProcesses = Process.GetProcesses();
            try
            {
                var fn = allProcesses.FirstOrDefault(x => x.ProcessName == Program.fortniteProcess && x.MainWindowHandle != default);
                if (fn != null)
                {
                    return fn.MainWindowHandle;
                }
                else
                {
                    return IntPtr.Zero;
                }
            }
            catch (InvalidOperationException ex)
            {
                Program.form.Log("Error scanning active processes.\n" + ex.ToString());
            }

            return IntPtr.Zero;
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        private struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
    }
}
