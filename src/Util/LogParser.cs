using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FortniteOverlay.Util
{
    internal class LogParser
    {
        private static FieldInfo[] LogRegexFields = typeof(FortniteLogActions).GetFields(BindingFlags.Static | BindingFlags.Public);

        public static void ProcessLine(string line)
        {
            foreach (var fieldInfo in LogRegexFields)
            {
                var action = fieldInfo.GetValue(null) as LogAction;
                if (action == null) { throw new Exception("LogParser action was null."); }
                var match = action.LineRegex.Match(line);
                if (match.Success)
                {
                    if (!action.SuppressLog)
                    {
                        Program.form.Log(FormatMatchGroups(fieldInfo.Name, match));
                    }
                    action.Action?.Invoke(match);
                    return;
                }
            }
        }

        private static string FormatMatchGroups(string name, Match match)
        {
            string output = "";
            foreach (Group group in match.Groups.Cast<Group>().Skip(1))
            {
                output += $"{group.Name}: {group.Value}, ";
            }
            output = name + (string.IsNullOrWhiteSpace(output) ? "" : " - " + output.TrimEnd(new char[] { ' ', ',' }));
            return output;
        }

        private class LogAction
        {
            public Regex LineRegex { get; set; }
            public Action<Match> Action { get; set; }
            public bool SuppressLog { get; set; }

            public LogAction(Regex lineRegex)
            {
                LineRegex = lineRegex;
            }
        }

        private static class FortniteLogActions
        {
            private static readonly string Timestamp = @"\[\d{4}\.\d{2}\.\d{2}\-\d{2}\.\d{2}\.\d{2}:\d{3}]";
            private static readonly string UnknownId = @"\[[\d ]{3}\]";
            private static readonly string LineStart = "^" + Timestamp + UnknownId;
            private static readonly RegexOptions regexOptions = RegexOptions.Compiled | RegexOptions.Singleline;

            public static LogAction LoggedIn = new LogAction(new Regex(LineStart + @"LogOnlineAccount: Display: \[UOnlineAccountCommon::ProcessUserLogin\] Successfully logged in user\. UserId=\[(?<UserId>[0-9a-fA-F]{32})\] DisplayName=\[(?<DisplayName>.{1,16})\] EpicAccountId=\[MCP:[0-9a-fA-F]{32}\] AuthTicket=\[<Redacted>\]$", regexOptions))
            {
                Action = (match) =>
                {
                    string name = match.Groups["DisplayName"].ToString();
                    string userId = match.Groups["UserId"].ToString();
                    Fortniter self = new Fortniter()
                    {
                        Name = name,
                        UserId = userId,
                    };
                    Program.localPlayer = self;
                },
            };

            public static LogAction PartyMemberJoined = new LogAction(new Regex(LineStart + @"LogParty: Display: New party member state for \[(?<DisplayName>.{1,16})\] Id \[MCP:(?<UserId>[0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5})\] added to the local player's party \[V2:[0-9a-fA-F]{32}\]\.$", regexOptions))
            {
                Action = (match) =>
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
                },
            };

            public static LogAction PartyMemberLeft = new LogAction(new Regex(LineStart + @"LogParty: Display: Party member state for \[(?<DisplayName>.{1,16})\] Id \[MCP:[0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5}\] removed from \[.{1,16}\]'s party\.$", regexOptions))
            {
                Action = (match) =>
                {
                    string name = match.Groups["DisplayName"].ToString();
                    var leaver = Program.fortniters.Find(x => x.Name == name);
                    if (leaver != null) { Program.fortniters.Remove(leaver); };
                },
            };

            public static LogAction StartedGame = new LogAction(new Regex(LineStart + @"LogDemo: UReplaySubsystem::RecordReplay: Starting recording with demo driver\.  Name:  FriendlyName: Unsaved Replay$", regexOptions))
            {
                // No longer used
                Action = (match) => { },
                SuppressLog = true,
            };

            public static LogAction LeftGame = new LogAction(new Regex(LineStart + @"LogDemo: StopDemo: Demo  stopped at frame \d+$", regexOptions))
            {
                // No longer used
                Action = (match) => { },
                SuppressLog = true,
            };

            public static LogAction TeamMemberId = new LogAction(new Regex(LineStart + @"LogTeamPedestal: Verbose: AFortTeamMemberPedestal::SetTeamMember - Assigning Team Member \S+ \((?<UserId>[0-9a-fA-F]{32})\) to pedestal \S+ \(VisualOrderIndex:\d+\). Pedestal was empty = \d+", regexOptions))
            {
                Action = (match) =>
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
                },
                SuppressLog = true,
            };
        }
    }
}
