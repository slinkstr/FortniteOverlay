using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FortniteOverlay.Util
{
    internal class LogReadUtil
    {
        public static void ReadLogFile(object sender, DoWorkEventArgs e)
        {
            string logDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FortniteGame\\Saved\\Logs";
            string logFile = "FortniteGame.log";

            var fs = new FileStream(logDir + "\\" + logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            long totalLenCached = 0;
            using (var sr = new StreamReader(fs))
            {
                var s = "";
                while (true)
                {
                    s = sr.ReadLine();
                    if (s != null)
                    {
                        ProcessLine(s);
                    }
                    else
                    {
                        if (fs.Length < totalLenCached)
                        {
                            Program.form.Log("Fortnite restarted, resetting log file.");
                            fs.Seek(0, SeekOrigin.Begin);
                            sr.DiscardBufferedData();
                        }
                        totalLenCached = fs.Length;
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        public static void ProcessLine(string line)
        {
            var match = Regex.Match(line, FortniteLogRegex.LoggedIn);
            if (match.Success)
            {
                Program.hostId = match.Groups[1].ToString();
                Program.form.SetHostId(Program.hostId);
                Program.hostName = match.Groups[2].ToString();
                Program.form.SetHostName(Program.hostName);
                Program.form.Log("[LoggedIn] User ID: " + match.Groups[1] + ", Display name: " + match.Groups[2]);
                return;
            }

            match = Regex.Match(line, FortniteLogRegex.PartyMemberJoined);
            if (match.Success)
            {
                if (Program.fortniters.Any(x => x.Name == match.Groups[1].ToString())) { return; }
                if (match.Groups[1].ToString() == Program.hostName)                    { return; }

                Fortniter newJoin = new Fortniter();
                newJoin.Name = match.Groups[1].ToString();
                Program.fortniters.Add(newJoin);
                Program.form.SetSquadGear(string.Join(", ", Program.fortniters.Select(x => x.Name)));

                Program.form.Log("[PartyMemberJoined] Name: " + match.Groups[1] + ", ID: " + match.Groups[2]);
                return;
            }

            match = Regex.Match(line, FortniteLogRegex.PartyMemberLeft);
            if (match.Success)
            {
                var leaver = Program.fortniters.Find(x => x.Name == match.Groups[1].ToString());
                if (leaver != null) { Program.fortniters.Remove(leaver); };
                Program.form.SetSquadGear(string.Join(", ", Program.fortniters.Select(x => x.Name)));

                Program.form.Log("[PartyMemberLeft] Name: " + match.Groups[1] + ", ID: " + match.Groups[2] + ", Host: " + match.Groups[3]);
                return;
            }

            match = Regex.Match(line, FortniteLogRegex.StartedGame);
            if (match.Success)
            {
                Program.form.Log("[StartedGame]");
                Program.inGame = true;
                return;
            }
            match = Regex.Match(line, FortniteLogRegex.LeftGame);
            if (match.Success)
            {
                Program.form.Log("[LeftGame]");
                Program.inGame = false;
                return;
            }
        }
    }

    public static class FortniteLogRegex
    {
        private static string Timestamp = @"\[\d{4}\.\d{2}\.\d{2}\-\d{2}\.\d{2}\.\d{2}:\d{3}]";
        private static string UnknownId = @"\[[\d ]{3}\]";
        public static string LoggedIn = "^" + Timestamp + UnknownId + @"LogOnlineAccount: Display: \[UOnlineAccountCommon::ProcessUserLogin\] Successfully logged in user\. UserId=\[([0-9a-fA-F]{32})\] DisplayName=\[(.{1,32})\] EpicAccountId=\[MCP:[0-9a-fA-F]{32}\] AuthTicket=\[<Redacted>\]$";
        public static string PartyMemberJoined = "^" + Timestamp + UnknownId + @"LogParty: Display: New party member state for \[(.{1,32})\] Id \[MCP:([0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5})\] added to the local player's party \[V2:[0-9a-fA-F]{32}\]\.$";
        public static string PartyMemberLeft = "^" + Timestamp + UnknownId + @"LogParty: Display: Party member state for \[(.{1,32})\] Id \[MCP:([0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5})\] removed from \[(.{1,32})\]'s party\.$";
        public static string StartedGame = "^" + Timestamp + UnknownId + @"LogDemo: UReplaySubsystem::RecordReplay: Starting recording with demo driver\.  Name:  FriendlyName: Unsaved Replay$";
        //public static string StartedGameAlt = "^" + Timestamp + UnknownId + @"LogLocalFileReplay: Writing replay to '.*' with \d+\.\d{2}MB free$";
        public static string LeftGame = "^" + Timestamp + UnknownId + @"LogDemo: StopDemo: Demo  stopped at frame \d+$";
    }
}
