using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteSquadOverlayClient
{
    internal class LogReader
    {
        private static readonly int[] _newlineChars = new int[] { 10, 13 };

        public bool Active { get; private set; } = false;
        public int  SleepDurationMs { get; set; } = 1000;

        private readonly string _logFile;
        private Action<string>  _onLine;
        private Action          _onReset;

        public LogReader(string logFile, Action<string> onLine, Action onReset, bool autostart = false)
        {
            _logFile = logFile;
            _onLine  = onLine;
            _onReset = onReset;

            if (autostart)
            {
                Start();
            }
        }

        public void Start()
        {
            Active = true;
            Task.Run(ReadLoop);
        }

        public void Stop()
        {
            Active = false;
        }

        private async Task ReadLoop()
        {
            FileStream fileStream = new FileStream(_logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader streamReader = new StreamReader(fileStream);
            StringBuilder lineBuilder = new StringBuilder();
            long lastLength = 0;

            while (true)
            {
                if (!Active)
                {
                    return;
                }

                int nextChar = streamReader.Read();
                if (nextChar == -1)
                {
                    if (fileStream.Length < lastLength)
                    {
                        _onReset();
                        fileStream.Seek(0, SeekOrigin.Begin);
                        streamReader.DiscardBufferedData();
                        lineBuilder.Clear();
                        lastLength = 0;
                    }
                    else
                    {
                        lastLength = fileStream.Length;
                    }

                    await Task.Delay(SleepDurationMs);
                    continue;
                }

                if (_newlineChars.Contains(nextChar))
                {
                    if (lineBuilder.Length > 0)
                    {
                        _onLine(lineBuilder.ToString());
                        lineBuilder.Clear();
                    }
                }
                else
                {
                    lineBuilder.Append(Convert.ToChar(nextChar));
                }
            }
        }
    }
}
