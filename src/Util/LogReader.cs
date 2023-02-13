using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
                string userId = match.Groups["UserId"].ToString();
                Fortniter self = new Fortniter()
                {
                    Name = name,
                    UserId = userId,
                };
                Program.localPlayer = self;
                Program.form.SetSelfName(self.Name);
                Program.form.Log("[LoggedIn] Display name: " + name);
                return;
            }

            match = FortniteLogRegex.PartyMemberJoined.Match(line);
            if (match.Success)
            {
                string name = match.Groups["DisplayName"].ToString();
                string userId = match.Groups["UserId"].ToString();

                if (Program.localPlayer.Name == name) { return; }
                if (Program.fortniters.Any(x => x.Name == name)) { return; }

                Fortniter newJoin = new Fortniter()
                {
                    Name = name,
                    UserId = userId,
                };
                newJoin.Index = Array.IndexOf(Program.order, newJoin.UserIdTruncated);
                Program.fortniters.Add(newJoin);
                Program.fortniters.Sort(MiscUtil.SortFortniters);
                Program.form.Log("[PartyMemberJoined] Name: " + name + ", index: " + newJoin.Index);
                return;
            }

            match = FortniteLogRegex.TeamMemberId.Match(line);
            if (match.Success)
            {
                string userid = match.Groups["UserId"].ToString();

                foreach (var fortniter in Program.fortniters)
                {
                    if (fortniter.UserId.IndexOf("...") == -1)
                    {
                        continue;
                    }
                    if (!fortniter.UserId.StartsWith(userid.Substring(0, 5)))
                    {
                        continue;
                    }
                    if (!fortniter.UserId.EndsWith(userid.Substring(userid.Length - 5, 5)))
                    {
                        continue;
                    }

                    fortniter.UserId = userid;
                }
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
        }

        //language=regex
        private static class FortniteLogRegex
        {
            private static readonly string Timestamp = @"\[\d{4}\.\d{2}\.\d{2}\-\d{2}\.\d{2}\.\d{2}:\d{3}]";
            private static readonly string UnknownId = @"\[[\d ]{3}\]";
            private static readonly string LineStart = "^" + Timestamp + UnknownId;
            public static readonly Regex LoggedIn               = new Regex(LineStart + @"LogOnlineAccount: Display: \[UOnlineAccountCommon::ProcessUserLogin\] Successfully logged in user\. UserId=\[(?<UserId>[0-9a-fA-F]{32})\] DisplayName=\[(?<DisplayName>.{1,16})\] EpicAccountId=\[MCP:[0-9a-fA-F]{32}\] AuthTicket=\[<Redacted>\]$", RegexOptions.Compiled | RegexOptions.Singleline);
            public static readonly Regex PartyMemberJoined      = new Regex(LineStart + @"LogParty: Display: New party member state for \[(?<DisplayName>.{1,16})\] Id \[MCP:(?<UserId>[0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5})\] added to the local player's party \[V2:[0-9a-fA-F]{32}\]\.$", RegexOptions.Compiled | RegexOptions.Singleline);
            public static readonly Regex PartyMemberLeft        = new Regex(LineStart + @"LogParty: Display: Party member state for \[(?<DisplayName>.{1,16})\] Id \[MCP:[0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5}\] removed from \[.{1,16}\]'s party\.$", RegexOptions.Compiled | RegexOptions.Singleline);
            public static readonly Regex StartedGame            = new Regex(LineStart + @"LogDemo: UReplaySubsystem::RecordReplay: Starting recording with demo driver\.  Name:  FriendlyName: Unsaved Replay$", RegexOptions.Compiled | RegexOptions.Singleline);
            public static readonly Regex LeftGame               = new Regex(LineStart + @"LogDemo: StopDemo: Demo  stopped at frame \d+$", RegexOptions.Compiled | RegexOptions.Singleline);
            public static readonly Regex TeamMemberId           = new Regex(LineStart + @"LogTeamPedestal: Verbose: AFortTeamMemberPedestal::SetTeamMember - Assigning Team Member \S+ \((?<UserId>[0-9a-fA-F]{32})\) to pedestal \S+ \(VisualOrderIndex:\d+\). Pedestal was empty = \d+", RegexOptions.Compiled | RegexOptions.Singleline);
        }
    }
}
