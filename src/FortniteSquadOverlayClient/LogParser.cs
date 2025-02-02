using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FortniteSquadOverlayClient
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
                    FortnitePlayer self = new FortnitePlayer()
                    {
                        Name = name,
                        UserId = userId,
                    };
                    Program.LocalPlayer = self;
                },
            };

            public static LogAction PartyMemberJoined = new LogAction(new Regex(LineStart + @"LogParty: Display: New party member state for \[(?<DisplayName>.{1,16})\] Id \[MCP:(?<UserId>[0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5})\] added to the local player's party \[V2:[0-9a-fA-F]{32}\]\.$", regexOptions))
            {
                Action = (match) =>
                {
                    string name = match.Groups["DisplayName"].ToString();
                    string userId = match.Groups["UserId"].ToString();

                    if (Program.LocalPlayer.Name == name) { return; }
                    if (Program.CurrentSquad.Any(x => x.Name == name)) { return; }

                    FortnitePlayer newJoin = new FortnitePlayer()
                    {
                        Name = name,
                        UserId = userId,
                    };
                    newJoin.Index = Program.UserIdOrder.IndexOf(newJoin.UserIdTruncated);
                    Program.CurrentSquad.Add(newJoin);
                    Program.CurrentSquad.Sort(MiscUtil.SortFortniters);
                },
            };

            public static LogAction PartyMemberLeft = new LogAction(new Regex(LineStart + @"LogParty: Display: Party member state for \[(?<DisplayName>.{1,16})\] Id \[MCP:[0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5}\] removed from \[.{1,16}\]'s party\.$", regexOptions))
            {
                Action = (match) =>
                {
                    string name = match.Groups["DisplayName"].ToString();
                    var leaver = Program.CurrentSquad.Find(x => x.Name == name);
                    if (leaver != null) { Program.CurrentSquad.Remove(leaver); };
                },
            };

            public static LogAction PartyMemberReadyState = new LogAction(new Regex(LineStart + @"LogMatchmakingUtility: \[HandlePartyMemberReadinessChanged called\] MCP:(?<UserId>[0-9a-fA-F]{5}[0-9a-fA-F\.]{3,22}[0-9a-fA-F]{5}), Party \(V2:[0-9a-fA-F]{32}\) readiness changed to: (?<State>.*)$", regexOptions))
            {
                Action = (match) =>
                {
                    string userId = match.Groups["UserId"].ToString();
                    if (userId == Program.LocalPlayer.UserId)
                    {
                        return;
                    }

                    string state  = match.Groups["State"].ToString();
                    var    user   = Program.CurrentSquad.Find(x => x.UserIdTruncated == userId) ?? throw new Exception("Unable to find player with ID: " + userId);

                    switch(state)
                    {
                        case "Ready":
                            user.State = FortnitePlayer.ReadyState.Ready;
                            break;
                        case "Not Ready":
                            user.State = FortnitePlayer.ReadyState.NotReady;
                            break;
                        case "Sitting Out":
                            user.State = FortnitePlayer.ReadyState.SittingOut;
                            break;
                    }
                },
                SuppressLog = true,
            };

            public static LogAction StartedGame = new LogAction(new Regex(LineStart + @"LogDemo: UReplaySubsystem::RecordReplay: Starting recording with demo driver\.  Name:  FriendlyName: Unsaved Replay$", regexOptions))
            {
                // No longer used
                Action = (match) => { },
            };

            public static LogAction LeftGame = new LogAction(new Regex(LineStart + @"LogDemo: StopDemo: Demo  stopped at frame \d+$", regexOptions))
            {
                // No longer used
                Action = (match) => { },
            };

            public static LogAction TeamMemberId = new LogAction(new Regex(LineStart + @"LogTeamPedestal: Verbose: AFortTeamMemberPedestal::SetTeamMember - Assigning Team Member \S+ \((?<UserId>[0-9a-fA-F]{32})\) to pedestal \S+ \(VisualOrderIndex:\d+\). Pedestal was empty = \d+", regexOptions))
            {
                Action = (match) =>
                {
                    string userid = match.Groups["UserId"].ToString();

                    foreach (var fortniter in Program.CurrentSquad)
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
