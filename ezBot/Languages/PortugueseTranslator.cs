namespace ezBot.Languages
{
    //Translated by kikiss from Elobuddy.net
    public class PortugueseTranslator : ITranslator
    {
        public string EzBot { get { return "ezBot - Auto Queue para LOL: {0}"; } }
        public string By { get { return "Feito por Tryller atualizado, customizado e apoiado por Hesa."; } }
        public string Version { get { return "Versao: {0}"; } }
        public string Support { get { return "Receba suporte direto no discord: https://discord.gg/Fg2tQGa"; } }
        public string Garena { get { return "Ja apoia o Garena!"; } }
        public string SourceCode { get { return "Eu coloquei o codigo fonte no github em github.com/hesa2020/HesaElobuddy."; } }
        public string Issues { get { return "Favor reportar problema(s) no topico do ezbot customizado pelo Hesa no Elobuddy.net."; } }
        public string AdministratorRequired { get { return "ezBot deve ser inicializado com privilegios de administrador."; } }
        public string ConfigLoaded { get { return "Configurações carregadas."; } }
        public string LauncherPathInvalid { get { return "Seu diretorio de jogo(LauncherPath) e invalido."; } }
        public string PleaseTryThis { get { return "Favor tentar isto:"; } }
        public string LauncherFix1 { get { return "1. Tenha certeza que o diretorio aponta para a PASTA onde pode-se encontrar o atualizador para o league of legends e nao para um arquivo exe."; } }
        public string LauncherFix2 { get { return "2. Tenha certeza que o diretorio de jogo(LauncherPath) termina com um \\"; } }
        public string LauncherFix3 { get { return "3. Navegue ate o diretorio de jogo(LauncherPath)"; } }
        public string LauncherFix4 { get { return "4. Navegue ate RADS\\solutions\\lol_game_client_sln\\releases\\"; } }
        public string LauncherFix5 { get { return "5. Delete todas as pastas encontradas exceto: 0.0.1.152"; } }
        public string ChangingGameConfig { get { return "Mudando as Configurações de Jogo."; } }
        public string LoadingAccounts { get { return "Carregando contas."; } }
        public string MaximumBotsRunning { get { return "Número maximo de bots rodando: {0}"; } }
        public string YouMayHaveAnIssueInAccountsFile { get { return "Voce pode ter um problema no seu accounts.txt"; } }
        public string AccountsStructure { get { return "Estrutura de conta CONTA|SENHA|REGIaO|TIPO_DE_QUEUE|e_LIDER"; } }
        public string ErrorGetGarenaToken { get { return "Erro Get Garena Token"; } }
        public string ErrorLeagueGameCfgRegular { get { return "Regular League game.cfg Erro: Se estiver usando VMWare Shared Folder, tenha certeza que nao esta configurado para Somente Leitura.\nException: {0}"; } }
        public string ErrorLeagueGameCfgGarena { get { return "Garena League game.cfg Erro: Se estiver usando VMWare Shared Folder, tenha certeza que nao esta configurado para Somente Leitura.\nException: {0}"; } }
        public string NoMoreAccountsToLogin { get { return "Nao ha mais contas para conectar."; } }
        public string GameModeInvalid { get { return "Modo de Jogo Invalido, tenha certeza que voce esta usando um dos seguintes modos."; } }
        public string WillShutdownOnceCurrentMatchEnds { get { return "Ira desligar assim que a partida atual acabar."; } }
        public string EzBotGameStatus { get { return "ezBot - {0} Total - {1} Vitoria - {2} Derrota"; } }
        public string AcceptingLobbyInvite { get { return "{0}: Aceitando convite de grupo."; } }
        public string AllPlayersAccepted { get { return "Todos jogadores foram aceitos, começando a fila."; } }
        public string PlayersAcceptedCount { get { return "{0}/{1} jogador(es) aceitos, esperando ate todos serem aceitos."; } }
        public string EnteringChampionSelect { get { return "{0}: Entrando na seleçao de campeões."; } }
        public string YouAreInChampionSelect { get { return "{0}: Voce esta selecionando um campeao."; } }
        public string SelectedChampion { get { return "{1}: Campeao selecionado: {0}."; } }
        public string WaitingForOtherPlayersLockin { get { return "{0}: Esperando por outros jogadores trancarem."; } }
        public string ChampionNotAvailable { get { return "{1}: Campeao '{0}' nao e possuído, nao esta livre para jogar ou ja foi escolhido."; } }
        public string WaitingChampSelectTimer { get { return "{0}: Esperando o tempo de seleçao chegar a 0."; } }
        public string YouAreInQueue { get { return "{0}: Voce esta na fila."; } }
        public string ReQueued { get { return "{1}: Entrando novamente na fila: {0}."; } }
        public string QueuePopped { get { return "{0}: Jogo encontrado."; } }
        public string AcceptedQueue { get { return "{0}: Jogo aceito!"; } }
        public string YouHaveLeaverBuster { get { return "Voce foi penalizado por sair."; } }
        public string LaunchingLeagueOfLegends { get { return "{0}: Abrindo League of Legends."; } }
        public string ClosingGameClient { get { return "{0}: Fechando o jogo."; } }
        public string InQueueAs { get { return "{1}: Na Fila: {0}."; } }
        public string QueueFailedReason { get { return "Jogo falhou, motivo: {0}."; } }
        public string LeaverBusterTaintedWarningError { get { return "Penalidade por Saída  Tainted Warning error:\n{0}"; } }
        public string WaitingDodgeTimer { get { return "Esperando o tempo para fila apos Esquiva: {0} minutes!"; } }
        public string WaitingLeaverTimer { get { return "Esperando o tempo para fila apos Saída: {0} minutes!"; } }
        public string JoinedLowPriorityQueue { get { return "Entrou em uma fila de baixa prioridade! as {0}."; } }
        public string ErrorJoiningLowPriorityQueue { get { return "Ocorreu um erro ao entrar na fila de baixa prioridade.\nDisconnecting."; } }
        public string ErrorOccured { get { return "Um erro ocorreu:\n{0}"; } }
        public string RestartingLeagueOfLegends { get { return "{0}: Reiniciando League of Legends."; } }
        public string RestartingLeagueOfLegendsAt { get { return "{1}: Reiniciando League of Legends em {0} favor aguardar."; } }
        public string PositionInLoginQueue { get { return "Posiçao na fila de acesso: {0}."; } }
        public string LoggingIntoAccount { get { return "Acessando a sua conta..."; } }
        public string SummonerDoesntExist { get { return "Invocador nao encontrado na conta."; } }
        public string CreatingSummoner { get { return "Criando Invocador..."; } }
        public string CreatedSummoner { get { return "Invocador criado: {0}."; } }
        public string AlreadyMaxLevel { get { return "Invador: {0} ja esta no nível maximo."; } }
        public string LogIntoNewAccount { get { return "Entrar em uma nova conta."; } }
        public string BuyingXpBoost { get { return "Comprando Boost de XP."; } }
        public string CouldntBuyBoost { get { return "Nao foi possível comprar o Boost:\n{0}"; } }
        public string Normal5Requirements { get { return "e preciso ser nível 3 antes da fila NORMAL_5X5."; } }
        public string JoinCoopBeginnerUntil { get { return "Utilize a fila Co-Op vs AI (Beginner) ate {0}."; } }
        public string NeedLevel6BeforeAram { get { return "e preciso ser nível 6 antes da fila ARAM."; } }
        public string NeedLevel7Before3v3 { get { return "e preciso ser nível 7 antes da fila NORMAL_3X3."; } }
        public string Welcome { get { return "Bem vindo {0} - lvl ({1}) IP: ({2}) - XP: ({3} / {4})."; } }
        public string SendingGameInvites { get { return "Enviando convite de jogo."; } }
        public string WaitingGameInviteFrom { get { return "Esperando convite de jogo de {0}."; } }
        public string LevelUp { get { return "{1}: Subiu de nível: {0}."; } }
        public string CurrentRp { get { return "Seu RP atual: {0}."; } }
        public string CurrentIp { get { return "Seu IP atual: {0}."; } }
        public string CharacterReachedMaxLevel { get { return "Seu personagem chegou ao nível maximo: {0}."; } }
        public string DownloadingMasteries { get { return "{0}: Baixando masterias de champion.gg."; } }
        public string UpdatingMasteries { get { return "{0}: Atualizando masteiras."; } }
        public string Disconnected { get { return "Desconectado."; } }
        public string BoughtXpBoost3Days { get { return "Comprou 'Boost de XP: 3 Dias'!"; } }
    }
}