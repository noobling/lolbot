namespace ezBot.Languages
{
    //Translated by Hesa from Elobuddy.net
    public class EnglishTranslator : ITranslator
    {
        public string EzBot { get { return "ezBot - Auto Queue for LOL: {0}"; } }
        public string By { get { return "Made by Tryller, Hesa maintained by fkingnoobgg."; } }
        public string Version { get { return "Version: {0}"; } }
        public string Support { get { return "Get support on discord: https://discord.gg/Fg2tQGa"; } }
        public string Garena { get { return "Garena now supported!"; } }
        public string SourceCode { get { return "Github: github.com/fkingnoobgg"; } }
        public string Issues { get { return "Please report issue(s) on elobuddy or github"; } }
        public string AdministratorRequired { get { return "ezBot must be started with administrator privileges."; } }
        public string ConfigLoaded { get { return "Bot Config loaded."; } }
        public string LauncherPathInvalid { get { return "Your LauncherPath is invalid."; } }
        public string PleaseTryThis { get { return "Please try this:"; } }
        public string LauncherFix1 { get { return "1. Make sure the path point to the FOLDER where we can find the launcher for league of legends and not to any exe file."; } }
        public string LauncherFix2 { get { return "2. Make sure the LauncherPath ends with a \\"; } }
        public string LauncherFix3 { get { return "3. Browse to the LauncherPath"; } }
        public string LauncherFix4 { get { return "4. Browse to RADS\\solutions\\lol_game_client_sln\\releases\\"; } }
        public string LauncherFix5 { get { return "5. Delete all folder in here except: 0.0.1.152"; } }
        public string ChangingGameConfig { get { return "Changing Game Config."; } }
        public string LoadingAccounts { get { return "Loading accounts."; } }
        public string MaximumBotsRunning { get { return "Maximum bots running: {0}"; } }
        public string YouMayHaveAnIssueInAccountsFile { get { return "You may have an issue in your accounts.txt"; } }
        public string AccountsStructure { get { return "Accounts structure ACCOUNT|PASSWORD|REGION|QUEUE_TYPE|IS_LEADER"; } }
        public string ErrorGetGarenaToken { get { return "Error Get Garena Token"; } }
        public string ErrorLeagueGameCfgRegular { get { return "Regular League game.cfg Error: If using VMWare Shared Folder, make sure it is not set to Read-Only.\nException: {0}"; } }
        public string ErrorLeagueGameCfgGarena { get { return "Garena League game.cfg Error: If using VMWare Shared Folder, make sure it is not set to Read-Only.\nException: {0}"; } }
        public string NoMoreAccountsToLogin { get { return "No more accounts to login."; } }
        public string GameModeInvalid { get { return "Game Mode invalid, make sure you are using one of the following modes."; } }
        public string WillShutdownOnceCurrentMatchEnds { get { return "Will shutdown once the current match ends."; } }
        public string EzBotGameStatus { get { return "ezBot - {0} Total - {1} Victory - {2} Defeat"; } }
        public string AcceptingLobbyInvite { get { return "{0}: Accepting lobby invite."; } }
        public string AllPlayersAccepted { get { return "All players accepted, starting queue."; } }
        public string PlayersAcceptedCount { get { return "{0}/{1} player(s) accepted, waiting till everybody accepted."; } }
        public string EnteringChampionSelect { get { return "{0}: Entering champion selection."; } }
        public string YouAreInChampionSelect { get { return "{0}: You are in champion select."; } }
        public string SelectedChampion { get { return "{1}: Selected Champion: {0}."; } }
        public string WaitingForOtherPlayersLockin { get { return "{0}: Waiting for other players to lockin."; } }
        public string ChampionNotAvailable { get { return "{1}: Champion '{0}' is not owned, is not free to play or has already been choosen."; } }
        public string WaitingChampSelectTimer { get { return "{0}: Waiting champ select timer to reach 0."; } }
        public string YouAreInQueue { get { return "{0}: You are in queue."; } }
        public string ReQueued { get { return "{1}: Re-queued: {0}."; } }
        public string QueuePopped { get { return "{0}: Queue popped."; } }
        public string AcceptedQueue { get { return "{0}: Accepted Queue!"; } }
        public string YouHaveLeaverBuster { get { return "You have leaver buster."; } }
        public string LaunchingLeagueOfLegends { get { return "{0}: Launching League of Legends."; } }
        public string ClosingGameClient { get { return "{0}: Closing game client."; } }
        public string InQueueAs { get { return "{1}: In Queue: {0}."; } }
        public string QueueFailedReason { get { return "Queue failed, reason: {0}."; } }
        public string LeaverBusterTaintedWarningError { get { return "Leaver Buster Tainted Warning error:\n{0}"; } }
        public string WaitingDodgeTimer { get { return "Waiting queue dodger timer: {0} minutes!"; } }
        public string WaitingLeaverTimer { get { return "Waiting leaver buster timer: {0} minutes!"; } }
        public string JoinedLowPriorityQueue { get { return "Joined lower priority queue! as {0}."; } }
        public string ErrorJoiningLowPriorityQueue { get { return "There was an error in joining lower priority queue.\nDisconnecting."; } }
        public string ErrorOccured { get { return "Error occured:\n{0}"; } }
        public string RestartingLeagueOfLegends { get { return "{0}: Restarting League of Legends."; } }
        public string RestartingLeagueOfLegendsAt { get { return "{1}: Restarting League of Legends at {0} please wait."; } }
        public string PositionInLoginQueue { get { return "Position in login queue: {0}."; } }
        public string LoggingIntoAccount { get { return "Logging in to your account..."; } }
        public string SummonerDoesntExist { get { return "Summoner not found in account."; } }
        public string CreatingSummoner { get { return "Creating Summoner..."; } }
        public string CreatedSummoner { get { return "Created Summoner: {0}."; } }
        public string AlreadyMaxLevel { get { return "Summoner: {0} is already max level."; } }
        public string LogIntoNewAccount { get { return "Log into new account."; } }
        public string BuyingXpBoost { get { return "Buying XP Boost."; } }
        public string CouldntBuyBoost { get { return "Couldn't buy Boost:\n{0}"; } }
        public string Normal5Requirements { get { return "Need to be Level 3 before NORMAL_5X5 queue."; } }
        public string JoinCoopBeginnerUntil { get { return "Joins Co-Op vs AI (Beginner) queue until {0}."; } }
        public string NeedLevel6BeforeAram { get { return "Need to be Level 6 before ARAM queue."; } }
        public string NeedLevel7Before3v3 { get { return "Need to be Level 7 before NORMAL_3X3 queue."; } }
        public string Welcome { get { return "Welcome {0} - lvl ({1}) IP: ({2}) - XP: ({3} / {4})."; } }
        public string SendingGameInvites { get { return "Sending game invites."; } }
        public string WaitingGameInviteFrom { get { return "Waiting game invite from {0}."; } }
        public string LevelUp { get { return "{1}: Level Up: {0}."; } }
        public string CurrentRp { get { return "Your Current RP: {0}."; } }
        public string CurrentIp { get { return "Your Current IP: {0}."; } }
        public string CharacterReachedMaxLevel { get { return "Your character reached the max level: {0}."; } }
        public string DownloadingMasteries { get { return "{0}: Downloading masteries from champion.gg."; } }
        public string UpdatingMasteries { get { return "{0}: Updating masteries."; } }
        public string Disconnected { get { return "Disconnected."; } }
        public string BoughtXpBoost3Days { get { return "Bought 'XP Boost: 3 Days'!"; } }

    }
}