
namespace ezBot.Languages
{
    //Translated by Eleng from Elobuddy.net
    public class PolishTranslator : ITranslator
    {
        public string EzBot { get { return "ezBot - Auto Queue for LOL: {0}"; } }
        public string By { get { return "Stworzone przez Tryller'a, zaktualizowane, zmodyfikowane i wspierane przez Hesa."; } }
        public string Version { get { return "Wersja: {0}"; } }
        public string Support { get { return "Poproś o pomoc na Discord: https://discord.gg/Fg2tQGa"; } }
        public string Garena { get { return "Region Garena jest w pełni wspierany!"; } }
        public string SourceCode { get { return "Wrzuciłem kod źródłowy na github'a: github.com/hesa2020/HesaElobuddy."; } }
        public string Issues { get { return "Proszę zgłaszać problemy w oficialnym temacie ezBot, zmodyfikowanym przez Hesa na Elobuddy.net."; } }
        public string AdministratorRequired { get { return "ezBot musi być włączony jako Administrator."; } }
        public string ConfigLoaded { get { return "Konfiguracja załadowana."; } }
        public string LauncherPathInvalid { get { return "Twoja ścieżka dostępu LoLLauncher jest nieprawidłowa."; } }
        public string PleaseTryThis { get { return "Spróbuj tego:"; } }
        public string LauncherFix1 { get { return "1. Upewnij się, że ścieżka prowadzi do FOLDERU gdzie jest launcher League of Legends, a nie do pliku exe."; } }
        public string LauncherFix2 { get { return "2. Upewnij się, że ścieżka kończy się \\"; } }
        public string LauncherFix3 { get { return "3. Przejdź do do folderu z Launcherem"; } }
        public string LauncherFix4 { get { return "4. Przejdź do RADS\\solutions\\lol_game_client_sln\\releases\\"; } }
        public string LauncherFix5 { get { return "5. Usuń wszystko oprócz: 0.0.1.152, bądź jemu podobnemu"; } }
        public string ChangingGameConfig { get { return "Trwa zmiana pliku konfiguracyjnego gry."; } }
        public string LoadingAccounts { get { return "Ładowanie kont."; } }
        public string MaximumBotsRunning { get { return "Maksymalna ilość botów: {0}"; } }
        public string YouMayHaveAnIssueInAccountsFile { get { return "Prawdopodobnie występuje błąd w twoim pliku accounts.txt"; } }
        public string AccountsStructure { get { return "Struktura kont: LOGIN_KONTA|HASŁO|REGION|TYP_KOLEJKI|CZY_JEST_LEADEREM"; } }
        public string ErrorGetGarenaToken { get { return "Błąd pobierania Tokenu Garena"; } }
        public string ErrorLeagueGameCfgRegular { get { return "Błąd pliku LoL'a game.cfg: Jeżeli używasz Wspólnego Folderu VMWare (VMWare Shared Folder), upewnij się, że nie jest ustawiony jako Tylko do Odczytu.\nException: {0}"; } }
        public string ErrorLeagueGameCfgGarena { get { return "Błąd pliku LoL'a Garena game.cfg:Jeżeli używasz Wspólnego Folderu VMWare (VMWare Shared Folder), upewnij się, że nie jest ustawiony jako Tylko do Odczytu.\nException: {0}"; } }
        public string NoMoreAccountsToLogin { get { return "Brak kolejnych kont do zalogowania."; } }
        public string GameModeInvalid { get { return "Nieprawidłowy Tryb Gry, upewnij się, że używasz jednego z następujących trybów."; } }
        public string WillShutdownOnceCurrentMatchEnds { get { return "Wyłączenie nastąpi po zakończeniu rozgrywki."; } }
        public string EzBotGameStatus { get { return "ezBot - {0} Ogólnie - {1} Wygrana - {2} Przegrana"; } }
        public string AcceptingLobbyInvite { get { return "{0}: Akceptowanie zaproszenia do lobby."; } }
        public string AllPlayersAccepted { get { return "Wszystkie boty zaakceptowały, następuje wejście do kolejki."; } }
        public string PlayersAcceptedCount { get { return "{0}/{1} Ilość zaakceptowanych kolejek, oczekiwanie na resztę botów."; } }
        public string EnteringChampionSelect { get { return "{0}: Przechodzenie do Wyboru Postaci."; } }
        public string YouAreInChampionSelect { get { return "{0}: Jesteś w wyborze postaci."; } }
        public string SelectedChampion { get { return "{1}: Wybrana postać: {0}."; } }
        public string WaitingForOtherPlayersLockin { get { return "{0}: Oczekiwanie na wybór postaci przez innych graczy."; } }
        public string ChampionNotAvailable { get { return "{1}: Postać '{0}' nie jest w Twoim posiadaniu, nie jest ona w Darmowej Rotacji Bohaterów, bądź została już wybrana."; } }
        public string WaitingChampSelectTimer { get { return "{0}: Oczekiwanie na zakończenie Wyboru Postaci."; } }
        public string YouAreInQueue { get { return "{0}: Jesteś w kolejce."; } }
        public string ReQueued { get { return "{1}: Ponowne dołączanie do kolejki: {0}."; } }
        public string QueuePopped { get { return "{0}: Akceptacja gotowości."; } }
        public string AcceptedQueue { get { return "{0}: Zaakceptowano gotowość!"; } }
        public string YouHaveLeaverBuster { get { return "Twojo konto zostało oflagowane systemem LeaverBuster."; } }
        public string LaunchingLeagueOfLegends { get { return "{0}: Uruchamianie League of Legends."; } }
        public string ClosingGameClient { get { return "{0}: Zamykanie klienta gry."; } }
        public string InQueueAs { get { return "{1}: W kolejce: {0}."; } }
        public string QueueFailedReason { get { return "Kolejkowanie nie powiodło się, powód: {0}."; } }
        public string LeaverBusterTaintedWarningError { get { return "Leaver Buster Tainted Warning error:\n{0}"; } }
        public string WaitingDodgeTimer { get { return "Oczekiwanie na timer wyjścia z Wyboru Postaci: {0} minutes!"; } }
        public string WaitingLeaverTimer { get { return "Oczekiwanie na timer LeaverBuster: {0} minutes!"; } }
        public string JoinedLowPriorityQueue { get { return "Dołączono do Kolejki Niskiego Priotytetu, jako {0}!"; } }
        public string ErrorJoiningLowPriorityQueue { get { return "Wystąpił błąd podczas dołączania do Kolejki Niskiego Priorytetu.\nRozłączanie."; } }
        public string ErrorOccured { get { return "Wystąpił błąd:\n{0}"; } }
        public string RestartingLeagueOfLegends { get { return "{0}: Ponowne uruchamianie League of Legends."; } }
        public string RestartingLeagueOfLegendsAt { get { return "{1}: Trwa ponowne uruchamianie League of Legends w {0}, proszę czekać."; } }
        public string PositionInLoginQueue { get { return "Pozycja w kolejce: {0}."; } }
        public string LoggingIntoAccount { get { return "Logowanie do Twojego konta..."; } }
        public string SummonerDoesntExist { get { return "Imię przywoływacza nie zostało ustalone."; } }
        public string CreatingSummoner { get { return "Tworzenie Przywoływacza..."; } }
        public string CreatedSummoner { get { return "Tworzenie Przywoływacza: {0}."; } }
        public string AlreadyMaxLevel { get { return "Przywoływacz: {0} posiada maksymalny poziom."; } }
        public string LogIntoNewAccount { get { return "Zaloguj się do nowego konta."; } }
        public string BuyingXpBoost { get { return "Kupowanie Dopalacza Punktów Doświadczenia."; } }
        public string CouldntBuyBoost { get { return "Kupienie Dopalacza nie powiodło się:\n{0}"; } }
        public string Normal5Requirements { get { return "Musisz mieć przynajmniej 3 Poziom doświadczenia, aby dołączyć do kolejki NORMAL_5X5."; } }
        public string JoinCoopBeginnerUntil { get { return "Dołączanie do kolejki Razem vs. AI (Początkujący) zanim {0}."; } }
        public string NeedLevel6BeforeAram { get { return "Musisz mieć przynajmniej 6 Poziom doświadczenia, aby dołączyć do kolejki ARAM."; } }
        public string NeedLevel7Before3v3 { get { return "Musisz mieć przynajmniej 7 Poziom doświadczenia, aby dołączyć do kolejki NORMAL_3X3."; } }
        public string Welcome { get { return "Witaj {0} - Poziom ({1}) PZ: ({2}) - PD: ({3} / {4})."; } }
        public string SendingGameInvites { get { return "Wysyłanie zaproszeń do gry."; } }
        public string WaitingGameInviteFrom { get { return "Oczekiwanie na zaproszenie od {0}."; } }
        public string LevelUp { get { return "{1}: Awans: {0}."; } }
        public string CurrentRp { get { return "Twoja aktualna ilość RP: {0}."; } }
        public string CurrentIp { get { return "Twoja aktualna ilość PZ: {0}."; } }
        public string CharacterReachedMaxLevel { get { return "Twoja postać osiągnęła maksymalny Poziom doświadczenia: {0}."; } }
        public string DownloadingMasteries { get { return "{0}: Pobieranie masterek z champion.gg."; } }
        public string UpdatingMasteries { get { return "{0}: Aktualizowanie masterek."; } }
        public string Disconnected { get { return "Rozłączono."; } }
        public string BoughtXpBoost3Days { get { return "Kupiono 'Dopalacz Punktów Doświadczenia: 3 Dni'!"; } }

    }
}