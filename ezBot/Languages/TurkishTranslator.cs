namespace ezBot.Languages
{
    //Translated by berkerbey from Elobuddy.net
    public class TurkishTranslator : ITranslator
    {
        public string EzBot { get { return "ezBot - LoL için Otomatik Karşılaşma Arama Aracı: {0}"; } }
        public string By { get { return "Tryller tarafından yapılmıştır, Hesa tarafından güncellenmiştir , düzenlenmiştir ve desteklenmektedir."; } }
        public string Version { get { return "Versiyon: {0}"; } }
        public string Support { get { return "Discord'da yardım al: https://discord.gg/Fg2tQGa"; } }
        public string Garena { get { return "Garena desteklenmektedir!"; } }
        public string SourceCode { get { return "Kaynak kodunu github'a yükledim github.com/hesa2020/HesaElobuddy."; } }
        public string Issues { get { return "Lütfen problemleri Elobuddy.net adresindeki Hesa tarafından düzenlenen ezbot başlığında bildiriniz."; } }
        public string AdministratorRequired { get { return "ezBot yönetici olarak çalıştırılmalıdır."; } }
        public string ConfigLoaded { get { return "Config dosyası yüklendi."; } }
        public string LauncherPathInvalid { get { return "Oyun dizini geçersiz."; } }
        public string PleaseTryThis { get { return "Lütfen şunu deneyin:"; } }
        public string LauncherFix1 { get { return "1. Girdğiniz oyun dizininin League of Legends exe'sinin olduğu KLASÖR oldugundan emin olun."; } }
        public string LauncherFix2 { get { return "2. Oyun dizininin \\ ile bittiğinden emin olun."; } }
        public string LauncherFix3 { get { return "3. Browse to the LauncherPath"; } }
        public string LauncherFix4 { get { return "4. RADS\\solutions\\lol_game_client_sln\\releases\\ dizinine gidin."; } }
        public string LauncherFix5 { get { return "5. Buradaki 0.0.1.152 klasörü hariç tüm klasörleri silin."; } }
        public string ChangingGameConfig { get { return "Oyun ekran ayarları değiştiriliyor."; } }
        public string LoadingAccounts { get { return "Hesaplar yükleniyor."; } }
        public string MaximumBotsRunning { get { return "Çalışan maximum bot: {0}"; } }
        public string YouMayHaveAnIssueInAccountsFile { get { return "Accounts.txt dosyasında bir sıkıntı olabilir"; } }
        public string AccountsStructure { get { return "Accounts şablonu ACCOUNT|PASSWORD|REGION|QUEUE_TYPE|IS_LEADER bu şekildedir."; } }
        public string ErrorGetGarenaToken { get { return "Error Get Garena Token"; } }
        public string ErrorLeagueGameCfgRegular { get { return "Regular League game.cfg Error: If using VMWare Shared Folder, make sure it is not set to Read-Only.\nException: {0}"; } }
        public string ErrorLeagueGameCfgGarena { get { return "Garena League game.cfg Error: If using VMWare Shared Folder, make sure it is not set to Read-Only.\nException: {0}"; } }
        public string NoMoreAccountsToLogin { get { return "Giriş yapılacak daha fazla hesap yok."; } }
        public string GameModeInvalid { get { return "Oyun modu geçersiz, Mevcut modlardan birini girdiğinizden emin olun."; } }
        public string WillShutdownOnceCurrentMatchEnds { get { return "Mevcut maç bitiminde kapatlıacak."; } }
        public string EzBotGameStatus { get { return "ezBot - {0} Toplam - {1} Zafer - {2} Bozgun"; } }
        public string AcceptingLobbyInvite { get { return "{0}: Lobi daveti kabul ediliyor."; } }
        public string AllPlayersAccepted { get { return "Tüm oyuncular kabul etti, karşılaşma aranıyor."; } }
        public string PlayersAcceptedCount { get { return "{0}/{1} oyuncu kabul etti, herkesin kabul etmesi bekleniyor."; } }
        public string EnteringChampionSelect { get { return "{0}: Şampiyon seçimine giriliyor."; } }
        public string YouAreInChampionSelect { get { return "{0}: Şampiyon seçimindesiniz."; } }
        public string SelectedChampion { get { return "{1}: Seçilen şampiyon: {0}."; } }
        public string WaitingForOtherPlayersLockin { get { return "{0}: Diğer oyuncuların seçim yapması bekleniyor."; } }
        public string ChampionNotAvailable { get { return "'{0}' adlı şampiyona sahip değilsiniz, ücretsiz şampiyon rotasyonu içinde dğeil ya da başkası tarafından seçilmiş."; } }
        public string WaitingChampSelectTimer { get { return "{0}: Seçim süresinin bitmesi bekleniyor."; } }
        public string YouAreInQueue { get { return "{0}: Karşılaşma aranıyor."; } }
        public string ReQueued { get { return "{1}: Tekrar sıraya girildi: {0} ."; } }
        public string QueuePopped { get { return "{0}: Karşılaşma bulundu."; } }
        public string AcceptedQueue { get { return "{0}: Karşılaşma kabul edildi!"; } }
        public string YouHaveLeaverBuster { get { return "Afk cezanız var."; } }
        public string LaunchingLeagueOfLegends { get { return "{0}: League of Legends başlatılıyor."; } }
        public string ClosingGameClient { get { return "{0}: Oyun istemcisi kapatılıyor."; } }
        public string InQueueAs { get { return "{1}: Sıradasınız: {0}."; } }
        public string QueueFailedReason { get { return "Karşılaşma arama, sebep: {0}."; } }
        public string LeaverBusterTaintedWarningError { get { return "Leaver Buster Tainted Warning error:\n{0}"; } }
        public string WaitingDodgeTimer { get { return "Sıra bozma cezası bekleniyor: {0} minutes!"; } }
        public string WaitingLeaverTimer { get { return "Afk cezası bekleniyor: {0} minutes!"; } }
        public string JoinedLowPriorityQueue { get { return "{0} olarak düşük öncelikli sıraya girildi."; } }
        public string ErrorJoiningLowPriorityQueue { get { return "Düşük öncelikli sıraya girme sırasında hata oluştu.\nBağlantı kopması."; } }
        public string ErrorOccured { get { return "Hata meydana geldi:\n{0}"; } }
        public string RestartingLeagueOfLegends { get { return "{0}: League of Legends yeniden başlatılıyor."; } }
        public string RestartingLeagueOfLegendsAt { get { return "{1}: League of Legends {0} içinde yeniden başlatılacak lütfen bekleyin."; } }
        public string PositionInLoginQueue { get { return "Giriş sırası pozişyonu: {0}."; } }
        public string LoggingIntoAccount { get { return "Hesaba giriş yapılıyor..."; } }
        public string SummonerDoesntExist { get { return "Hesapta sihirdar bulunamadı."; } }
        public string CreatingSummoner { get { return "Sihirdar oluşturuluyor..."; } }
        public string CreatedSummoner { get { return "Sihirdar Oluşturuldu: {0}."; } }
        public string AlreadyMaxLevel { get { return "Sihirdar: {0} zaten maximum seviyede."; } }
        public string LogIntoNewAccount { get { return "Yeni hesaba giriliyor."; } }
        public string BuyingXpBoost { get { return "XP takviyesi alınıyor."; } }
        public string CouldntBuyBoost { get { return "Takviye alınamadı:\n{0}"; } }
        public string Normal5Requirements { get { return "NORMAL_5X5 sırası için 3. seviye olmanız gerekmektedir."; } }
        public string JoinCoopBeginnerUntil { get { return "{0} seviye olana kadar Başlangıç Bot moduna giriliyor."; } }
        public string NeedLevel6BeforeAram { get { return "Aram sırası için 6. seviye olmanız gerekmektedir."; } }
        public string NeedLevel7Before3v3 { get { return "NORMAL_3X3 sırası için 7. seviye olmanız gerekmektedir."; } }
        public string Welcome { get { return "Hoş geldiniz {0} - Seviye ({1}) IP: ({2}) - XP: ({3} / {4})."; } }
        public string SendingGameInvites { get { return "Oyun daveti gönderiliyor."; } }
        public string WaitingGameInviteFrom { get { return "{0} 'den oyun daveti bekleniyor."; } }
        public string LevelUp { get { return "{1}: Seviye atladınız: {0}."; } }
        public string CurrentRp { get { return "Mevcut RP: {0}."; } }
        public string CurrentIp { get { return "Mevcut IP: {0}."; } }
        public string CharacterReachedMaxLevel { get { return "Hesabınız maximum seviyeye ulaştı: {0}."; } }
        public string DownloadingMasteries { get { return "{0}: Champion.gg 'den kabiliyetler indiriliyor."; } }
        public string UpdatingMasteries { get { return "{0}: Kabiliyetler güncelleniyor."; } }
        public string Disconnected { get { return "Bağlantı Kesildi."; } }
        public string BoughtXpBoost3Days { get { return "XP takviyesi alındı: 3 Günlük!"; } }
    }
}