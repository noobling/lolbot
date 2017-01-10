using BananaLib;
using BananaLib.RiotObjects.Platform;
using BananaLib.RiotObjects.Team;
using Newtonsoft.Json.Linq;
using RtmpSharp.IO;
using RtmpSharp.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ezBot
{
    internal class ezBot
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        public LoginDataPacket loginPacket = new LoginDataPacket();
        public bool firstTimeInLobby = true;
        public bool firstTimeInQueuePop = true;
        public bool firstTimeInCustom = true;
        public bool firstTimeInPostChampSelect = true;
        public Process exeProcess;
        public LoLClient connection;
        public string Accountname;
        public string Password;
        public string ipath;
        public bool useGarena;
        public ChampionDTO[] AvailableChampions;
        public DateTime? GameStartedAt = null;

        public string region { get; set; }

        public string sumName { get; set; }

        public double sumId { get; set; }

        public double sumLevel { get; set; }

        public double archiveSumLevel { get; set; }

        public double rpBalance { get; set; }

        public double ipBalance { get; set; }

        public double m_leaverBustedPenalty { get; set; }

        public string m_accessToken { get; set; }

        public string queueType { get; set; }

        public string actualQueueType { get; set; }

        private bool ShouldBeInGame { get; set; }
        private bool IsInQueue { get; set; }

        private LobbyStatus Lobby { get; set; }

        private bool m_isLeader { get; set; }

        private int pickAtTurn = 0;
        private int turn = 0;

        private ConsoleEventDelegate handler;   // Keeps it from getting garbage collected

        private delegate bool ConsoleEventDelegate(int eventType);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        //
        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(int hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(int hWnd, StringBuilder title, int size);

        private delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        private bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                try
                {
                    connection.LeaveGroupFinderLobby();
                    connection.DestroyGroupFinderLobby();
                    connection.Disconnect();
                }
                catch (Exception ex)
                {
                    Tools.Log(ex.StackTrace);
                }
            }
            return false;
        }

        private int notRespondingTick = 0;

        public ezBot(string username, string password, string reg, string _queueType, string LoLVersion, bool isLeader)
        {
            useGarena = Tools.ParseEnum<Region>(reg).UseGarena();
            ipath = useGarena ? Program.lolGarenaPath : Program.lolPath;
            queueType = _queueType;
            region = reg;
            connection = new LoLClient(username, password, Tools.ParseEnum<Region>(region), LoLVersion);
            Accountname = username;
            Password = password;
            connection.OnConnect += connection_OnConnect;
            connection.OnDisconnect += connection_OnDisconnect;
            connection.OnError += connection_OnError;
            connection.OnLogin += connection_OnLogin;
            connection.OnMessageReceived += connection_OnMessageReceived;
            connection.ConnectAndLogin().Wait();
            m_isLeader = isLeader;

            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            Program.OnInvite += OnReceiveInvite;

            new Thread(() =>
            {
                while (true)
                {
                    if (exeProcess != null)
                    {
                        if(!exeProcess.Responding)
                        {
                            notRespondingTick++;
                            if(notRespondingTick == 7)
                            {
                                try
                                {
                                    var processInfo = new ProcessStartInfo()
                                    {
                                        Arguments = "/f /PID " + exeProcess.Id,
                                        FileName = "taskkill.exe",
                                        WindowStyle = ProcessWindowStyle.Hidden
                                    };
                                    Process.Start(processInfo);
                                }
                                catch(Exception ex)
                                {
                                    Tools.Log(ex.StackTrace);
                                }
                            }
                        }else notRespondingTick = 0;
                        foreach (ProcessThread processThread in exeProcess.Threads)
                        {
                            try
                            {
                                EnumThreadWindows(processThread.Id, (hWnd, lParam) =>
                                {
                                    //Check if Window is Visible or not.
                                    if (!IsWindowVisible((int)hWnd))
                                        return true;

                                    //Get the Window's Title.
                                    StringBuilder title = new StringBuilder(256);
                                    GetWindowText((int)hWnd, title, 256);

                                    //Check if Window has Title.
                                    if (title.Length == 0)
                                        return true;

                                    if (title.ToString().ToLower() == "network warning" || title.ToString().ToLower() == "failed to connect")
                                    {
                                        exeProcess.Kill();
                                        Thread.Sleep(1000);
                                        if (exeProcess.Responding)
                                        {
                                            var processInfo = new ProcessStartInfo()
                                            {
                                                Arguments = "/f /PID " + exeProcess.Id,
                                                FileName = "taskkill.exe",
                                                WindowStyle = ProcessWindowStyle.Hidden
                                            };
                                            Process.Start(processInfo);
                                        }
                                    }

                                    return true;
                                }, IntPtr.Zero);
                            }
                            catch(Exception ex)
                            {
                                Tools.Log(ex.StackTrace);
                            }
                        }
                    }
                    Thread.Sleep(10 * 1000);
                }
            }).Start();
        }

        public string EncryptText(string input, string password)
        {
            var hasher = new SHA1CryptoServiceProvider();
            byte[] textWithSaltBytes = Encoding.UTF8.GetBytes(string.Concat(input, password));
            byte[] hashedBytes = hasher.ComputeHash(textWithSaltBytes);
            hasher.Clear();
            return Convert.ToBase64String(hashedBytes);
        }

        public async void OnReceiveInvite(string from, string to, string inviteId)
        {
            if (to.ToLower() == sumName.ToLower())
            {
                if (from.ToLower() == Program.leaderName.ToLower())
                {
                    Tools.ConsoleMessage(string.Format(Program.Translator.AcceptingLobbyInvite, sumName), ConsoleColor.Cyan);
                    await Task.Delay(new Random().Next(1, 3) * new Random().Next(800, 1200));
                    await connection.AcceptLobbyInvite(inviteId);
                }
                else
                {
                    await Task.Delay(new Random().Next(1, 3) * new Random().Next(800, 1200));
                    await connection.DeclineLobbyInvite(inviteId);
                }
            }
        }

        public bool IsCurrentlyInMatch()
        {
            try
            {
                WebClient webClient = new WebClient();
                var json = webClient.DownloadString(string.Format("https://{0}.api.pvp.net/observer-mode/rest/consumer/getSpectatorGameInfo/{0}1/{1}?api_key=RGAPI-4840c81c-2c7f-47bc-a370-11f73e20cf19", region, sumId));
                return !json.Contains("\"status_code\": 404");
            }
            catch (Exception ex)
            {
                Tools.Log(ex.StackTrace);
            }
            return false;
        }

        public async void connection_OnMessageReceived(object sender, object messageReceivedEventArgs)
        {
            try
            {
                object message = !(messageReceivedEventArgs is MessageReceivedEventArgs) ? messageReceivedEventArgs : ((MessageReceivedEventArgs)messageReceivedEventArgs).Message.Body;
                if (message is PlatformGameLifecycleDTO)
                {
                    PlatformGameLifecycleDTO PGLC = message as PlatformGameLifecycleDTO;
                    Tools.ConsoleMessage(PGLC.Game.GameState, ConsoleColor.Gray);
                    Tools.ConsoleMessage(PGLC.Game.GameStateString, ConsoleColor.Gray);
                    PGLC = null;
                }

                #region LobbyStatus

                if (message is LobbyStatus && !IsInQueue)
                {
                    Lobby = message as LobbyStatus;
                    if (Lobby.Members.Count == GetFriendsToInvite().Count + 1)
                    {
                        Tools.ConsoleMessage(Program.Translator.AllPlayersAccepted, ConsoleColor.Cyan);
                        EnterQueue();
                    }
                    else
                    {
                        Tools.ConsoleMessage(string.Format(Program.Translator.PlayersAcceptedCount, Lobby.Members.Count - 1, GetFriendsToInvite().Count), ConsoleColor.Cyan);
                    }
                }

                #endregion LobbyStatus

                #region GameDTO

                if (message is GameDTO)
                {
                    GameDTO gameDTO = message as GameDTO;
                    string gameState;
                    if ((gameState = gameDTO.GameState) != null)
                    {
                        switch (gameState)
                        {
                            case "IDLE":
                            break;

                            case "TEAM_SELECT":
                            if (firstTimeInCustom)
                            {
                                Tools.ConsoleMessage(string.Format(Program.Translator.EnteringChampionSelect, sumName), ConsoleColor.White);
                                await connection.StartChampionSelection(gameDTO.Id, gameDTO.OptimisticLock);
                                firstTimeInCustom = false;
                            }
                            break;

                            case "CHAMP_SELECT":
                            {
                                firstTimeInCustom = true;
                                firstTimeInQueuePop = true;
                                if (firstTimeInLobby)
                                {
                                    if (queueType != "ARAM")
                                    {
                                        if (pickAtTurn == 0)
                                        {
                                            lock (Program.FriendsChampions)
                                            {
                                                if (Program.FriendsChampions.Count != 0) Program.FriendsChampions.Clear();
                                            }

                                            Tools.ConsoleMessage(string.Format(Program.Translator.YouAreInChampionSelect, sumName), ConsoleColor.White);

                                            try
                                            {
                                                await connection.SetClientReceivedGameMessage(gameDTO.Id, "CHAMP_SELECT_CLIENT");
                                            }
                                            catch (InvocationException ex)
                                            {
                                                Tools.Log(ex.StackTrace);
                                                Tools.ConsoleMessage("Fault Code: " + ex.FaultCode + " Fault String" + ex.FaultString + " Fault Detail:" + ex.FaultDetail + "Root Cause:" + ex.RootCause, ConsoleColor.White);
                                            }
                                            if (!m_isLeader || !Program.queueWithFriends)
                                            {
                                                pickAtTurn = new Random().Next(1, 3);
                                                turn = 0;
                                                return;
                                            }
                                        }
                                        else if (turn < pickAtTurn)
                                        {
                                            turn++;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        Tools.ConsoleMessage(string.Format(Program.Translator.YouAreInChampionSelect, sumName), ConsoleColor.White);
                                        try
                                        {
                                            await connection.SetClientReceivedGameMessage(gameDTO.Id, "CHAMP_SELECT_CLIENT");
                                        }
                                        catch (InvocationException ex)
                                        {
                                            Tools.Log(ex.StackTrace);
                                            Tools.ConsoleMessage("Fault Code: " + ex.FaultCode + " Fault String" + ex.FaultString + " Fault Detail:" + ex.FaultDetail + "Root Cause:" + ex.RootCause, ConsoleColor.White);
                                        }
                                    }
                                    firstTimeInLobby = false;

                                    #region Not Aram

                                    if (queueType != "ARAM" && queueType != "ARAM_UNRANKED_1x1" && queueType != "ARAM_UNRANKED_2x2" && (queueType != "ARAM_UNRANKED_3x3" && queueType != "ARAM_UNRANKED_5x5") && queueType != "ARAM_UNRANKED_6x6")
                                    {
                                        List<ChampionDTO> ChampList = new List<ChampionDTO>(AvailableChampions);

                                        if (loginPacket.ClientSystemStates.freeToPlayChampionsForNewPlayersMaxLevel >= loginPacket.AllSummonerData.SummonerLevel.Level)
                                        {
                                            foreach (ChampionDTO championDto in ChampList)
                                            {
                                                var freeToPlay = ((IEnumerable<int>)loginPacket.ClientSystemStates.freeToPlayChampionForNewPlayersIdList).Contains(championDto.ChampionId);
                                                if (freeToPlay)
                                                {
                                                    championDto.FreeToPlay = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            foreach (ChampionDTO championDto in ChampList)
                                            {
                                                var freeToPlay = ((IEnumerable<int>)loginPacket.ClientSystemStates.freeToPlayChampionIdList).Contains(championDto.ChampionId);
                                                if (freeToPlay)
                                                {
                                                    championDto.FreeToPlay = true;
                                                }
                                            }
                                        }
                                        int selectedChampionId = 22;
                                        string championName = "Ashe";
                                        string[] strArray = new string[5]
                                        {
                                            Program.firstChampionPick.ToLower(),
                                            Program.secondChampionPick.ToLower(),
                                            Program.thirdChampionPick.ToLower(),
                                            Program.fourthChampionPick.ToLower(),
                                            Program.fifthChampionPick.ToLower()
                                        };

                                        if(Program.queueWithFriends && (GetFriendsToInvite().Contains(sumName.ToLower()) || Program.leaderName.ToLower() == sumName.ToLower() ))
                                        {
                                            lock (Program.FriendsChampions)
                                            {
                                                var tmp = strArray.ToList();
                                                foreach(var champion in Program.FriendsChampions)
                                                {
                                                    tmp.Remove(champion);
                                                }
                                                strArray = tmp.ToArray();

                                                if (Program.randomChampionPick)
                                                {
                                                    List<string> champList = new List<string>();
                                                    champList.AddRange(strArray);
                                                    bool found = false;
                                                    while (!found && champList.Count != 0)
                                                    {
                                                        var index = new Random().Next(0, champList.Count - 1);
                                                        var championString = champList[index];
                                                        champList.RemoveAt(index);

                                                        int championId = Enums.GetChampion(championString);
                                                        ChampionDTO champDto = ChampList.FirstOrDefault(c => c.ChampionId == championId);

                                                        if (champDto != null && (champDto.Owned || champDto.FreeToPlay && !gameDTO.PlayerChampionSelections.Any(c => c.ChampionId == championId)))
                                                        {
                                                            selectedChampionId = championId;
                                                            championName = UcFirst(Enums.GetChampionById(selectedChampionId));
                                                            if (string.IsNullOrEmpty(championName)) championName = UcFirst(championString);
                                                            found = true;
                                                            break;
                                                        }
                                                        champDto = null;
                                                        championString = null;
                                                    }
                                                }
                                                else
                                                {
                                                    for (int index = 0; index < strArray.Length; ++index)
                                                    {
                                                        string championString = strArray[index];
                                                        int championId = Enums.GetChampion(championString);
                                                        ChampionDTO champDto = ChampList.FirstOrDefault(c => c.ChampionId == championId);
                                                        if (champDto != null && (champDto.Owned || champDto.FreeToPlay && !gameDTO.PlayerChampionSelections.Any(c => c.ChampionId == championId)))
                                                        {
                                                            selectedChampionId = championId;
                                                            championName = UcFirst(Enums.GetChampionById(selectedChampionId));
                                                            if (string.IsNullOrEmpty(championName)) championName = UcFirst(championString);
                                                            break;
                                                        }
                                                        champDto = null;
                                                        championString = null;
                                                    }
                                                }
                                                Program.FriendsChampions.Add(championName.ToLower());
                                            }
                                        }else
                                        {
                                            if (Program.randomChampionPick)
                                            {
                                                List<string> champList = new List<string>();
                                                champList.AddRange(strArray);
                                                bool found = false;
                                                while (!found && champList.Count != 0)
                                                {
                                                    var index = new Random().Next(0, champList.Count - 1);
                                                    var championString = champList[index];
                                                    champList.RemoveAt(index);

                                                    int championId = Enums.GetChampion(championString);
                                                    ChampionDTO champDto = ChampList.FirstOrDefault(c => c.ChampionId == championId);

                                                    if (champDto != null && (champDto.Owned || champDto.FreeToPlay && !gameDTO.PlayerChampionSelections.Any(c => c.ChampionId == championId)))
                                                    {
                                                        selectedChampionId = championId;
                                                        championName = UcFirst(Enums.GetChampionById(selectedChampionId));
                                                        if (string.IsNullOrEmpty(championName)) championName = UcFirst(championString);
                                                        found = true;
                                                        break;
                                                    }
                                                    champDto = null;
                                                    championString = null;
                                                }
                                            }
                                            else
                                            {
                                                for (int index = 0; index < strArray.Length; ++index)
                                                {
                                                    string championString = strArray[index];
                                                    int championId = Enums.GetChampion(championString);
                                                    ChampionDTO champDto = ChampList.FirstOrDefault(c => c.ChampionId == championId);
                                                    if (champDto != null && (champDto.Owned || champDto.FreeToPlay && !gameDTO.PlayerChampionSelections.Any(c => c.ChampionId == championId)))
                                                    {
                                                        selectedChampionId = championId;
                                                        championName = UcFirst(Enums.GetChampionById(selectedChampionId));
                                                        if (string.IsNullOrEmpty(championName)) championName = UcFirst(championString);
                                                        break;
                                                    }
                                                    champDto = null;
                                                    championString = null;
                                                }
                                            }
                                        }
                                        
                                        strArray = null;
                                        try
                                        {
                                            await connection.SelectChampion(selectedChampionId);
                                        }
                                        catch (InvocationException ex)
                                        {
                                            Tools.Log(ex.StackTrace);
                                            Tools.ConsoleMessage(string.Format(Program.Translator.ChampionNotAvailable, championName, sumName), ConsoleColor.Red);
                                            Console.WriteLine(ex.StackTrace);
                                        }
                                        Tools.ConsoleMessage(string.Format(Program.Translator.SelectedChampion, UcFirst(championName.ToLower()), sumName), ConsoleColor.DarkYellow);
                                        await Task.Delay(new Random().Next(1, 9) * new Random().Next(800, 1000));
                                        await connection.ChampionSelectCompleted();
                                        Tools.ConsoleMessage(string.Format(Program.Translator.WaitingForOtherPlayersLockin, sumName), ConsoleColor.White);

                                        //Select Summoners Spell
                                        int spellOneId;
                                        int spellTwoId;
                                        if (!Program.randomSpell)
                                        {
                                            spellOneId = Enums.GetSpell(Program.spell1);
                                            spellTwoId = Enums.GetSpell(Program.spell2);
                                        }
                                        else
                                        {
                                            Random random = new Random();
                                            List<int> list = new List<int>()
                                            {
                                                6, 14, 7, 12, 3, 21, 4, 13, 1
                                            };
                                            int index = random.Next(list.Count);
                                            int index2 = random.Next(list.Count);
                                            int num = list[index];
                                            int num2 = list[index2];
                                            if (num == num2)
                                            {
                                                int index3 = random.Next(list.Count);
                                                num2 = list[index3];
                                            }
                                            spellOneId = Convert.ToInt32(num);
                                            spellTwoId = Convert.ToInt32(num2);
                                            random = null;
                                            list = null;
                                        }
                                        await Task.Delay(new Random().Next(1, 9) * new Random().Next(800, 1000));
                                        try
                                        {
                                            await connection.SelectSpells(spellOneId, spellTwoId);
                                        }
                                        catch (Exception e)
                                        {
                                            Tools.Log(e.StackTrace);
                                        }

                                        try
                                        {
                                            SetMasteries(loginPacket.AllSummonerData.SummonerLevel.Level, UcFirst(championName.Replace("'", "").Replace(" ", "")));
                                        }
                                        catch(Exception e)
                                        {
                                            Tools.Log(e.StackTrace);
                                        }
                                        
                                        ChampList = null;
                                        championName = null;
                                    }

                                    #endregion Not Aram

                                    #region ARAM

                                    if (queueType == "ARAM" || queueType == "ARAM_UNRANKED_1x1" || queueType == "ARAM_UNRANKED_2x2" || (queueType == "ARAM_UNRANKED_3x3" || queueType == "ARAM_UNRANKED_5x5") || queueType == "ARAM_UNRANKED_6x6")
                                    {
                                        var championName = "";
                                        var champion = gameDTO.PlayerChampionSelections.FirstOrDefault(x => x.SummonerInternalName.ToLower() == sumName.ToLower().Replace(" ", ""));
                                        if (champion != null)
                                        {
                                            championName = Enums.GetChampionById(champion.ChampionId);
                                            if (!string.IsNullOrEmpty(championName))
                                            {
                                                Tools.ConsoleMessage(string.Format(Program.Translator.SelectedChampion, UcFirst(championName.ToLower()), sumName), ConsoleColor.DarkYellow);
                                            }
                                        }

                                        int Spell1;
                                        int Spell2;
                                        if (!Program.randomSpell)
                                        {
                                            Spell1 = Enums.GetSpell(Program.spell1);
                                            Spell2 = Enums.GetSpell(Program.spell2);
                                        }
                                        else
                                        {
                                            var random = new Random();
                                            var spellList = new List<int>
                                            {
                                                6, 14, 7, 12, 3, 21, 4, 13, 1
                                            };

                                            int index = random.Next(spellList.Count);
                                            int index2 = random.Next(spellList.Count);

                                            int randomSpell1 = spellList[index];
                                            int randomSpell2 = spellList[index2];

                                            if (randomSpell1 == randomSpell2)
                                            {
                                                int index3 = random.Next(spellList.Count);
                                                randomSpell2 = spellList[index3];
                                            }

                                            Spell1 = Convert.ToInt32(randomSpell1);
                                            Spell2 = Convert.ToInt32(randomSpell2);
                                        }

                                        try
                                        {
                                            await Task.Delay(new Random().Next(1, 9) * new Random().Next(800, 1000));
                                            await connection.SelectSpells(Spell1, Spell2);
                                            await connection.ChampionSelectCompleted();
                                            await Task.Delay(new Random().Next(1, 9) * new Random().Next(800, 1000));
                                        }
                                        catch (Exception ex)
                                        {
                                            Tools.Log(ex.StackTrace);
                                        }
                                        
                                        try
                                        {
                                            SetMasteries(loginPacket.AllSummonerData.SummonerLevel.Level, UcFirst(championName.Replace("'", "").Replace(" ", "")));
                                        }
                                        catch (Exception e)
                                        {
                                            Tools.Log(e.StackTrace);
                                        }
                                        
                                        Tools.ConsoleMessage(string.Format(Program.Translator.WaitingForOtherPlayersLockin, sumName), ConsoleColor.White);
                                    }

                                    #endregion ARAM
                                }
                            }
                            break;

                            case "POST_CHAMP_SELECT":
                            {
                                firstTimeInLobby = false;
                                if (firstTimeInPostChampSelect)
                                {
                                    firstTimeInPostChampSelect = false;
                                    Tools.ConsoleMessage(string.Format(Program.Translator.WaitingChampSelectTimer, sumName), ConsoleColor.White);
                                }
                            }
                            break;

                            case "PRE_CHAMP_SELECT":
                            break;

                            case "START_REQUESTED":
                            {
                                ShouldBeInGame = true;
                            }
                            break;

                            case "GAME_START_CLIENT":
                            break;

                            case "GameClientConnectedToServer":
                            break;

                            case "IN_PROGRESS":
                            break;

                            case "IN_QUEUE":
                            {
                                Tools.ConsoleMessage(string.Format(Program.Translator.YouAreInQueue, sumName), ConsoleColor.White);
                            }
                            break;

                            case "POST_GAME":
                            break;

                            case "TERMINATED":
                            {
                                if (Program.DontQueue) return;
                                pickAtTurn = 0;
                                Tools.ConsoleMessage(string.Format(Program.Translator.ReQueued, queueType, sumName), ConsoleColor.Cyan);
                                firstTimeInQueuePop = true;
                                firstTimeInPostChampSelect = true;
                                //ShouldBeInGame = false;
                                if (!Program.queueWithFriends)
                                {
                                    if (!IsInQueue)
                                    {
                                        new Thread(async () =>
                                        {
                                            await Task.Delay(1000 * 3);
                                            if (!IsInQueue) EnterQueue();
                                        }).Start();
                                    }
                                }
                                else
                                {
                                    if (m_isLeader)
                                    {
                                        sendGameInvites();
                                    }
                                }
                            }
                            break;

                            case "TERMINATED_IN_ERROR":
                            break;

                            case "CHAMP_SELECT_CLIENT":
                            //ShouldBeInGame = true;
                            break;

                            case "GameReconnect":
                            break;

                            case "GAME_IN_PROGRESS":
                            break;

                            case "JOINING_CHAMP_SELECT":
                            {
                                if (firstTimeInQueuePop)
                                {
                                    if (gameDTO.StatusOfParticipants.Contains("1"))
                                    {
                                        Tools.ConsoleMessage(string.Format(Program.Translator.QueuePopped, sumName), ConsoleColor.White);
                                        firstTimeInQueuePop = false;
                                        firstTimeInLobby = true;
                                        await Task.Delay(new Random().Next(1, Program.queueWithFriends ? (GetFriendsToInvite().ToList().Count > 3 ? 2 : 3) : 3) * new Random().Next(800, 1000));
                                        Tools.ConsoleMessage(string.Format(Program.Translator.AcceptedQueue, sumName), ConsoleColor.White);
                                        try
                                        {
                                            await connection.AcceptPoppedGame(true);
                                            ShouldBeInGame = false;
                                        }
                                        catch (InvocationException ex)
                                        {
                                            Tools.Log(ex.StackTrace);
                                            Tools.ConsoleMessage("Fault Code: " + ex.FaultCode + " Fault String" + ex.FaultString + " Fault Detail:" + ex.FaultDetail + "Root Cause:" + ex.RootCause, ConsoleColor.White);
                                            firstTimeInQueuePop = true;
                                            firstTimeInPostChampSelect = true;
                                            firstTimeInLobby = false;
                                            Tools.ConsoleMessage(string.Format(Program.Translator.ReQueued, queueType, sumName), ConsoleColor.Cyan);
                                        }
                                        catch (InvalidCastException ex)
                                        {
                                            Tools.Log(ex.StackTrace);
                                            Tools.ConsoleMessage(ex.StackTrace, ConsoleColor.Red);
                                        }
                                        catch (Exception ex)
                                        {
                                            Tools.Log(ex.StackTrace);
                                            Tools.ConsoleMessage(ex.StackTrace, ConsoleColor.Red);
                                        }
                                    }
                                }
                            }
                            break;

                            case "WAITING":
                            break;

                            case "DISCONNECTED":
                            break;

                            case "LEAVER_BUSTED":
                            {
                                Tools.ConsoleMessage(Program.Translator.YouHaveLeaverBuster, ConsoleColor.White);
                            }
                            break;

                            default:
                            break;
                        }
                    }
                    gameDTO = null;
                    gameState = null;
                }

                #endregion GameDTO

                #region PlayerCredentials

                else if (message is PlayerCredentialsDto)
                {
                    try
                    {
                        if (GameStartedAt == null)
                        {
                            GameStartedAt = DateTime.Now;
                            Program.GameStarted();
                        }
                        firstTimeInPostChampSelect = true;
                        PlayerCredentialsDto playerCredentialsDto = message as PlayerCredentialsDto;
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.CreateNoWindow = false;
                        startInfo.WorkingDirectory = FindLoLExe();
                        //Console.WriteLine(FindLoLExe() + "League of Legends.exe");
                        startInfo.FileName = "League of Legends.exe";
                        startInfo.Arguments = "\"8394\" \"LoLLauncher.exe\" \"\" \"" + playerCredentialsDto.ServerIp + " " + playerCredentialsDto.ServerPort + " " + playerCredentialsDto.EncryptionKey + " " + loginPacket.AllSummonerData.Summoner.SummonerId + "\"";
                        new Thread(() =>
                        {
                            try
                            {
                                exeProcess = Process.Start(startInfo);
                                exeProcess.Exited += exeProcess_Exited;
                                do
                                {
                                }
                                while (exeProcess.MainWindowHandle == IntPtr.Zero);
                                exeProcess.PriorityClass = !Program.LOWPriority ? ProcessPriorityClass.High : ProcessPriorityClass.Idle;
                                exeProcess.EnableRaisingEvents = true;
                            }
                            catch (InvocationException ex)
                            {
                                Tools.Log(ex.StackTrace);
                            }
                        }).Start();
                        Tools.ConsoleMessage(string.Format(Program.Translator.LaunchingLeagueOfLegends, sumName), ConsoleColor.White);
                        playerCredentialsDto = null;
                        ShouldBeInGame = true;
                        IsInQueue = false;
                    }
                    catch (Exception ex)
                    {
                        Tools.Log(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                        ShouldBeInGame = false;
                        //
                    }
                }

                #endregion PlayerCredentials

                else
                {
                    if (message is GameNotification || message is SearchingForMatchNotification)
                    {
                        return;
                    }
                    if (message is EndOfGameStats)
                    {
                        var match = message as EndOfGameStats;
                        if (match.TeamPlayerParticipantStats != null && match.TeamPlayerParticipantStats.Count > 0)
                        {
                            var game = match.TeamPlayerParticipantStats.FirstOrDefault(x => x.SummonerName.ToLower() == sumName.ToLower().Replace(" ", ""));

                            if (game != null)
                            {
                                if (Program.printGameStats) foreach (var stat1 in game.Statistics) Tools.ConsoleMessage(stat1.StatTypeName + " = " + stat1.Value.ToString(), ConsoleColor.White);
                                var statWin = game.Statistics.FirstOrDefault(x => x.StatTypeName == "WIN");
                                bool win = statWin != null && statWin.Value == 1;

                                Program.AddGame(win);

                                var kills = 0;
                                var deaths = 0;
                                var assists = 0;

                                var k = game.Statistics.FirstOrDefault(x => x.StatTypeName == "CHAMPIONS_KILLED");
                                if (k != null) kills = (int)k.Value;

                                var d = game.Statistics.FirstOrDefault(x => x.StatTypeName == "NUM_DEATHS");
                                if (d != null) deaths = (int)d.Value;

                                var a = game.Statistics.FirstOrDefault(x => x.StatTypeName == "ASSISTS");
                                if (a != null) assists = (int)a.Value;

                                Tools.ConsoleMessage(string.Format("{4}: {0} - {1}/{2}/{3}", win ? "Victory" : "Defeat", kills, deaths, assists, sumName), ConsoleColor.Magenta);
                            }
                        }
                        ShouldBeInGame = false;
                        GameStartedAt = null;
                        if (exeProcess == null)
                        {
                            if (!Program.queueWithFriends)
                            {
                                if (!IsInQueue) EnterQueue();
                            }
                            else
                            {
                                if (m_isLeader)
                                {
                                    sendGameInvites();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (message is AsObject)
                        {
                            var temp = (AsObject)message;
                            //Console.WriteLine(temp.TypeName);
                            if (temp.TypeName.ToLower().Contains("eog") || (queueType == "INTRO_BOT" || queueType == "BEGINNER_BOT" || queueType == "MEDIUM_BOT" || queueType == "BOT_3X3") && temp.TypeName.ToLower().Contains("storeaccountbalancenotification"))
                            {
                                lock (locker)
                                {
                                    if (exeProcess == null) return;
                                    new Thread(async () =>
                                    {
                                        await CloseGame();
                                    }).Start();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Tools.Log(exc.StackTrace);
                //Tools.ConsoleMessage(exc.StackTrace, ConsoleColor.Red);
            }
        }

        private static string UcFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        private object locker = new object();

        private async Task CloseGame()
        {
            try
            {
                Program.GameTerminated();
                GameStartedAt = null;
                ShouldBeInGame = false;
                if (exeProcess == null) return;
                Tools.ConsoleMessage(string.Format(Program.Translator.ClosingGameClient, sumName), ConsoleColor.White);
                EndOfGameStats eog = new EndOfGameStats();
                connection_OnMessageReceived(this, eog);
                exeProcess.Exited -= new EventHandler(exeProcess_Exited);
                exeProcess.Kill();
                await Task.Delay(1000);
                if (exeProcess.Responding)
                {
                    var processInfo = new ProcessStartInfo()
                    {
                        Arguments = "/f /PID " + exeProcess.Id,
                        FileName = "taskkill.exe",
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    Process.Start(processInfo);
                }
                exeProcess = null;
                loginPacket = await connection.GetLoginDataPacketForUser();
                archiveSumLevel = sumLevel;
                sumLevel = loginPacket.AllSummonerData.SummonerLevel.Level;

                if (sumLevel == archiveSumLevel)
                    return;
                levelUp();
            }
            catch (Exception ex)
            {
                Tools.Log(ex.Message.ToString());
            }
            if (!Program.queueWithFriends)
            {
                if (!IsInQueue)
                {
                    new Thread(async () =>
                    {
                        await Task.Delay(1000 * 3);
                        if (!IsInQueue) EnterQueue();
                    }).Start();
                }
            }
            else
            {
                if (m_isLeader)
                {
                    sendGameInvites();
                }
            }
        }

        private async void EnterQueue()
        {
            if (IsInQueue)
                return;
            if (Program.DontQueue) return;

            IsInQueue = true;

            ShouldBeInGame = false;
            try
            {
                MatchMakerParams matchMakerParams = new MatchMakerParams();
                if (queueType == "INTRO_BOT")
                    matchMakerParams.BotDifficulty = "INTRO";
                else if (queueType == "BEGINNER_BOT")
                    matchMakerParams.BotDifficulty = "EASY";
                else if (queueType == "MEDIUM_BOT")
                    matchMakerParams.BotDifficulty = "MEDIUM";
                else if (queueType.ToUpper() == "BOT_3X3")
                {
                    matchMakerParams.BotDifficulty = "MEDIUM";
                }
                if (sumLevel == 3.0 && actualQueueType == "NORMAL_5X5")
                    queueType = actualQueueType;
                else if (sumLevel == 6.0 && actualQueueType == "ARAM")
                    queueType = actualQueueType;
                else if (sumLevel == 7.0 && actualQueueType == "NORMAL_3X3")
                    queueType = actualQueueType;
                else if (sumLevel == 10.0 && actualQueueType == "TT_HEXAKILL")
                    queueType = actualQueueType;
                else if (sumLevel == 30.0 && actualQueueType == "RANKED_SOLO_5X5")
                    queueType = actualQueueType;

                var queues = await connection.GetAvailableQueues();

                matchMakerParams.QueueIds = new int[1] { (int)Enum.Parse(typeof(QueueTypeId), queueType) };
                if (Lobby != null && Program.queueWithFriends)
                {
                    matchMakerParams.InvitationId = Lobby.InvitationID;
                    matchMakerParams.Team = Lobby.Members.Select(stats => Convert.ToInt32(stats.SummonerId)).ToList();
                }

                SearchingForMatchNotification searchingForMatchNotification = null;
                try
                {
                    if (Program.queueWithFriends)
                        searchingForMatchNotification = await connection.AttachTeamToQueue(matchMakerParams);
                    else
                        searchingForMatchNotification = await connection.AttachToQueue(matchMakerParams);
                }
                catch (RtmpSharp.Net.ClientDisconnectedException ex)
                {
                    Tools.Log(ex.StackTrace);
                    Console.WriteLine("Got an exception where we should not!");
                }
                catch (Exception ex)
                {
                    Tools.Log(ex.StackTrace);
                    Console.WriteLine(ex.StackTrace);
                }

                if (searchingForMatchNotification == null)
                {
                    return;
                }

                if (searchingForMatchNotification.PlayerJoinFailures == null)
                {
                    if (searchingForMatchNotification.JoinedQueues.Count == 0)
                    {
                        IsInQueue = false;
                    }
                    else
                    {
                        Tools.ConsoleMessage(string.Format(Program.Translator.InQueueAs, queueType, loginPacket.AllSummonerData.Summoner.Name), ConsoleColor.Cyan);
                        IsInQueue = true;
                    }
                }
                else
                {
                    foreach (FailedJoinPlayer playerJoinFailure in searchingForMatchNotification.PlayerJoinFailures)
                    {
                        IsInQueue = false;
                        FailedJoinPlayer failedJoinPlayer = playerJoinFailure;
                        Tools.ConsoleMessage(string.Format(Program.Translator.QueueFailedReason, failedJoinPlayer.ReasonFailed), ConsoleColor.Red);
                        if (failedJoinPlayer is BustedLeaver)
                        {
                            BustedLeaver current = failedJoinPlayer as BustedLeaver;
                            if (current.ReasonFailed == "LEAVER_BUSTED")
                            {
                                m_accessToken = current.AccessToken;
                                if (current.LeaverPenaltyMilisRemaining > m_leaverBustedPenalty)
                                    m_leaverBustedPenalty = current.LeaverPenaltyMilisRemaining;
                            }
                            else if (current.ReasonFailed == "LEAVER_BUSTER_TAINTED_WARNING")
                            {
                                try
                                {
                                    object obj1 = await connection.ackLeaverBusterWarning();
                                    object obj2 = await connection.callPersistenceMessaging(new SimpleDialogMessageResponse()
                                    {
                                        AccountId = loginPacket.AllSummonerData.Summoner.SummonerId,
                                        MessageId = loginPacket.AllSummonerData.Summoner.SummonerId.ToString(),
                                        Command = "ack"
                                    });
                                }
                                catch (Exception ex)
                                {
                                    Tools.ConsoleMessage(string.Format(Program.Translator.LeaverBusterTaintedWarningError, ex.StackTrace), ConsoleColor.Red);
                                }
                                connection_OnMessageReceived(null, new EndOfGameStats());
                            }
                            current = null;
                            failedJoinPlayer = null;
                        }
                        else if (failedJoinPlayer is QueueDodger)
                        {
                            QueueDodger current = failedJoinPlayer as QueueDodger;
                            if (current.ReasonFailed == "QUEUE_DODGER")
                            {
                                Tools.ConsoleMessage(string.Format(Program.Translator.WaitingDodgeTimer, (float)(current.PenaltyRemainingTime / 1000.0 / 60.0)), ConsoleColor.White);
                                await Task.Delay(TimeSpan.FromMilliseconds(current.PenaltyRemainingTime));
                                connection_OnMessageReceived(null, new EndOfGameStats());
                            }
                            current = null;
                            failedJoinPlayer = null;
                        }
                        else if (failedJoinPlayer.ReasonFailed == "LEAVER_BUSTER_TAINTED_WARNING")
                        {
                            try
                            {
                                object obj1 = await connection.ackLeaverBusterWarning();
                                object obj2 = await connection.callPersistenceMessaging(new SimpleDialogMessageResponse()
                                {
                                    AccountId = loginPacket.AllSummonerData.Summoner.SummonerId,
                                    MessageId = loginPacket.AllSummonerData.Summoner.SummonerId.ToString(),
                                    Command = "ack"
                                });
                            }
                            catch (Exception ex)
                            {
                                Tools.ConsoleMessage(string.Format(Program.Translator.LeaverBusterTaintedWarningError, ex.StackTrace), ConsoleColor.Red);
                            }
                            connection_OnMessageReceived(null, new EndOfGameStats());
                        }
                    }
                    //List<FailedJoinPlayer>.Enumerator enumerator = new List<FailedJoinPlayer>.Enumerator();
                    if (!string.IsNullOrEmpty(m_accessToken))
                    {
                        Tools.ConsoleMessage(string.Format(Program.Translator.WaitingLeaverTimer, (float)(m_leaverBustedPenalty / 1000.0 / 60.0)), ConsoleColor.White);
                        //Thread.Sleep(TimeSpan.FromMilliseconds(m_leaverBustedPenalty));
                        await Task.Delay(TimeSpan.FromMilliseconds(m_leaverBustedPenalty));
                        Dictionary<string, object> lbdic = new Dictionary<string, object>();
                        lbdic.Add("LEAVER_BUSTER_ACCESS_TOKEN", m_accessToken);
                        try
                        {
                            searchingForMatchNotification = await connection.AttachToQueue(matchMakerParams, new AsObject(lbdic));

                            if (searchingForMatchNotification.PlayerJoinFailures == null)
                            {
                                Tools.ConsoleMessage(string.Format(Program.Translator.JoinedLowPriorityQueue, loginPacket.AllSummonerData.Summoner.Name), ConsoleColor.Cyan);
                                IsInQueue = true;
                            }
                            else
                            {
                                Tools.ConsoleMessage(Program.Translator.ErrorJoiningLowPriorityQueue, ConsoleColor.White);
                                connection.Disconnect();
                                Thread.Sleep(500);
                                connection.ConnectAndLogin().Wait();
                            }
                        }
                        catch(Exception ex)
                        {
                        }
                        lbdic = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Tools.Log(ex.StackTrace);
                Tools.ConsoleMessage(string.Format(Program.Translator.ErrorOccured, ex.StackTrace), ConsoleColor.Red);
                connection.ConnectAndLogin().Wait();
            }
        }

        private string FindLoLExe()
        {
            if (ipath.Contains("notfound"))
                return ipath;
            if(useGarena)
            {
                return ipath + "GAME\\";
            }
            else
            return Directory.EnumerateDirectories(ipath + "RADS\\solutions\\lol_game_client_sln\\releases\\").OrderBy(f => new DirectoryInfo(f).CreationTime).Last() + "\\deploy\\";
        }

        private async void RestartLeague()
        {
            var found = false;
            while (!found)
            {
                if (GameStartedAt == null)// || exeProcess != null)
                {
                    found = true;
                    break;
                }
                var ellapsedTime = DateTime.Now.Subtract(GameStartedAt.Value).Minutes;
                if (ellapsedTime >= 0)
                {
                    try
                    {
                        loginPacket = await connection.GetLoginDataPacketForUser();
                        if (loginPacket.ReconnectInfo != null || ((PlatformGameLifecycleDTO)loginPacket.ReconnectInfo).Game != null)
                        {
                            Tools.ConsoleMessage(string.Format(Program.Translator.RestartingLeagueOfLegends, sumName), ConsoleColor.White);
                            connection_OnMessageReceived(this, ((PlatformGameLifecycleDTO)loginPacket.ReconnectInfo).PlayerCredentials);
                        }
                        else
                        {
                            connection_OnMessageReceived(this, new EndOfGameStats());
                            ShouldBeInGame = false;
                        }
                        found = true;
                        break;
                    }
                    catch (InvocationException ex)
                    {
                        Tools.Log(ex.StackTrace);
                        //Console.WriteLine("Got ClientDisconnected exception after the game has quit.");
                        //connection.ConnectAndLogin().Wait();
                    }
                    catch (NullReferenceException ex)
                    {
                        Tools.Log(ex.StackTrace);
                        //Console.WriteLine("Got ClientDisconnected exception after the game has quit.");
                        if (ShouldBeInGame)
                            connection.ConnectAndLogin().Wait();
                    }
                    catch (RtmpSharp.Net.ClientDisconnectedException ex)
                    {
                        Tools.Log(ex.StackTrace);
                        //Console.WriteLine("Got ClientDisconnected exception after the game has quit.");
                        connection.ConnectAndLogin().Wait();
                    }
                }
                Thread.Sleep(1000);
            }
        }

        private async void exeProcess_Exited(object sender, EventArgs e)
        {
            try
            {
                exeProcess = null;
                loginPacket = await connection.GetLoginDataPacketForUser();
                if (loginPacket.ReconnectInfo != null || ((PlatformGameLifecycleDTO)loginPacket.ReconnectInfo).Game != null)
                {
                    var ellapsedTime = DateTime.Now.Subtract(GameStartedAt.Value).Minutes;

                    if (ellapsedTime >= 0)
                    {
                        Tools.ConsoleMessage(string.Format(Program.Translator.RestartingLeagueOfLegends, sumName), ConsoleColor.White);
                        connection_OnMessageReceived(sender, ((PlatformGameLifecycleDTO)loginPacket.ReconnectInfo).PlayerCredentials);
                    }
                    else
                    {
                        Tools.ConsoleMessage(string.Format(Program.Translator.RestartingLeagueOfLegendsAt, GameStartedAt.Value.AddMinutes(1).ToLongTimeString(), sumName), ConsoleColor.White);
                        new Thread(RestartLeague).Start();
                    }
                }
                else
                {
                    ShouldBeInGame = false;
                    connection_OnMessageReceived(sender, new EndOfGameStats());
                }
            }
            catch (InvocationException ex)
            {
                Tools.Log(ex.StackTrace);
                //Console.WriteLine("Got ClientDisconnected exception after the game has quit.");
                //connection.ConnectAndLogin().Wait();
            }
            catch (NullReferenceException ex)
            {
                Tools.Log(ex.StackTrace);
                //Console.WriteLine("Got ClientDisconnected exception after the game has quit.");
                if (ShouldBeInGame)
                    connection.ConnectAndLogin().Wait();
            }
            catch (RtmpSharp.Net.ClientDisconnectedException ex)
            {
                Tools.Log(ex.StackTrace);
                //Console.WriteLine("Got ClientDisconnected exception after the game has quit.");
                connection.ConnectAndLogin().Wait();
            }
        }

        private void connection_OnLoginQueueUpdate(int positionInLine)
        {
            if (positionInLine <= 0) return;
            Tools.ConsoleMessage(string.Format(Program.Translator.PositionInLoginQueue, positionInLine), ConsoleColor.White);
        }

        private async void connection_OnLogin(string username)
        {
            try
            {
                await InitializeLogin();
            }
            catch (RtmpSharp.Net.ClientDisconnectedException ex)
            {
                Tools.Log(ex.StackTrace);
                //Console.WriteLine("Got ClientDisconnected exception after the game has quit.");
                connection.ConnectAndLogin().Wait();
            }
        }

        private async Task InitializeLogin()
        {
            Tools.ConsoleMessage(Program.Translator.LoggingIntoAccount, ConsoleColor.White);
            loginPacket = await connection.GetLoginDataPacketForUser();
            AvailableChampions = await connection.GetAvailableChampions();

            if (loginPacket.AllSummonerData == null)
            {
                Tools.ConsoleMessage(Program.Translator.SummonerDoesntExist, ConsoleColor.Red);
                Tools.ConsoleMessage(Program.Translator.CreatingSummoner, ConsoleColor.Red);
                Random random = new Random();
                string text = Accountname;
                if (text.Length > 16) text = text.Substring(0, 12) + new Random().Next(1000, 9999).ToString();
                loginPacket.AllSummonerData = await connection.CreateDefaultSummoner(text);
                Tools.ConsoleMessage(string.Format(Program.Translator.CreatedSummoner, text), ConsoleColor.White);
                text = null;
            }
            sumLevel = loginPacket.AllSummonerData.SummonerLevel.Level;
            sumName = loginPacket.AllSummonerData.Summoner.Name;
            sumId = loginPacket.AllSummonerData.Summoner.SummonerId;

            double summonerId = loginPacket.AllSummonerData.Summoner.SummonerId;
            rpBalance = loginPacket.RpBalance;
            ipBalance = loginPacket.IpBalance;
            if (sumLevel >= Program.maxLevel)
            {
                Tools.ConsoleMessage(string.Format(Program.Translator.AlreadyMaxLevel, sumName), ConsoleColor.White);
                connection.Disconnect();
                Tools.ConsoleMessage(Program.Translator.LogIntoNewAccount, ConsoleColor.White);
                Program.LognNewAccount();
            }
            else
            {
                if (rpBalance >= 400.0 && Program.buyExpBoost)
                {
                    Tools.ConsoleMessage(Program.Translator.BuyingXpBoost, ConsoleColor.White);
                    try
                    {
                        Task task = new Task(new Action(buyBoost));
                        task.Start();
                        task = null;
                    }
                    catch (Exception ex)
                    {
                        Tools.Log(ex.StackTrace);
                        Tools.ConsoleMessage(string.Format(Program.Translator.CouldntBuyBoost, ex.Message.ToString()), ConsoleColor.White);
                    }
                }
                var queueLevel = await GetQueueLevel();
                if (queueLevel < 3.0 && queueType == "NORMAL_5X5")
                {
                    Tools.ConsoleMessage(Program.Translator.Normal5Requirements, ConsoleColor.White);
                    Tools.ConsoleMessage(string.Format(Program.Translator.JoinCoopBeginnerUntil, 3), ConsoleColor.White);
                    queueType = "BEGINNER_BOT";
                    actualQueueType = "NORMAL_5X5";
                }
                else if (queueLevel < 6.0 && queueType == "ARAM")
                {
                    Tools.ConsoleMessage(Program.Translator.NeedLevel6BeforeAram, ConsoleColor.White);
                    Tools.ConsoleMessage(string.Format(Program.Translator.JoinCoopBeginnerUntil, 6), ConsoleColor.White);
                    queueType = "BEGINNER_BOT";
                    actualQueueType = "ARAM";
                }
                else if (queueLevel < 7.0 && queueType == "NORMAL_3X3")
                {
                    Tools.ConsoleMessage(Program.Translator.NeedLevel7Before3v3, ConsoleColor.White);
                    Tools.ConsoleMessage(string.Format(Program.Translator.JoinCoopBeginnerUntil, 7), ConsoleColor.White);
                    queueType = "BEGINNER_BOT";
                    actualQueueType = "NORMAL_3X3";
                }
                
                Tools.ConsoleMessage(string.Format(Program.Translator.Welcome, loginPacket.AllSummonerData.Summoner.Name, loginPacket.AllSummonerData.SummonerLevel.Level, ipBalance.ToString(), loginPacket.AllSummonerData.SummonerLevelAndPoints.ExpPoints, loginPacket.AllSummonerData.SummonerLevel.ExpToNextLevel), ConsoleColor.White);
                try
                {
                    PlayerDto player = await connection.CreatePlayer();
                    
                    if (loginPacket.ReconnectInfo != null && ((PlatformGameLifecycleDTO)loginPacket.ReconnectInfo).Game != null)
                    {
                        connection_OnMessageReceived(this, ((PlatformGameLifecycleDTO)loginPacket.ReconnectInfo).PlayerCredentials);
                    }
                    else
                    {
                        try
                        {
                            await connection.DestroyGroupFinderLobby();
                        }
                        catch (Exception ex)
                        {
                            Tools.Log(ex.StackTrace);
                        }
                        if (Program.queueWithFriends)
                        {
                            if (m_isLeader)
                            {
                                Tools.ConsoleMessage(Program.Translator.SendingGameInvites, ConsoleColor.Cyan);
                                sendGameInvites();
                            }
                            else
                            {
                                Tools.ConsoleMessage(string.Format(Program.Translator.WaitingGameInviteFrom, Program.leaderName), ConsoleColor.Cyan);
                            }
                        }
                        else
                        {
                            connection_OnMessageReceived(this, new EndOfGameStats());
                        }
                    }
                }
                catch(Exception ex)
                {
                    Tools.Log(ex.Message);
                    Tools.ConsoleMessage(ex.Message, ConsoleColor.Red);
                }
            }
        }

        private async void sendGameInvites()
        {
            try
            {
                if (queueType == "INTRO_BOT" || queueType == "BEGINNER_BOT")
                {
                    var lobby = await connection.CreateArrangedBotTeamLobby(GetGameModeId(), "EASY");
                }
                else if (queueType == "MEDIUM_BOT" || queueType == "BOT_3x3")
                {
                    var lobby = await connection.CreateArrangedBotTeamLobby(GetGameModeId(), "MEDIUM");
                }
                else
                {
                    var lobby = await connection.CreateArrangedTeamLobby(GetGameModeId());
                }
                //1
                try
                {
                    if (!string.IsNullOrEmpty(Program.firstFriend))
                    {
                        var summoner = await connection.GetSummonerByName(Program.firstFriend);
                        var summonerData = await connection.GetAllPublicSummonerDataByAccount(summoner.AccountId);
                        var invitation = await connection.InvitePlayer(summonerData.Summoner.SumId);
                        await Task.Delay(500);
                        Program.OnInvite?.Invoke(sumName, Program.firstFriend.ToLower(), Lobby.InvitationID);
                    }
                }
                catch (Exception ex)
                {
                    Tools.Log(ex.StackTrace);
                    Program.firstFriend = null;
                }
                //2
                try
                {
                    if (!string.IsNullOrEmpty(Program.secondFriend))
                    {
                        var summoner = await connection.GetSummonerByName(Program.secondFriend);
                        var summonerData = await connection.GetAllPublicSummonerDataByAccount(summoner.AccountId);
                        InvitationRequest invitation = await connection.InvitePlayer(summonerData.Summoner.SumId) as InvitationRequest;
                        Program.OnInvite?.Invoke(sumName, Program.secondFriend.ToLower(), Lobby.InvitationID);
                    }
                }
                catch (Exception ex)
                {
                    Tools.Log(ex.StackTrace);
                    Program.secondFriend = null;
                }
                //3
                try
                {
                    if (!string.IsNullOrEmpty(Program.thirdFriend))
                    {
                        var summoner = await connection.GetSummonerByName(Program.thirdFriend);
                        var summonerData = await connection.GetAllPublicSummonerDataByAccount(summoner.AccountId);
                        InvitationRequest invitation = await connection.InvitePlayer(summonerData.Summoner.SumId) as InvitationRequest;
                        Program.OnInvite?.Invoke(sumName, Program.thirdFriend.ToLower(), Lobby.InvitationID);
                    }
                }
                catch (Exception ex)
                {
                    Tools.Log(ex.StackTrace);
                    Program.thirdFriend = null;
                }
                //4
                try
                {
                    if (!string.IsNullOrEmpty(Program.fourthFriend))
                    {
                        var summoner = await connection.GetSummonerByName(Program.fourthFriend);
                        var summonerData = await connection.GetAllPublicSummonerDataByAccount(summoner.AccountId);
                        InvitationRequest invitation = await connection.InvitePlayer(summonerData.Summoner.SumId) as InvitationRequest;
                        Program.OnInvite?.Invoke(sumName, Program.fourthFriend.ToLower(), Lobby.InvitationID);
                    }
                }
                catch (Exception ex)
                {
                    Tools.Log(ex.StackTrace);
                    Program.fourthFriend = null;
                }
            }
            catch (InvocationException ex)
            {
                Tools.ConsoleMessage(ex.StackTrace, ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                Tools.ConsoleMessage(ex.StackTrace, ConsoleColor.Red);
            }
        }

        public async Task<double> GetQueueLevel()
        {
            double level = 30;
            if (Program.queueWithFriends)
            {
                //1
                try
                {
                    if (!string.IsNullOrEmpty(Program.firstFriend))
                    {
                        var summoner = await connection.GetSummonerByName(Program.firstFriend);
                        var summonerData = await connection.GetAllPublicSummonerDataByAccount(summoner.AccountId);

                        if (summonerData.SummonerLevel.Level < level)
                            level = summonerData.SummonerLevel.Level;
                    }
                }
                catch (Exception ex)
                {
                    Tools.Log(ex.StackTrace);
                    Program.firstFriend = null;
                }
                //2
                try
                {
                    if (!string.IsNullOrEmpty(Program.secondFriend))
                    {
                        var summoner = await connection.GetSummonerByName(Program.secondFriend);
                        var summonerData = await connection.GetAllPublicSummonerDataByAccount(summoner.AccountId);

                        if (summonerData.SummonerLevel.Level < level)
                            level = summonerData.SummonerLevel.Level;
                    }
                }
                catch (Exception ex)
                {
                    Tools.Log(ex.StackTrace);
                    Program.secondFriend = null;
                }
                //3
                try
                {
                    if (!string.IsNullOrEmpty(Program.thirdFriend))
                    {
                        var summoner = await connection.GetSummonerByName(Program.thirdFriend);
                        var summonerData = await connection.GetAllPublicSummonerDataByAccount(summoner.AccountId);

                        if (summonerData.SummonerLevel.Level < level)
                            level = summonerData.SummonerLevel.Level;
                    }
                }
                catch (Exception ex)
                {
                    Tools.Log(ex.StackTrace);
                    Program.thirdFriend = null;
                }
                //4
                try
                {
                    if (!string.IsNullOrEmpty(Program.fourthFriend))
                    {
                        var summoner = await connection.GetSummonerByName(Program.fourthFriend);
                        var summonerData = await connection.GetAllPublicSummonerDataByAccount(summoner.AccountId);

                        if (summonerData.SummonerLevel.Level < level)
                            level = summonerData.SummonerLevel.Level;
                    }
                }
                catch (Exception ex)
                {
                    Tools.Log(ex.StackTrace);
                    Program.fourthFriend = null;
                }
            }
            //Me
            if (sumLevel < level)
                level = sumLevel;

            return level;
        }

        public List<string> GetFriendsToInvite()
        {
            var list = new List<string>();
            if (!string.IsNullOrEmpty(Program.firstFriend))
            {
                list.Add(Program.firstFriend.ToLower());
            }
            if (!string.IsNullOrEmpty(Program.secondFriend))
            {
                list.Add(Program.secondFriend.ToLower());
            }
            if (!string.IsNullOrEmpty(Program.thirdFriend))
            {
                list.Add(Program.thirdFriend.ToLower());
            }
            if (!string.IsNullOrEmpty(Program.fourthFriend))
            {
                list.Add(Program.fourthFriend.ToLower());
            }
            return list;
        }

        public int GetGameModeId()
        {
            if (sumLevel == 3.0 && actualQueueType == "NORMAL_5X5")
                queueType = actualQueueType;
            else if (sumLevel == 6.0 && actualQueueType == "ARAM")
                queueType = actualQueueType;
            else if (sumLevel == 7.0 && actualQueueType == "NORMAL_3X3")
                queueType = actualQueueType;
            else if (sumLevel == 10.0 && actualQueueType == "TT_HEXAKILL")
                queueType = actualQueueType;
            return (int)Enum.Parse(typeof(QueueTypeId), queueType);
        }

        private void connection_OnError(Error error)
        {
            if (error.Message.Contains("is not owned by summoner"))
                return;
            if (error.Message.Contains("Your summoner level is too low to select the spell"))
            {
                var random = new Random();
                var spellList = new List<int> { 13, 6, 7, 10, 1, 11, 21, 12, 3, 14, 2, 4 };

                int index = random.Next(spellList.Count);
                int index2 = random.Next(spellList.Count);

                int randomSpell1 = spellList[index];
                int randomSpell2 = spellList[index2];

                if (randomSpell1 == randomSpell2)
                {
                    int index3 = random.Next(spellList.Count);
                    randomSpell2 = spellList[index3];
                }

                int Spell1 = Convert.ToInt32(randomSpell1);
                int Spell2 = Convert.ToInt32(randomSpell2);
                return;
            }
            else
                Tools.ConsoleMessage(string.Format(Program.Translator.ErrorOccured, error.Message), ConsoleColor.White);
        }

        private void connection_OnDisconnect(object sender, EventArgs e)
        {
            Console.Title = "ezBot - Offline";
            Tools.ConsoleMessage(Program.Translator.Disconnected, ConsoleColor.White);
        }

        private void connection_OnConnect(object sender, EventArgs e)
        {
            Console.Title = "ezBot - Online";
        }

        private async void buyBoost()
        {
            try
            {
                if (region == "EUW")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);

                    string storeURL = "https://store.euw1.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);

                    string purchaseURL = "https://store.euw1.lol.riotgames.com/store/purchase/item";

                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);

                    Tools.ConsoleMessage(Program.Translator.BoughtXpBoost3Days, ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (region == "EUNE")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);

                    string storeURL = "https://store.eun1.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);

                    string purchaseURL = "https://store.eun1.lol.riotgames.com/store/purchase/item";

                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);

                    Tools.ConsoleMessage(Program.Translator.BoughtXpBoost3Days, ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (region == "NA")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);

                    string storeURL = "https://store.na2.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);

                    string purchaseURL = "https://store.na2.lol.riotgames.com/store/purchase/item";

                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);

                    Tools.ConsoleMessage(Program.Translator.BoughtXpBoost3Days, ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (region == "KR")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);

                    string storeURL = "https://store.kr.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);

                    string purchaseURL = "https://store.kr.lol.riotgames.com/store/purchase/item";

                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);

                    Tools.ConsoleMessage(Program.Translator.BoughtXpBoost3Days, ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (region == "BR")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);

                    string storeURL = "https://store.br.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);

                    string purchaseURL = "https://store.br.lol.riotgames.com/store/purchase/item";

                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);

                    Tools.ConsoleMessage(Program.Translator.BoughtXpBoost3Days, ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (region == "RU")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);

                    string storeURL = "https://store.ru.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);

                    string purchaseURL = "https://store.ru.lol.riotgames.com/store/purchase/item";

                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);

                    Tools.ConsoleMessage(Program.Translator.BoughtXpBoost3Days, ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (region == "TR")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);

                    string storeURL = "https://store.tr.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);

                    string purchaseURL = "https://store.tr.lol.riotgames.com/store/purchase/item";

                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);

                    Tools.ConsoleMessage(Program.Translator.BoughtXpBoost3Days, ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (region == "LAS")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);

                    string storeURL = "https://store.la2.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);

                    string purchaseURL = "https://store.la2.lol.riotgames.com/store/purchase/item";

                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);

                    Tools.ConsoleMessage(Program.Translator.BoughtXpBoost3Days, ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (region == "LAN")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);

                    string storeURL = "https://store.la1.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);

                    string purchaseURL = "https://store.la1.lol.riotgames.com/store/purchase/item";

                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);

                    Tools.ConsoleMessage(Program.Translator.BoughtXpBoost3Days, ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (region == "OCE")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);

                    string storeURL = "https://store.oc1.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);

                    string purchaseURL = "https://store.oc1.lol.riotgames.com/store/purchase/item";

                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);

                    Tools.ConsoleMessage(Program.Translator.BoughtXpBoost3Days, ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (region == "JP")
                {
                    string url = await connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    await httpClient.GetStringAsync(url);

                    string storeURL = "https://store.jp1.lol.riotgames.com/store/tabs/view/boosts/1";
                    await httpClient.GetStringAsync(storeURL);

                    string purchaseURL = "https://store.jp1.lol.riotgames.com/store/purchase/item";

                    List<KeyValuePair<string, string>> storeItemList = new List<KeyValuePair<string, string>>();
                    storeItemList.Add(new KeyValuePair<string, string>("item_id", "boosts_2"));
                    storeItemList.Add(new KeyValuePair<string, string>("currency_type", "rp"));
                    storeItemList.Add(new KeyValuePair<string, string>("quantity", "1"));
                    storeItemList.Add(new KeyValuePair<string, string>("rp", "260"));
                    storeItemList.Add(new KeyValuePair<string, string>("ip", "null"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration_type", "PURCHASED"));
                    storeItemList.Add(new KeyValuePair<string, string>("duration", "3"));
                    HttpContent httpContent = new FormUrlEncodedContent(storeItemList);
                    await httpClient.PostAsync(purchaseURL, httpContent);

                    Tools.ConsoleMessage(Program.Translator.BoughtXpBoost3Days, ConsoleColor.White);
                    httpClient.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void levelUp()
        {
            Tools.ConsoleMessage(string.Format(Program.Translator.LevelUp, sumLevel, sumName), ConsoleColor.Yellow);
            rpBalance = loginPacket.RpBalance;
            ipBalance = loginPacket.IpBalance;
            Tools.ConsoleMessage(string.Format(Program.Translator.CurrentRp, rpBalance), ConsoleColor.Yellow);
            Tools.ConsoleMessage(string.Format(Program.Translator.CurrentIp, ipBalance), ConsoleColor.Yellow);
            if (sumLevel >= Program.maxLevel)
            {
                Tools.ConsoleMessage(string.Format(Program.Translator.CharacterReachedMaxLevel, Program.maxLevel), ConsoleColor.Red);
                connection.Disconnect();
                return;
            }
            try
            {
                if (rpBalance >= 400.0 && Program.buyExpBoost)
                {
                    Tools.ConsoleMessage(Program.Translator.BuyingXpBoost, ConsoleColor.White);
                    try
                    {
                        Task t = new Task(buyBoost);
                        t.Start();
                    }
                    catch (Exception exception)
                    {
                        Tools.ConsoleMessage(string.Format(Program.Translator.CouldntBuyBoost, exception), ConsoleColor.Red);
                    }
                }
            }
            catch(Exception ex)
            {
                Tools.Log(ex.StackTrace);
            }
        }

        private async void SetMasteries(int level, string championName)
        {
            bool updatedFromChampionGG = false;
            try
            {
                Tools.ConsoleMessage(string.Format(Program.Translator.DownloadingMasteries, sumName), ConsoleColor.White);
                List<MasteriesTreeGG> masteries = new List<MasteriesTreeGG>();
                using (WebClient webClient = new WebClient())
                {
                    var json = webClient.DownloadString("http://api.champion.gg/champion/" + championName + "/?api_key=48fa37068e223a93d88868c4c8b4909a");
                    json = json.Remove(0, 1);
                    json = json.Remove(json.Length - 1, 1);

                    var test = JObject.Parse(json);
                    
                    foreach (var masteryTree in test["masteries"]["highestWinPercent"]["masteries"])
                    {
                        var mastery = new MasteriesTreeGG()
                        {
                            tree = masteryTree["tree"].Value<string>(),
                            total = masteryTree["total"].Value<int>(),
                            data = new List<MasteryGG>()
                        };
                        foreach (var masteryData in masteryTree["data"])
                        {
                            mastery.data.Add(new MasteryGG()
                            {
                                mastery = masteryData["mastery"].Value<int>(),
                                points = masteryData["points"].Value<int>()
                            });
                        }
                        mastery.data = mastery.data.OrderBy(o => o.mastery).ToList();

                        masteries.Add(mastery);
                    }
                    masteries = masteries.OrderByDescending(o => o.total).ToList();
                }

                if(masteries.Count != 0)
                {
                    var masteryBook = await connection.GetMasteryBook(sumId);
                    var firstPage = masteryBook.BookPages.First();

                    if (firstPage == null)
                    {
                        masteryBook.BookPages.Add(new MasteryBookPageDTO()
                        {
                            Current = true,
                            Name = "Generic",
                            SummonerId = sumId,
                            TalentEntries = new List<TalentEntry>()
                        });
                        firstPage = masteryBook.BookPages.First();
                    }

                    firstPage.TalentEntries.Clear();

                    int masterySet = 0;
                    //while(masterySet != level)
                    {
                        foreach(var masteryTree in masteries)
                        {
                            if(masteryTree.total != 0)
                            {
                                foreach(var mastery in masteryTree.data)
                                {
                                    if(mastery.points != 0)
                                    {
                                        if (masterySet + mastery.points <= level)
                                        {
                                            firstPage.TalentEntries.Add(new TalentEntry()
                                            {
                                                Rank = mastery.points,
                                                SummonerId = -1,
                                                TalentId = mastery.mastery
                                            });
                                            masterySet += mastery.points;
                                        }
                                        else
                                        {
                                            switch (mastery.points)
                                            {
                                                case 5:
                                                {
                                                    if (masterySet + 4 <= level)
                                                    {
                                                        firstPage.TalentEntries.Add(new TalentEntry()
                                                        {
                                                            Rank = 4,
                                                            SummonerId = -1,
                                                            TalentId = mastery.mastery
                                                        });
                                                        masterySet += 4;
                                                    }
                                                    else if (masterySet + 3 <= level)
                                                    {
                                                        firstPage.TalentEntries.Add(new TalentEntry()
                                                        {
                                                            Rank = 3,
                                                            SummonerId = -1,
                                                            TalentId = mastery.mastery
                                                        });
                                                        masterySet += 3;
                                                    }
                                                    else if (masterySet + 2 <= level)
                                                    {
                                                        firstPage.TalentEntries.Add(new TalentEntry()
                                                        {
                                                            Rank = 2,
                                                            SummonerId = -1,
                                                            TalentId = mastery.mastery
                                                        });
                                                        masterySet += 2;
                                                    }
                                                    else if (masterySet + 1 <= level)
                                                    {
                                                        firstPage.TalentEntries.Add(new TalentEntry()
                                                        {
                                                            Rank = 1,
                                                            SummonerId = -1,
                                                            TalentId = mastery.mastery
                                                        });
                                                        masterySet += 1;
                                                    }
                                                }
                                                break;
                                                case 4:
                                                {
                                                    if (masterySet + 3 <= level)
                                                    {
                                                        firstPage.TalentEntries.Add(new TalentEntry()
                                                        {
                                                            Rank = 3,
                                                            SummonerId = -1,
                                                            TalentId = mastery.mastery
                                                        });
                                                        masterySet += 3;
                                                    }
                                                    else if (masterySet + 2 <= level)
                                                    {
                                                        firstPage.TalentEntries.Add(new TalentEntry()
                                                        {
                                                            Rank = 2,
                                                            SummonerId = -1,
                                                            TalentId = mastery.mastery
                                                        });
                                                        masterySet += 2;
                                                    }
                                                    else if (masterySet + 1 <= level)
                                                    {
                                                        firstPage.TalentEntries.Add(new TalentEntry()
                                                        {
                                                            Rank = 1,
                                                            SummonerId = -1,
                                                            TalentId = mastery.mastery
                                                        });
                                                        masterySet += 1;
                                                    }
                                                }
                                                break;
                                                case 3:
                                                {
                                                    if (masterySet + 2 <= level)
                                                    {
                                                        firstPage.TalentEntries.Add(new TalentEntry()
                                                        {
                                                            Rank = 2,
                                                            SummonerId = -1,
                                                            TalentId = mastery.mastery
                                                        });
                                                        masterySet += 2;
                                                    }
                                                    else if (masterySet + 1 <= level)
                                                    {
                                                        firstPage.TalentEntries.Add(new TalentEntry()
                                                        {
                                                            Rank = 1,
                                                            SummonerId = -1,
                                                            TalentId = mastery.mastery
                                                        });
                                                        masterySet += 1;
                                                    }
                                                }
                                                break;
                                                case 2:
                                                {
                                                    if (masterySet + 1 <= level)
                                                    {
                                                        firstPage.TalentEntries.Add(new TalentEntry()
                                                        {
                                                            Rank = 1,
                                                            SummonerId = -1,
                                                            TalentId = mastery.mastery
                                                        });
                                                        masterySet += 1;
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //Set our page as the current...
                    foreach (var page in masteryBook.BookPages)
                    {
                        page.Current = false;
                    }
                    firstPage.Current = true;
                    masteryBook.BookPages[0] = firstPage;
                    Tools.ConsoleMessage(string.Format(Program.Translator.UpdatingMasteries, sumName), ConsoleColor.White);
                    //save the page
                    var newBook = await connection.SaveMasteryBook(masteryBook);
                    updatedFromChampionGG = true;
                }
            }
            catch(Exception ex)
            {
                Tools.Log(ex.StackTrace);
            }

            if (updatedFromChampionGG) return;
            Tools.ConsoleMessage(string.Format(Program.Translator.UpdatingMasteries, sumName), ConsoleColor.White);
            try
            {
                var masteryBook = await connection.GetMasteryBook(sumId);
                var firstPage = masteryBook.BookPages.First();

                if (firstPage == null)
                {
                    masteryBook.BookPages.Add(new MasteryBookPageDTO()
                    {
                        Current = true,
                        Name = "Generic",
                        SummonerId = sumId,
                        TalentEntries = new List<TalentEntry>()
                    });
                    firstPage = masteryBook.BookPages.First();
                }

                firstPage.TalentEntries.Clear();
                switch (level)
                {
                    case 1:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                    }
                    break;
                    case 2:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 2,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                    }
                    break;
                    case 3:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                    }
                    break;
                    case 4:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 4,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                    }
                    break;
                    case 5:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                    }
                    break;
                    case 6:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                    }
                    break;
                    case 7:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                    }
                    break;
                    case 8:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 2,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                    }
                    break;
                    case 9:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                    }
                    break;
                    case 10:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 4,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                    }
                    break;
                    case 11:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                    }
                    break;
                    case 12:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                    }
                    break;
                    case 13:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                    }
                    break;
                    case 14:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 2,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                    }
                    break;
                    case 15:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                    }
                    break;
                    case 16:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 4,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                    }
                    break;
                    case 17:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                    }
                    break;
                    case 18:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                        //Thunderlord's Decree
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6362
                        });
                    }
                    break;
                    case 19:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                        //Thunderlord's Decree
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6362
                        });
                        //Fury
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6111
                        });
                    }
                    break;
                    case 20:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                        //Thunderlord's Decree
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6362
                        });
                        //Fury
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 2,
                            SummonerId = -1,
                            TalentId = 6111
                        });
                    }
                    break;
                    case 21:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                        //Thunderlord's Decree
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6362
                        });
                        //Fury
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6111
                        });
                    }
                    break;
                    case 22:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                        //Thunderlord's Decree
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6362
                        });
                        //Fury
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6111
                        });
                        //Sorcery
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6114
                        });
                    }
                    break;
                    case 23:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                        //Thunderlord's Decree
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6362
                        });
                        //Fury
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6111
                        });
                        //Sorcery
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 2,
                            SummonerId = -1,
                            TalentId = 6114
                        });
                    }
                    break;
                    case 24:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                        //Thunderlord's Decree
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6362
                        });
                        //Fury
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6111
                        });
                        //Sorcery
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 2,
                            SummonerId = -1,
                            TalentId = 6114
                        });
                        //Expose Weakness
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6123
                        });
                    }
                    break;
                    case 25:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                        //Thunderlord's Decree
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6362
                        });
                        //Fury
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6111
                        });
                        //Sorcery
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 2,
                            SummonerId = -1,
                            TalentId = 6114
                        });
                        //Expose Weakness
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6123
                        });
                        //Natural Talent
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6134
                        });
                    }
                    break;
                    case 26:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                        //Thunderlord's Decree
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6362
                        });
                        //Fury
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6111
                        });
                        //Sorcery
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 2,
                            SummonerId = -1,
                            TalentId = 6114
                        });
                        //Expose Weakness
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6123
                        });
                        //Natural Talent
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 2,
                            SummonerId = -1,
                            TalentId = 6134
                        });
                    }
                    break;
                    case 27:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                        //Thunderlord's Decree
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6362
                        });
                        //Fury
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6111
                        });
                        //Sorcery
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 2,
                            SummonerId = -1,
                            TalentId = 6114
                        });
                        //Expose Weakness
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6123
                        });
                        //Natural Talent
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6134
                        });
                    }
                    break;
                    case 28:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                        //Thunderlord's Decree
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6362
                        });
                        //Fury
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6111
                        });
                        //Sorcery
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 2,
                            SummonerId = -1,
                            TalentId = 6114
                        });
                        //Expose Weakness
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6123
                        });
                        //Natural Talent
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 4,
                            SummonerId = -1,
                            TalentId = 6134
                        });
                    }
                    break;
                    case 29:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                        //Thunderlord's Decree
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6362
                        });
                        //Fury
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6111
                        });
                        //Sorcery
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 2,
                            SummonerId = -1,
                            TalentId = 6114
                        });
                        //Expose Weakness
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6123
                        });
                        //Natural Talent
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6134
                        });
                    }
                    break;
                    case 30:
                    {
                        //Wanderer
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6311
                        });
                        //Secret Stash
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6322
                        });
                        //Merciless
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6331
                        });
                        //Dangerous Game
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6343
                        });
                        //Precision
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6351
                        });
                        //Thunderlord's Decree
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6362
                        });
                        //Fury
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 3,
                            SummonerId = -1,
                            TalentId = 6111
                        });
                        //Sorcery
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 2,
                            SummonerId = -1,
                            TalentId = 6114
                        });
                        //Expose Weakness
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6123
                        });
                        //Natural Talent
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 5,
                            SummonerId = -1,
                            TalentId = 6134
                        });
                        //Bounty Hunter
                        firstPage.TalentEntries.Add(new TalentEntry()
                        {
                            Rank = 1,
                            SummonerId = -1,
                            TalentId = 6141
                        });
                    }
                    break;
                }
                //Set our page as the current...
                foreach(var page in masteryBook.BookPages)
                {
                    page.Current = false;
                }
                firstPage.Current = true;
                masteryBook.BookPages[0] = firstPage;
                //save the page
                var newBook = await connection.SaveMasteryBook(masteryBook);
            }
            catch (Exception e)
            {
                Tools.Log(e.StackTrace);
            }
        }
        
        private string RandomString(int size)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < size; ++index)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26.0 * ezBot.random.NextDouble() + 65.0)));
                stringBuilder.Append(ch);
            }
            return stringBuilder.ToString();
        }
    }
    
    public class MasteriesTreeGG
    {
        public string tree { get; set; }
        public int total { get; set; }
        public List<MasteryGG> data { get; set; }
    }

    public class MasteryGG
    {
        public int points { get; set; }
        public int mastery { get; set; }
    }

}