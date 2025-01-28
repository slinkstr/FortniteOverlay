using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace FortniteOverlay.Util
{
    internal class ProcMon
    {
        public bool      Active      { get; private set; } = false;
        public string    ProcessName { get; private set; }
        public IntPtr    Handle      { get; private set; }
        public bool      Focused     { get; private set; }
        public Rectangle WindowSize  { get; private set; }

        public int IntervalHandleCheck { get; set; } = 10_000;
        public int IntervalFocusCheck  { get; set; } = 250;

        private Action   _onChange;
        private Timer    _handleTimer;
        private Timer    _focusTimer;

        public ProcMon(string processName, Action onChange = null, bool autostart = false)
        {
            ProcessName = processName;
            _onChange   = onChange;

            _handleTimer = new Timer((e) => UpdateHandle(), null, 0, Timeout.Infinite);
            _focusTimer  = new Timer((e) => UpdateFocus() , null, 0, Timeout.Infinite);

            if (autostart)
            {
                Start();
            }
        }

        public void Start()
        {
            Active = true;
            _handleTimer.Change(0, IntervalHandleCheck);
        }

        public void Stop()
        {
            Active = false;
            _handleTimer.Change(0, Timeout.Infinite);
            _focusTimer.Change(0, Timeout.Infinite);
        }

        public bool ValidHandle()
        {
            return !Handle.Equals(IntPtr.Zero);
        }

        private void UpdateFocus()
        {
            if (!ValidHandle()) { return; }
            var newFocus = IsFocused(Handle);
            if (newFocus != Focused)
            {
                _onChange?.Invoke();
            }
            Focused = newFocus;
        }

        private void UpdateHandle()
        {
            var newHandle = GetHandle(ProcessName);
            if (newHandle != Handle)
            {
                _onChange?.Invoke();
            }
            Handle = newHandle;

            if (ValidHandle())
            {
                Rect procRect = new Rect();
                GetWindowRect(Handle, ref procRect);
                WindowSize = new Rectangle(procRect.Left, procRect.Top, procRect.Right - procRect.Left, procRect.Bottom - procRect.Top);
                _focusTimer.Change(0, IntervalFocusCheck);
            }
            else
            {
                _focusTimer.Change(0, Timeout.Infinite);
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
