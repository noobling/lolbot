using BananaLib;
using ezBot.Languages;
using ezBot.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ezBot
{
    internal class Program
    {
        public static ArrayList accounts = new ArrayList();
        public static ArrayList accountsNew = new ArrayList();
        public static int maxBots = 1;
        public static bool printGameStats = false;
        public static bool replaceConfig = false;
        public static bool randomChampionPick = false;
        public static string firstChampionPick = "";
        public static string secondChampionPick = "";
        public static string thirdChampionPick = "";
        public static string fourthChampionPick = "";
        public static string fifthChampionPick = "";
        public static int maxLevel = 30;
        public static bool randomSpell = false;
        public static string spell1 = "flash";
        public static string spell2 = "ignite";
        public static string LoLVersion = "";
        public static bool buyExpBoost = false;
        public static int delay1 = 1;
        public static int delay2 = 1;
        public static int lolHeight = 200;
        public static int lolWidth = 300;
        public static bool LOWPriority = true;
        public static string lolPath;
        public static string lolGarenaPath;
        public static bool lowGraphics = false;
        //Friends...
        public static bool queueWithFriends = false;
        public static string leaderName = "";
        public static string firstFriend = "";
        public static string secondFriend = "";
        public static string thirdFriend = "";
        public static string fourthFriend = "";

        public static string EzBotVersion = "Unknown";
        public static Action<string, string, string> OnInvite;
        public static int shutdownAfterXMatch = 0;
        public static bool shutdownComputer = false;
        public static int currentMatchCount = 0;
        public static string language = "en";
        public static ITranslator Translator;
        public static List<string> FriendsChampions = new List<string>();

        private static void LoadTranslator()
        {
            switch(language.ToLower())
            {
                default:
                case "english":
                case "en":
                {
                    Translator = new EnglishTranslator();
                }
                break;
                case "french":
                case "fr":
                {
                    Translator = new FrenchTranslator();
                }
                break;
                case "korean":
                case "kr":
                {
                    Console.OutputEncoding = Encoding.Default;
                    Translator = new KoreanTranslator();
                }
                break;
                case "turkish":
                case "tr":
                {
                    Translator = new TurkishTranslator();
                }
                break;
                case "portuguese":
                case "pt":
                {
                    //Console.OutputEncoding = Encoding.GetEncoding("ISO-8859-1");
                    Translator = new PortugueseTranslator();
                }
                break;
                case "polish":
                case "pl":
                {
                    Translator = new PolishTranslator();
                }
                break;
            }
        }

        private static void Main(string[] args)
        {
            EzBotVersion = LoadEzBotVersion();
            var remoteVersion = LoadRemoteVersion();
            if (string.IsNullOrEmpty(EzBotVersion) || string.IsNullOrEmpty(remoteVersion) || EzBotVersion != remoteVersion)
            {
                Tools.ConsoleMessage("New update available: https://github.com/fkingnoobgg/lolbot/releases (download the *.zip)", ConsoleColor.DarkRed);
            }
            
            LoadLeagueVersion();

            LoadConfigs();

            LoadTranslator();

            Console.Title = "ezBot";
            Tools.TitleMessage(string.Format(Translator.EzBot, LoLVersion.Substring(0, 4)));
            Tools.TitleMessage("Made by Tryller and Hesa, maintained by fkingnoobgg");
            Tools.TitleMessage(string.Format(Translator.Version, EzBotVersion));
            Tools.ConsoleMessage("For help please PM me or use thread on elobuddy", ConsoleColor.Magenta);
            Tools.ConsoleMessage("Garena needs fixing please report errors to me", ConsoleColor.Cyan);
            Tools.ConsoleMessage("Hesa has a new bot here: https://www.hesabot.com/", ConsoleColor.Cyan);
            Tools.ConsoleMessage("Please report issue(s) on elobuddy or github", ConsoleColor.Cyan);

            if (!IsUserAdministrator() && replaceConfig)
            {
                Tools.ConsoleMessage(Translator.AdministratorRequired, ConsoleColor.Red);
                Console.ReadKey(true);
                return;
            }

            Tools.ConsoleMessage(Translator.ConfigLoaded, ConsoleColor.White);
            
            try
            {
                var dir = Directory.EnumerateDirectories(lolPath + "RADS\\solutions\\lol_game_client_sln\\releases\\").OrderBy(f => new DirectoryInfo(f).CreationTime).Last() + "\\deploy\\";
            }catch(Exception)
            {
                Tools.ConsoleMessage(Translator.LauncherPathInvalid, ConsoleColor.Red);
                Tools.ConsoleMessage(Translator.PleaseTryThis, ConsoleColor.Red);
                Tools.ConsoleMessage(Translator.LauncherFix1, ConsoleColor.Red);
                Tools.ConsoleMessage(Translator.LauncherFix2, ConsoleColor.Red);
                Tools.ConsoleMessage(Translator.LauncherFix3, ConsoleColor.Red);
                Tools.ConsoleMessage(Translator.LauncherFix4, ConsoleColor.Red);
                Tools.ConsoleMessage(Translator.LauncherFix5, ConsoleColor.Red);
            }
            
            if (replaceConfig)
            {
                Tools.ConsoleMessage(Translator.ChangingGameConfig, ConsoleColor.White);
                ChangeGameConfig();
            }
            Tools.ConsoleMessage(Translator.LoadingAccounts, ConsoleColor.White);
            LoadAccounts();
            int num = 0;
            lock(accounts)
            {
                foreach (string account in accounts)
                {
                    try
                    {
                        accountsNew.RemoveAt(0);
                        string[] strArray = account.Split(new string[1] { "|" }, StringSplitOptions.None);
                        ++num;
                        var isLeader = strArray[4] != null ? (strArray[4].ToLower() == "leader" ? true : false) : true;
                        if (strArray[3] != null)
                        {
                            Generator.CreateRandomThread(delay1, delay2);
                            string queueType = strArray[3];

                            var region = Tools.ParseEnum<Region>(strArray[2].ToUpper());
                            var password = strArray[1];
                            //if (region.UseGarena())
                            //{
                            //    password = GetGarenaToken();
                            //}
                            if (IsGameModeValid(queueType))
                            {
                                ezBot ezBot = new ezBot(strArray[0], password, strArray[2].ToUpper(), queueType.ToUpper(), LoLVersion, isLeader);
                            }
                        }
                        else
                        {
                            Generator.CreateRandomThread(delay1, delay2);
                            string queueType = "ARAM";
                            ezBot ezBot = new ezBot(strArray[0], strArray[1], strArray[2].ToUpper(), queueType.ToUpper(), LoLVersion, isLeader);
                        }
                        if (num == maxBots)
                        {
                            Tools.ConsoleMessage(string.Format(Translator.MaximumBotsRunning, maxBots), ConsoleColor.Red);
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Tools.ConsoleMessage(ex.Message + " " + ex.StackTrace, ConsoleColor.Green);
                        Tools.ConsoleMessage(Translator.YouMayHaveAnIssueInAccountsFile, ConsoleColor.Red);
                        Tools.ConsoleMessage(Translator.AccountsStructure, ConsoleColor.Red);
                        Console.ReadKey(true);
                    }
                }
            }
            
            while(true)
                Console.ReadKey(true);
        }

        public static string LoadEzBotVersion()
        {
            try
            {
                using (StreamReader reader = new StreamReader("version.txt"))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Tools.Log(ex.StackTrace);
            }
            return null;
        }

        public static string LoadRemoteVersion()
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
                webClient.Headers.Add("Cache-Control", "no-cache");

                return webClient.DownloadString("https://raw.githubusercontent.com/fkingnoobgg/lolbot/master/version.txt");
            }
            catch (Exception ex)
            {
                Tools.Log(ex.StackTrace);
            }
            return null;
        }

        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            WindowsIdentity user = null;
            try
            {
                //get the currently logged in user
                user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
                Tools.Log(ex.StackTrace);
            }
            catch (Exception ex)
            {
                isAdmin = false;
                Tools.Log(ex.StackTrace);
            }
            finally
            {
                if (user != null)
                    user.Dispose();
            }
            return isAdmin;
        }

        private static string GetGarenaToken()
        {
            string s1 = "";
            bool token = false;
            do
            {
                foreach (var process in Process.GetProcessesByName("lol"))
                {
                    try
                    {

                        s1 = GetCommandLine(process);
                        foreach (var p1 in Process.GetProcessesByName("lolclient"))
                        {
                            p1.Kill();
                        }
                        process.Kill();
                        s1 = s1.Substring(1);
                        token = true;
                    }
                    catch (Win32Exception ex)
                    {
                        Console.WriteLine(Translator.ErrorGetGarenaToken);
                        if ((uint)ex.ErrorCode != 0x80004005)
                        {
                            throw;
                        }
                    }catch(Exception ex)
                    {
                        Console.WriteLine("Exception:\n" + ex);
                    }
                }
            } while (!token);
            return s1;

        }

        private static string GetCommandLine(Process process)
        {
            var commandLine = new StringBuilder("");
            using (var searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
            {
                foreach (var @object in searcher.Get())
                {
                    commandLine.Append(@object["CommandLine"]);
                }
            }
            return commandLine.ToString();
        }

        public static void LoadLeagueVersion()
        {
            LoLVersion = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "configs\\version.txt").ReadLine();
        }

        private static void ChangeGameConfig()
        {
            if(!string.IsNullOrEmpty(lolPath))
            {
                try
                {
                    var configFileIni = new IniFile(lolPath + "Config\\game.cfg");
                    configFileIni.Write("General", "WindowMode", "1");
                    configFileIni.Write("General", "Height", lolHeight.ToString());
                    configFileIni.Write("General", "Width", lolWidth.ToString());
                    if(lowGraphics)
                    {
                        configFileIni.Write("Performance", "GraphicsSlider", "0");
                        configFileIni.Write("Performance", "ShadowsEnabled", "0");
                        configFileIni.Write("Performance", "CharacterInking", "0");
                        configFileIni.Write("Performance", "EnableHUDAnimations", "0");
                        configFileIni.Write("Performance", "PerPixelPointLighting", "0");
                        configFileIni.Write("Performance", "EnableParticleOptimizations", "0");
                        configFileIni.Write("Performance", "BudgetOverdrawAverage", "1");
                        configFileIni.Write("Performance", "BudgetSkinnedVertexCount", "10000");
                        configFileIni.Write("Performance", "BudgetSkinnedDrawCallCount", "50");
                        configFileIni.Write("Performance", "BudgetTextureUsage", "1024");
                        configFileIni.Write("Performance", "BudgetVertexCount", "15000");
                        configFileIni.Write("Performance", "BudgetTriangleCount", "5000");
                        configFileIni.Write("Performance", "BudgetDrawCallCount", "100");
                        configFileIni.Write("Performance", "EnableGrassSwaying", "0");
                        configFileIni.Write("Performance", "EnableFXAA", "0");
                        configFileIni.Write("Performance", "AdvancedShader", "0");
                        configFileIni.Write("Performance", "FrameCapType", "3");
                        configFileIni.Write("Performance", "ShadowQuality", "0");
                        configFileIni.Write("Performance", "EffectsQuality", "0");
                        configFileIni.Write("Performance", "GammaEnabled", "0");
                        configFileIni.Write("Performance", "Full3DModeEnabled", "0");
                        configFileIni.Write("Performance", "EnvironmentQuality", "0");
                        configFileIni.Write("Performance", "CharacterQuality", "0");
                        configFileIni.Write("Performance", "AutoPerformanceSettings", "0");
                    }
                }
                catch (Exception ex)
                {
                    Tools.Log(string.Format(Translator.ErrorLeagueGameCfgRegular, ex.Message));
                }
            }
            if (string.IsNullOrEmpty(lolGarenaPath)) return;
            try
            {
                var configFileIni = new IniFile(lolGarenaPath + "Game\\Config\\game.cfg");
                configFileIni.Write("General", "WindowMode", "1");
                configFileIni.Write("General", "Height", lolHeight.ToString());
                configFileIni.Write("General", "Width", lolWidth.ToString());
            }
            catch (Exception ex)
            {
                Tools.Log(string.Format(Translator.ErrorLeagueGameCfgGarena, ex.Message));
            }
        }

        public static void LognNewAccount()
        {
            if (accounts.Count == 0)
            {
                Tools.ConsoleMessage(Translator.NoMoreAccountsToLogin, ConsoleColor.Red);
                return;
            }
            
            accounts = accountsNew;
            int num = 0;
            
            foreach (string account in accountsNew)
            {
                accounts.RemoveAt(0);
                string[] strArray = account.Split(new string[1] { "|" }, StringSplitOptions.None);
                ++num;
                var isLeader = string.IsNullOrEmpty(strArray[4]) ? true : (strArray[4].ToLower() == "leader" ? true : false);
                if (strArray[3] != null)
                {
                    Generator.CreateRandomThread(Program.delay1, Program.delay2);
                    string queueType = strArray[3];
                    if (IsGameModeValid(queueType))
                    {
                        ezBot ezBot = new ezBot(strArray[0], strArray[1], strArray[2].ToUpper(), queueType.ToUpper(), LoLVersion, isLeader);
                    }
                }
                else
                {
                    Generator.CreateRandomThread(delay1, delay2);
                    string queueType = "ARAM";
                    ezBot ezBot = new ezBot(strArray[0], strArray[1], strArray[2].ToUpper(), queueType.ToUpper(), LoLVersion, isLeader);
                }
                if (num == maxBots)
                {
                    Tools.ConsoleMessage(string.Format(Translator.MaximumBotsRunning, maxBots), ConsoleColor.Red);
                    break;
                }
            }
        }

        public static bool IsGameModeValid(string gameMode)
        {
            switch(gameMode.ToUpper())
            {
                case "INTRO_BOT": return true;
                case "BEGINNER_BOT": return true;
                case "MEDIUM_BOT": return true;
                case "BOT_3X3": return true;
                case "NORMAL_5X5": return true;
                case "NORMAL_3X3": return true;
                case "ARAM": return true;
                case "RANKED_SOLO_5X5": return true;

                case "ASCENSION_5X5": return true;
                case "HEXAKILL": return true;
                case "BOT_URF_5X5": return true;
                case "URF_5X5": return true;
                case "ONEFORALL_MIRRORMODE_5X5": return true;
                case "BILGEWATER_ARAM_5X5": return true;
                case "KING_PORO_5X5": return true;
                case "BILGEWATER_5X5": return true;
                case "SIEGE": return true;
                case "DEFINITELY_NOT_DOMINION_5X5": return true;
                case "ARURF_5X5": return true;
            }

            Tools.ConsoleMessage(Translator.GameModeInvalid, ConsoleColor.Red);
            Tools.ConsoleMessage("INTRO_BOT", ConsoleColor.Red);
            Tools.ConsoleMessage("BEGINNER_BOT", ConsoleColor.Red);
            Tools.ConsoleMessage("MEDIUM_BOT", ConsoleColor.Red);
            Tools.ConsoleMessage("BOT_3X3", ConsoleColor.Red);
            Tools.ConsoleMessage("NORMAL_5X5", ConsoleColor.Red);
            Tools.ConsoleMessage("NORMAL_3X3", ConsoleColor.Red);
            Tools.ConsoleMessage("ARAM", ConsoleColor.Red);
            Tools.ConsoleMessage("RANKED_SOLO_5x5", ConsoleColor.Red);

            Tools.ConsoleMessage("ASCENSION_5X5", ConsoleColor.Red);
            Tools.ConsoleMessage("HEXAKILL", ConsoleColor.Red);
            Tools.ConsoleMessage("BOT_URF_5X5", ConsoleColor.Red);
            Tools.ConsoleMessage("URF_5X5", ConsoleColor.Red);
            Tools.ConsoleMessage("ONEFORALL_MIRRORMODE_5X5", ConsoleColor.Red);
            Tools.ConsoleMessage("BILGEWATER_ARAM_5X5", ConsoleColor.Red);
            Tools.ConsoleMessage("KING_PORO_5X5", ConsoleColor.Red);
            Tools.ConsoleMessage("BILGEWATER_5X5", ConsoleColor.Red);
            Tools.ConsoleMessage("SIEGE", ConsoleColor.Red);
            Tools.ConsoleMessage("DEFINITELY_NOT_DOMINION_5X5", ConsoleColor.Red);
                Tools.ConsoleMessage("ARURF_5X5", ConsoleColor.Red);

            return false;
        }

        public static void LoadConfigs()
        {
            try
            {
                IniFile iniFile = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "configs\\settings.ini");
                try
                {
                    lolPath = iniFile.Read("GENERAL", "LauncherPath");
                    if (string.IsNullOrEmpty(lolPath))
                    {
                        iniFile.Write("GENERAL", "LauncherPath", "C:\\Riot Games\\League of Legends\\");
                        lolPath = "C:\\Riot Games\\League of Legends\\";
                    }
                }
                catch (Exception ex)
                {
                    iniFile.Write("GENERAL", "LauncherPath", "C:\\Riot Games\\League of Legends\\");
                    lolPath = "C:\\Riot Games\\League of Legends\\";
                    Tools.Log(ex.StackTrace);
                }
                try
                {
                    lolPath = iniFile.Read("GENERAL", "LauncherPath");
                    maxBots = Convert.ToInt32(iniFile.Read("GENERAL", "MaxBots"));
                    maxLevel = Convert.ToInt32(iniFile.Read("GENERAL", "MaxLevel"));
                    randomSpell = Convert.ToBoolean(iniFile.Read("GENERAL", "RandomSpell"));
                    spell1 = iniFile.Read("GENERAL", "Spell1").ToUpper();
                    spell2 = iniFile.Read("GENERAL", "Spell2").ToUpper();
                    delay1 = Convert.ToInt32(iniFile.Read("ACCOUNT", "MinDelay"));
                    delay2 = Convert.ToInt32(iniFile.Read("ACCOUNT", "MaxDelay"));
                    buyExpBoost = Convert.ToBoolean(iniFile.Read("ACCOUNT", "BuyExpBoost"));
                    randomChampionPick = Convert.ToBoolean(iniFile.Read("CHAMPIONS", "PickRandomlyFromThisList"));
                    firstChampionPick = iniFile.Read("CHAMPIONS", "FirstChampionPick");
                    secondChampionPick = iniFile.Read("CHAMPIONS", "SecondChampionPick");
                    thirdChampionPick = iniFile.Read("CHAMPIONS", "ThirdChampionPick");
                    fourthChampionPick = iniFile.Read("CHAMPIONS", "FourthChampionPick");
                    fifthChampionPick = iniFile.Read("CHAMPIONS", "FifthChampionPick");
                    replaceConfig = Convert.ToBoolean(iniFile.Read("LOLSCREEN", "ReplaceLoLConfig"));
                    lolHeight = Convert.ToInt32(iniFile.Read("LOLSCREEN", "SreenHeight"));
                    lolWidth = Convert.ToInt32(iniFile.Read("LOLSCREEN", "SreenWidth"));
                    LOWPriority = Convert.ToBoolean(iniFile.Read("LOLSCREEN", "LOWPriority"));
                }
                catch(Exception ex)
                {
                    Tools.ConsoleMessage(ex.StackTrace, ConsoleColor.Red, false);
                }

                try
                {
                    lowGraphics = Convert.ToBoolean(iniFile.Read("LOLSCREEN", "LOWGraphics"));
                }
                catch(Exception ex)
                {
                    iniFile.Write("LOLSCREEN", "LOWGraphics", "false");
                    Tools.Log(ex.StackTrace);
                }

                try
                {
                    queueWithFriends = Convert.ToBoolean(iniFile.Read("FRIENDS", "QueueWithFriends"));
                    leaderName = iniFile.Read("FRIENDS", "LeaderName");
                    firstFriend = iniFile.Read("FRIENDS", "FirstFriend");
                    secondFriend = iniFile.Read("FRIENDS", "SecondFriend");
                    thirdFriend = iniFile.Read("FRIENDS", "ThirdFriend");
                    fourthFriend = iniFile.Read("FRIENDS", "FourthFriend");
                }
                catch(Exception ex)
                {
                    iniFile.Write("FRIENDS", "QueueWithFriends", "false");
                    iniFile.Write("FRIENDS", "LeaderName", "");
                    iniFile.Write("FRIENDS", "FirstFriend", "");
                    iniFile.Write("FRIENDS", "SecondFriend", "");
                    iniFile.Write("FRIENDS", "ThirdFriend", "");
                    iniFile.Write("FRIENDS", "FourthFriend", "");
                    Tools.Log(ex.StackTrace);
                }
                try
                {
                    printGameStats = Convert.ToBoolean(iniFile.Read("GENERAL", "PrintGameStats"));
                }
                catch (Exception ex)
                {
                    iniFile.Write("GENERAL", "PrintGameStats", "false");
                    Tools.Log(ex.StackTrace);
                }
                try
                {
                    lolGarenaPath = iniFile.Read("GENERAL", "GarenaLoLFolder");
                    if(string.IsNullOrEmpty(lolGarenaPath))
                    {
                        iniFile.Write("GENERAL", "GarenaLoLFolder", "C:\\GarenaLoL\\GameData\\Apps\\LoL\\");
                        lolGarenaPath = "C:\\GarenaLoL\\GameData\\Apps\\LoL\\";
                    }
                }
                catch (Exception ex)
                {
                    iniFile.Write("GENERAL", "GarenaLoLFolder", "C:\\GarenaLoL\\GameData\\Apps\\LoL\\");
                    lolGarenaPath = "C:\\GarenaLoL\\GameData\\Apps\\LoL\\";
                    Tools.Log(ex.StackTrace);
                }
                try
                {
                    shutdownAfterXMatch = Convert.ToInt32(iniFile.Read("SHUTDOWN", "AfterXGames"));
                    shutdownComputer = Convert.ToBoolean(iniFile.Read("SHUTDOWN", "AlsoCloseComputer"));
                }
                catch(Exception ex)
                {
                    iniFile.Write("SHUTDOWN", "AfterXGames", "0");
                    iniFile.Write("SHUTDOWN", "AlsoCloseComputer", "false");
                    Tools.Log(ex.StackTrace);
                }

                try
                {
                    language = iniFile.Read("GENERAL", "Language");
                    if(string.IsNullOrEmpty(language))
                    {
                        iniFile.Write("GENERAL", "Language", "en");
                        language = "en";
                    }
                }
                catch (Exception ex)
                {
                    iniFile.Write("GENERAL", "Language", "en");
                    Tools.Log(ex.StackTrace);
                    language = "en";
                }
            }
            catch (Exception ex)
            {
                Tools.Log(ex.Message);
                Thread.Sleep(10000);
                Application.Exit();
            }
        }

        public static void LoadAccounts()
        {
            lock (accounts)
            {
                lock (accountsNew)
                {
                    TextReader textReader = (TextReader)File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "configs\\accounts.txt");
                    string str;
                    while ((str = textReader.ReadLine()) != null)
                    {
                        accounts.Add((object)str);
                        accountsNew.Add((object)str);
                    }
                    textReader.Close();
                }
            }
        }

        private static int victory = 0;
        private static int defeat = 0;
        private static int gameTerminated;
        public static bool DontQueue = false;

        public static void GameStarted()
        {
            currentMatchCount++;
        }

        public static void GameTerminated()
        {
            gameTerminated++;
            currentMatchCount--;
            if (shutdownAfterXMatch != 0 && shutdownAfterXMatch >= gameTerminated)
            {
                if (currentMatchCount != 0)
                {
                    DontQueue = true;
                    Tools.ConsoleMessage(Translator.WillShutdownOnceCurrentMatchEnds, ConsoleColor.Yellow);
                    return;
                }
                if (shutdownComputer)
                {
                    var processInfo = new ProcessStartInfo()
                    {
                        Arguments = "-s -t 30 ",
                        FileName = "shutdown.exe",
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    Process.Start(processInfo);
                }
                Environment.Exit(0);
            }
        }
        
        public static void AddGame(bool won)
        {
            if (won)
            {
                victory++;
            }
            else
            {
                defeat++;
            }
            var total = (victory + defeat);
            Console.Title = string.Format(Translator.EzBotGameStatus, total, victory, defeat);
        }
    }
}