namespace ezBot.Languages
{
    //Translated by pexhorse from Discord
    public class KoreanTranslator : ITranslator
    {
        public string EzBot { get { return "ezBot - 롤 클라이언트 오토 큐: {0}"; } }
        public string By { get { return "원작자 : Tryller 리메이크 및 최적화 : Hesa."; } }
        public string Version { get { return "버전: {0}"; } }
        public string Support { get { return "디스코드에서 의견/도움을 말해주세요. : https://discord.gg/Fg2tQGa"; } }
        public string Garena { get { return "가레나도 지원합니다!"; } }
        public string SourceCode { get { return "해당 프로그램의 소스 깃 허브 github.com/hesa2020/HesaElobuddy."; } }
        public string Issues { get { return "이슈/에러는 Elobuddy.net의 hesa의 스레드(작성글)에 작성해주세요."; } }
        public string AdministratorRequired { get { return "ezBot은 반드시 관리자 권한으로 실행해야합니다."; } }
        public string ConfigLoaded { get { return "설정(Config)을 불러왔습니다."; } }
        public string LauncherPathInvalid { get { return "롤 클라이언트 설치경로(LauncherPath)에 문제가 있습니다."; } }
        public string PleaseTryThis { get { return "문제 발생시 확인해봐야 할것들:"; } }
        public string LauncherFix1 { get { return "1. 롤 클라이언트 설치경로(LauncherPath)가 정확한지 확인해주시고, lol.exe이 아닌 폴더 경로만 설정해주시면 됩니다."; } }
        public string LauncherFix2 { get { return "2. 롤 클라이언트 설치경로(LauncherPath) 끝에 \\를 추가해주세요. 꼭 있어야합니다.\n 예시 : C:\\lol -> / C:\\lol\\"; } }
        public string LauncherFix3 { get { return "3. 롤 클라이언트 설치경로(LauncherPath)에 들어갑니다."; } }
        public string LauncherFix4 { get { return "4. 그리고 해당 경로에 들어갑니다. RADS\\solutions\\lol_game_client_sln\\releases\\"; } }
        public string LauncherFix5 { get { return "5. 다음 폴더를 제외한 모든 폴더 삭제합니다 제외: 0.0.1.152"; } }
        public string ChangingGameConfig { get { return "게임 설정이 변경되었습니다."; } }
        public string LoadingAccounts { get { return "계정을 불러오는 중입니다."; } }
        public string MaximumBotsRunning { get { return "최대 봇 실행중!!: {0}"; } }
        public string YouMayHaveAnIssueInAccountsFile { get { return "accounts.txt에 문제가 있습니다 확인해주세요."; } }
        public string AccountsStructure { get { return "계정(Accounts.txt) 설정방법: ACCOUNT|PASSWORD|REGION|QUEUE_TYPE|IS_LEADER"; } }
        public string ErrorGetGarenaToken { get { return "가레나 토큰 가져오기 에러"; } }
        public string ErrorLeagueGameCfgRegular { get { return "일반 롤 클라이언트 game.cfg 에러: 만약에 VMWare 공유 폴더 사용시, 읽기 전용으로 설정되어 있지 않은지 확인하세요.\nException: {0}"; } }
        public string ErrorLeagueGameCfgGarena { get { return "가레나 롤 클라이언트 game.cfg 에러: 만약에 VMWare 공유 폴더 사용시, 읽기 전용으로 설정되어 있지 않은지 확인하세요.\nException: {0}"; } }
        public string NoMoreAccountsToLogin { get { return "더이상 로그인 할 계정이 없습니다."; } }
        public string GameModeInvalid { get { return "게임 모드 에러!, 게임 모드를 제대로 설정해주세요."; } }
        public string WillShutdownOnceCurrentMatchEnds { get { return "현재 매치가 모두 끝나면 프로그램이 종료됩니다."; } }
        public string EzBotGameStatus { get { return "ezBot - 총 {0} 게임 - {1} 승리 - {2} 패배"; } }
        public string AcceptingLobbyInvite { get { return "{0}: 로비 초대를 허용합니다."; } }
        public string AllPlayersAccepted { get { return "모든 플레이어가 수락했습니다, 큐를 시작합니다."; } }
        public string PlayersAcceptedCount { get { return "{0}/{1}명 수락, 나머지 플레이어를 기다리는 중입니다."; } }
        public string EnteringChampionSelect { get { return "{0}: 챔피언 선택에 들어갑니다."; } }
        public string YouAreInChampionSelect { get { return "{0}: 챔피언을 선택합니다."; } }
        public string SelectedChampion { get { return "{1}: 선택된 챔피언: {0}."; } }
        public string WaitingForOtherPlayersLockin { get { return "{0}: 다른 플레이어 챔피언 선택을 기다리는 중입니다."; } }
        public string ChampionNotAvailable { get { return "{1}: 챔피언 '{0}' 선택하지 못함, 누군가 선택했거나, 챔피언이 없습니다."; } }
        public string WaitingChampSelectTimer { get { return "{0}: 챔피언 선택 시간이 0초가 되도록 기다리는 중입니다."; } }
        public string YouAreInQueue { get { return "{0}: 큐에 있습니다."; } }
        public string ReQueued { get { return "{1}: 게임 대기열 재진입: {0} 소환사."; } }
        public string QueuePopped { get { return "{0}: 대기열이 잡혔습니다."; } }
        public string AcceptedQueue { get { return "{0}: 게임이 수락됐습니다!"; } }
        public string YouHaveLeaverBuster { get { return "탈주 패널티를 받았습니다."; } }
        public string LaunchingLeagueOfLegends { get { return "{0}: League of Legends를 실행합니다."; } }
        public string ClosingGameClient { get { return "{0}: 게임 클라이언트를 닫습니다."; } }
        public string InQueueAs { get { return "{1}: 게임 대기열 진입: {0}."; } }
        public string QueueFailedReason { get { return "큐 실패, 이유: {0}."; } }
        public string LeaverBusterTaintedWarningError { get { return "탈주 패널티 경고 에러:\n{0}"; } }
        public string WaitingDodgeTimer { get { return "닷지 패널티 시간을 기다리는 중입니다: {0} 분 남았습니다!"; } }
        public string WaitingLeaverTimer { get { return "탈주 패널티 시간을 기다리는 중입니다: {0} 분 남았습니다!"; } }
        public string JoinedLowPriorityQueue { get { return "낮은 우선순위 대기열에 들어갔습니다. 표시: {0}."; } }
        public string ErrorJoiningLowPriorityQueue { get { return "낮은 우선순위 대기열에 들어가는데 문제가 생겼습니다.\n연결해제 합니다.."; } }
        public string ErrorOccured { get { return "에러가 발생했습니다:\n{0}"; } }
        public string RestartingLeagueOfLegends { get { return "{0}: League of Legends를 재시작합니다."; } }
        public string RestartingLeagueOfLegendsAt { get { return "{1}: League of Legends {0} 초 후 재실합니다."; } }
        public string PositionInLoginQueue { get { return "로그인 대기열: {0}."; } }
        public string LoggingIntoAccount { get { return "계정에 로그인합니다..."; } }
        public string SummonerDoesntExist { get { return "소환사 계정이 없습니다."; } }
        public string CreatingSummoner { get { return "소환사를 만듭니다..."; } }
        public string CreatedSummoner { get { return "소환사를 만들었습니다: {0}."; } }
        public string AlreadyMaxLevel { get { return "소환사: {0} 는 이미 최대 레벨입니다."; } }
        public string LogIntoNewAccount { get { return "새 계정에 로그인."; } }
        public string BuyingXpBoost { get { return "XP 부스트 구매."; } }
        public string CouldntBuyBoost { get { return "부스트 구매 실패:\n{0}"; } }
        public string Normal5Requirements { get { return "소환사의 협곡(NORMAL_5X5)을 하기 위해서는 레벨 3이 필요합니다."; } }
        public string JoinCoopBeginnerUntil { get { return "초급 봇(Co-Op vs AI BEGINNER_BOT) 를 시작합니다. 레벨 {0} 될때까지."; } }
        public string NeedLevel6BeforeAram { get { return "칼바람 나락(ARAM)을 하기 위해서는 레벨 6이 필요합니다."; } }
        public string NeedLevel7Before3v3 { get { return "뒤틀린 숲(NORMAL_3X3)을 하기 위해서는 레벨 7이 필요합니다."; } }
        public string Welcome { get { return "환영합니다! 소환사명 : {0} - 레벨 ({1}) IP: ({2}) - XP: ({3} / {4})."; } }
        public string SendingGameInvites { get { return "게임 초대를 보냈습니다."; } }
        public string WaitingGameInviteFrom { get { return "게임 초대를 기다리는 중입니다 {0}."; } }
        public string LevelUp { get { return "{1}: 레벨 업!: {0}."; } }
        public string CurrentRp { get { return "현재 RP: {0}."; } }
        public string CurrentIp { get { return "현재 IP: {0}."; } }
        public string CharacterReachedMaxLevel { get { return "캐릭터가 최대 레벨에 도달했습니다: {0}."; } }
        public string DownloadingMasteries { get { return "{0}: champion.gg에서 마스터리를 가져옵니다."; } }
        public string UpdatingMasteries { get { return "{0}: 마스터리를 업데이트했습니다."; } }
        public string Disconnected { get { return "연결해제되었습니다."; } }
        public string BoughtXpBoost3Days { get { return "'XP 부스트: 3일 구입'!"; } }
    }
}