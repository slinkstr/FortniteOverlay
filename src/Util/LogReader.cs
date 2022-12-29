using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace FortniteOverlay.Util
{
    internal class LogReader
    {
        public string LogDir  { get; private set; }
        public string LogFile { get; private set; }

        public LogReader(string logDir, string logFile)
        {
            LogDir = logDir;
            LogFile = logFile;
        }

        public void ReadLogFile(object sender, DoWorkEventArgs e)
        {
            //LogFile = "Test-3teammates.log";
            var fs = new FileStream(LogDir + "\\" + LogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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
                            Program.fortniters.Clear();
                        }
                        totalLenCached = fs.Length;
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        private static void ProcessLine(string line)
        {
            var match = FortniteLogRegex.LoggedIn.Match(line);
            if (match.Success)
            {
                string name = match.Groups["DisplayName"].ToString();
                Program.hostName = name;
                Program.form.SetHostName(name);
                Program.form.Log("[LoggedIn] Display name: " + name);
                return;
            }

            //match = FortniteLogRegex.PartyMemberJoined.Match(line);
            //if (match.Success)
            //{
            //    string name = match.Groups["DisplayName"].ToString();
            //
            //    if (Program.hostName == name)                    { return; }
            //    if (Program.fortniters.Any(x => x.Name == name)) { return; }
            //
            //    Fortniter newJoin = new Fortniter()
            //    {
            //        Name = name,
            //    };
            //    Program.fortniters.Add(newJoin);
            //    Program.form.Log("[PartyMemberJoined] Name: " + name);
            //    return;
            //}

            match = FortniteLogRegex.PartyMemberJoinedAlt.Match(line);
            if (match.Success)
            {
                string name = match.Groups["DisplayName"].ToString();
                int partyIndex = Int32.Parse(match.Groups["PartyIndex"].ToString());

                if (Program.hostName == name) { return; }

                var matchingPlayer = Program.fortniters.FirstOrDefault(x => x.Name == name);
                if (matchingPlayer != null)
                {
                    matchingPlayer.PartyIndex = partyIndex;
                    return;
                }

                Fortniter newJoin = new Fortniter()
                {
                    Name = name,
                    PartyIndex = partyIndex,
                    Index = Array.IndexOf(Program.order, name)
                };
                    
                Program.fortniters.Add(newJoin);
                Program.fortniters.Sort(MiscUtil.SortFortniters);
                Program.form.Log("[PartyMemberJoinedAlt] Name: " + newJoin.Name + ", index: " + newJoin.Index);
                return;
            }

            match = FortniteLogRegex.PartyMemberUpdated.Match(line);
            if (match.Success)
            {
                string name = match.Groups["DisplayName"].ToString();
                int index = Int32.Parse(match.Groups["PartyIndex"].ToString());

                if (Program.hostName == name) { return; }

                var matchingPlayer = Program.fortniters.FirstOrDefault(x => x.Name == name);
                if (matchingPlayer != null)
                {
                    matchingPlayer.PartyIndex = index;
                }
                //Program.form.Log("[PartyMemberUpdated] Name: " + name + ", index: " + index);
                return;
            }

            match = FortniteLogRegex.PartyMemberLeft.Match(line);
            if (match.Success)
            {
                string name = match.Groups["DisplayName"].ToString();
                var leaver = Program.fortniters.Find(x => x.Name == name);
                if (leaver != null) { Program.fortniters.Remove(leaver); };

                Program.form.Log("[PartyMemberLeft] Name: " + name);
                return;
            }

            //match = FortniteLogRegex.StartedGame.Match(line);
            //if (match.Success)
            //{
            //    Program.form.Log("[StartedGame]");
            //    Program.inGame = true;
            //    return;
            //}
            //match = FortniteLogRegex.LeftGame.Match(line);
            //if (match.Success)
            //{
            //    Program.form.Log("[LeftGame]");
            //    Program.inGame = false;
            //    return;
            //}
        }

        //language=regex
        private static class FortniteLogRegex
        {
            private static readonly string Timestamp = @"\[\d{4}\.\d{2}\.\d{2}\-\d{2}\.\d{2}\.\d{2}:\d{3}]";
            private static readonly string UnknownId = @"\[[\d ]{3}\]";
            private static readonly string LineStart = "^" + Timestamp + UnknownId;
            public static readonly Regex LoggedIn               = new Regex(LineStart + @"LogOnlineAccount: Display: \[UOnlineAccountCommon::ProcessUserLogin\] Successfully logged in user\. UserId=\[[0-9a-fA-F]{32}\] DisplayName=\[(?<DisplayName>.{1,16})\] EpicAccountId=\[MCP:[0-9a-fA-F]{32}\] AuthTicket=\[<Redacted>\]$", RegexOptions.Compiled | RegexOptions.Singleline);
            public static readonly Regex PartyMemberJoined      = new Regex(LineStart + @"LogParty: Display: New party member state for \[(?<DisplayName>.{1,16})\] Id \[MCP:[0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5}\] added to the local player's party \[V2:[0-9a-fA-F]{32}\]\.$", RegexOptions.Compiled | RegexOptions.Singleline);
            public static readonly Regex PartyMemberJoinedAlt   = new Regex(LineStart + @"LogParty: Verbose: \[(?<DisplayName>.{1,16})\] Id \[MCP:[0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5}\] added to team \[\w+\] at index \[(?<PartyIndex>\d+)\] in \[.{1,16}\]'s game.$", RegexOptions.Compiled | RegexOptions.Singleline);
            public static readonly Regex PartyMemberUpdated     = new Regex(LineStart + @"LogParty: Verbose: \[(?<DisplayName>.{1,16})\] Id \[MCP:[0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5}\] team member data updated, team \[\w+\] at index \[(?<PartyIndex>\d+)\] in \[.{1,16}\]'s game\.", RegexOptions.Compiled | RegexOptions.Singleline);
            public static readonly Regex PartyMemberLeft        = new Regex(LineStart + @"LogParty: Display: Party member state for \[(?<DisplayName>.{1,16})\] Id \[MCP:[0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5}\] removed from \[.{1,16}\]'s party\.$", RegexOptions.Compiled | RegexOptions.Singleline);
            public static readonly Regex StartedGame            = new Regex(LineStart + @"LogDemo: UReplaySubsystem::RecordReplay: Starting recording with demo driver\.  Name:  FriendlyName: Unsaved Replay$", RegexOptions.Compiled | RegexOptions.Singleline);
            public static readonly Regex StartedGameAlt         = new Regex(LineStart + @"LogLocalFileReplay: Writing replay to '.*' with \d+\.\d{2}MB free$", RegexOptions.Compiled | RegexOptions.Singleline);
            public static readonly Regex LeftGame               = new Regex(LineStart + @"LogDemo: StopDemo: Demo  stopped at frame \d+$", RegexOptions.Compiled | RegexOptions.Singleline);
        }
    }
}
