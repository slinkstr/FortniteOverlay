using System.IO;
using System.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FortniteOverlay.Util
{
    internal class LogReader
    {
        public delegate void ParseLine(string line);
        public delegate void LogFileReset();
        private readonly string _logFile;
        private readonly ParseLine _parseLineCallback;
        private readonly LogFileReset _logFileResetCallback;
        private int _sleepDurationMs;
        private bool _readingPaused = false;
        private int[] _newlineChars = new int[] { 10, 13 };

        public LogReader(string logFile, ParseLine parseLineCallback, LogFileReset logFileResetCallback, int sleepDurationMs = 1000)
        {
            _logFile = logFile;
            _parseLineCallback = parseLineCallback;
            _logFileResetCallback = logFileResetCallback;
            _sleepDurationMs = sleepDurationMs;
            _logFile = Path.Combine(Path.GetDirectoryName(logFile), "test-2players.log");
        }

        public async Task BeginReading()
        {
            FileStream fileStream = new FileStream(_logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader streamReader = new StreamReader(fileStream);
            long lastTotalLength = 0;
            StringBuilder lineBuilder = new StringBuilder();

            while (true)
            {
                if (_readingPaused)
                {
                    await Task.Delay(_sleepDurationMs);
                    continue;
                }

                int nextChar = streamReader.Read();
                if (nextChar != -1)
                {
                    if (_newlineChars.Contains(nextChar))
                    {
                        if (lineBuilder.Length > 0)
                        {
                            _parseLineCallback(lineBuilder.ToString());
                            lineBuilder.Clear();
                        }
                    }
                    else
                    {
                        lineBuilder.Append(Convert.ToChar(nextChar));
                    }
                }
                else
                {
                    if (fileStream.Length < lastTotalLength)
                    {
                        _logFileResetCallback();
                        fileStream.Seek(0, SeekOrigin.Begin);
                        streamReader.DiscardBufferedData();
                        lineBuilder.Clear();
                        lastTotalLength = 0;
                    }
                    else
                    {
                        lastTotalLength = fileStream.Length;
                        await Task.Delay(_sleepDurationMs);
                    }
                }
            }
        }

        public void Pause()
        {
            _readingPaused = true;
        }

        public void Resume()
        {
            _readingPaused = false;
        }
    }
}
