namespace ezBot.Languages
{
    //Translated by Hesa from Elobuddy.net
    public class FrenchTranslator : ITranslator
    {
        public string EzBot { get { return "ezBot - Auto Queue pour LOL: {0}"; } }
        public string By { get { return "Fabriqué par Tryller mis à jour, personnalisé et pris en charge par Hesa."; } }
        public string Version { get { return "Version: {0}"; } }
        public string Support { get { return "Obtenir de l'aide sur discord: https://discord.gg/Fg2tQGa"; } }
        public string Garena { get { return "Garena maintenant pris en charge!"; } }
        public string SourceCode { get { return "J'ai mis le code source sur github à github.com/hesa2020/HesaElobuddy."; } }
        public string Issues { get { return "S'il vous plaît signaler les problèmes sur le fil ezbot personnalisé par hesa sur Elobuddy.net."; } }
        public string AdministratorRequired { get { return "ezBot doit être démarré avec des privilèges d'administrateur."; } }
        public string ConfigLoaded { get { return "Configuration chargée."; } }
        public string LauncherPathInvalid { get { return "Votre LauncherPath n'est pas valide."; } }
        public string PleaseTryThis { get { return "Veuillez essayer ceci:"; } }
        public string LauncherFix1 { get { return "1. Assurez-vous que le chemin d'accès pointe vers le DOSSIER où nous pouvons trouver le lanceur pour League of Legends et non un fichier exe."; } }
        public string LauncherFix2 { get { return "2. Assurez-vous que le LauncherPath se termine par un \\"; } }
        public string LauncherFix3 { get { return "3. Accédez à "; } }
        public string LauncherFix4 { get { return "4. Accédez à RADS\\solutions\\lol_game_client_sln\\releases\\"; } }
        public string LauncherFix5 { get { return "5. Supprimer tout les dossiers ici sauf: 0.0.1.152"; } }
        public string ChangingGameConfig { get { return "Modification de la configuration du jeu."; } }
        public string LoadingAccounts { get { return "Chargement des comptes."; } }
        public string MaximumBotsRunning { get { return "Maximum de bots en cours d'exécution: {0}"; } }
        public string YouMayHaveAnIssueInAccountsFile { get { return "Vous avez probablement rencontrer un problème dans votre accounts.txt"; } }
        public string AccountsStructure { get { return "Structure des comptes: ACCOUNT|PASSWORD|REGION|QUEUE_TYPE|IS_LEADER"; } }
        public string ErrorGetGarenaToken { get { return "Erreur lors de la capture du Jeton Garena."; } }
        public string ErrorLeagueGameCfgRegular { get { return "Ordinaire League game.cfg Erreur: Si vous utilisez le dossier partagé VMWare, assurez-vous qu'il n'est pas défini sur Lecture seule.\nException: {0}"; } }
        public string ErrorLeagueGameCfgGarena { get { return "Garena League game.cfg Erreur: Si vous utilisez le dossier partagé VMWare, assurez-vous qu'il n'est pas défini sur Lecture seule.\nException: {0}"; } }
        public string NoMoreAccountsToLogin { get { return "Plus de comptes à connecter."; } }
        public string GameModeInvalid { get { return "Mode de jeu non valide, assurez-vous que vous utilisez l'un des modes suivants."; } }
        public string WillShutdownOnceCurrentMatchEnds { get { return "S'arrêtera une fois le match en cours terminé."; } }
        public string EzBotGameStatus { get { return "ezBot - {0} Totalr - {1} Victoire - {2} Défaite"; } }
        public string AcceptingLobbyInvite { get { return "{0}: Accepter l'invitation au lobby."; } }
        public string AllPlayersAccepted { get { return "Tous les joueurs ont accepté, en commençant la file d'attente."; } }
        public string PlayersAcceptedCount { get { return "{0}/{1} jouer(s) ont accepté, attendant que tout le monde accepte."; } }
        public string EnteringChampionSelect { get { return "{0}: Enterons dans la sélection de champion."; } }
        public string YouAreInChampionSelect { get { return "{0}: Vous êtes dans la sélection de champion."; } }
        public string SelectedChampion { get { return "{1}: Champion sélectionné: {0}."; } }
        public string WaitingForOtherPlayersLockin { get { return "{0}: En attente des autres joueurs."; } }
        public string ChampionNotAvailable { get { return "{1}: Le champion '{0}' n'est pas possédé, n'est pas libre de jouer ou a déjà été choisi."; } }
        public string WaitingChampSelectTimer { get { return "{0}: En attente pour la minuterie de la sélection de champion pour atteindre 0."; } }
        public string YouAreInQueue { get { return "{0}: Vous êtes en file d'attente."; } }
        public string ReQueued { get { return "{1}: Mis en file d'attente: {0}."; } }
        public string QueuePopped { get { return "{0}: File d'attente prête."; } }
        public string AcceptedQueue { get { return "{0}: File d'attente acceptée!"; } }
        public string YouHaveLeaverBuster { get { return "Vous avez une probation de type leaverbuster."; } }
        public string LaunchingLeagueOfLegends { get { return "{0}: Lancement de League of Legends."; } }
        public string ClosingGameClient { get { return "{0}: Fermeture du client de jeu."; } }
        public string InQueueAs { get { return "{1}: Dans la file d'attente: {0}."; } }
        public string QueueFailedReason { get { return "Échec de la file d'attente, raison: {0}."; } }
        public string LeaverBusterTaintedWarningError { get { return "Avertissement Leaver Buster, erreur:\n{0}"; } }
        public string WaitingDodgeTimer { get { return "En attente de probation d'esquive: {0} minutes!"; } }
        public string WaitingLeaverTimer { get { return "En attente de probation du quitteur: {0} minutes!"; } }
        public string JoinedLowPriorityQueue { get { return "Rejoint la file d'attente de priorité inférieure! comme {0}."; } }
        public string ErrorJoiningLowPriorityQueue { get { return "Une erreur s'est produite lors de l'ajout à la file d'attente de priorité inférieure.\nDisconnecting."; } }
        public string ErrorOccured { get { return "Erreur est survenue:\n{0}"; } }
        public string RestartingLeagueOfLegends { get { return "{0}: Redémarrage League of Legends."; } }
        public string RestartingLeagueOfLegendsAt { get { return "{1}: Redémarrage League of Legends à {0} s'il vous plaît, attendez."; } }
        public string PositionInLoginQueue { get { return "Position dans la file d'attente de connexion: {0}."; } }
        public string LoggingIntoAccount { get { return "Connexion à votre compte..."; } }
        public string SummonerDoesntExist { get { return "Invocateur introuvable."; } }
        public string CreatingSummoner { get { return "Création d'invocateur..."; } }
        public string CreatedSummoner { get { return "Invocateur créé: {0}."; } }
        public string AlreadyMaxLevel { get { return "Invocateur: {0} sst déjà niveau max."; } }
        public string LogIntoNewAccount { get { return "Connexion à un nouveau compte."; } }
        public string BuyingXpBoost { get { return "Achat XP Boost."; } }
        public string CouldntBuyBoost { get { return "Impossible d'acheter Boost:\n{0}"; } }
        public string Normal5Requirements { get { return "Besoin d'être Niveau 3 avant la file d'attente NORMAL_5X5."; } }
        public string JoinCoopBeginnerUntil { get { return "Joint la file d'attente Co-Op vs AI (Débutant) jusqu'à niveau {0}."; } }
        public string NeedLevel6BeforeAram { get { return "Besoin d'être Niveau 6 avant la file d'attente ARAM."; } }
        public string NeedLevel7Before3v3 { get { return "Besoin d'être Niveau 8 avant la file d'attente NORMAL_3X3."; } }
        public string Welcome { get { return "Bienvenue {0} - lvl ({1}) IP: ({2}) - XP: ({3} / {4})."; } }
        public string SendingGameInvites { get { return "Envoi d'invitations au jeu."; } }
        public string WaitingGameInviteFrom { get { return "En attente d'invitation à jouer la part de {0}."; } }
        public string LevelUp { get { return "{1}: Niveau supérieur: {0}."; } }
        public string CurrentRp { get { return "Vos RP: {0}."; } }
        public string CurrentIp { get { return "Vos IP: {0}."; } }
        public string CharacterReachedMaxLevel { get { return "Votre personnage a atteint le niveau maximal: {0}."; } }
        public string DownloadingMasteries { get { return "{0}: Téléchargement de masteries de champion.gg."; } }
        public string UpdatingMasteries { get { return "{0}: Mise à jour des masteries."; } }
        public string Disconnected { get { return "Déconnecté."; } }
        public string BoughtXpBoost3Days { get { return "Acheté 'XP Boost: 3 Jours'!"; } }

    }
}