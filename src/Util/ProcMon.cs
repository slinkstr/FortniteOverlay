using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace FortniteOverlay.Util
{
    internal class ProcMon
    {
        public string    ProcessName { get; private set; }
        public IntPtr    Handle      { get; private set; }
        public bool      ValidHandle => !Handle.Equals(IntPtr.Zero);
        public bool      Focused     { get; private set; }
        public Rectangle WindowSize  { get; private set; }

        public ProcMon(string processName)
        {
            Focused = false;
            ProcessName = processName;
            Handle = IntPtr.Zero;
            WindowSize = new Rectangle();
        }

        public void UpdateProcessStatus(object sender, DoWorkEventArgs e)
        {
            int openCheckDelay = 10_000;
            int focusCheckDelay = 250;

            while (true)
            {
                Handle = GetHandle(ProcessName);
                if (ValidHandle)
                {
                    for (int i = 0; i < openCheckDelay / focusCheckDelay; i++)
                    {
                        Focused = IsFocused(Handle);
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

        private static bool IsFocused(IntPtr handle)
        {
            IntPtr focused = GetForegroundWindow();
            return (focused == handle);
        }

        private static IntPtr GetHandle(string processName)
        {
            var allProcesses = Process.GetProcesses();
            try
            {
                var proc = allProcesses.FirstOrDefault(x => x.ProcessName == processName && x.MainWindowHandle != default);
                if (proc != null)
                {
                    return proc.MainWindowHandle;
                }
                else
                {
                    return IntPtr.Zero;
                }
            }
            catch (InvalidOperationException exc)
            {
                Program.form.Log("Error scanning active processes.\n" + exc.ToString());
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
