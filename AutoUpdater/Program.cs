using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Threading;

namespace AutoUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checking ezBot Version");
            var localVersion = LoadVersion();
            Console.WriteLine("Current ezBot Version: " + localVersion);
            var remoteVersion = LoadRemoteVersion();
            Console.WriteLine("Remote ezBot Version: " + remoteVersion);
            if(string.IsNullOrEmpty(localVersion) || string.IsNullOrEmpty(remoteVersion) || localVersion != remoteVersion)
            {
                ShutdownEzBot();
                Console.WriteLine("Currently updating ezBot please wait.");
                var updated = DownloadUpdate();
                if (updated)
                    Console.WriteLine("You are up to date!");
                else
                    Console.WriteLine("An error occured during the update. please tell Hesa!");
            }
            else
            {
                Console.WriteLine("You are up to date!");
            }
            Process.Start("ezBot.exe");
        }
        static string LoadVersion()
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
            }
            return null;
        }

        static string LoadRemoteVersion()
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
                webClient.Headers.Add("Cache-Control", "no-cache");
                return webClient.DownloadString("https://raw.githubusercontent.com/hesa2020/HesaElobuddy/master/eZ_Source/version.txt");
            }catch(Exception ex)
            {
            }
            return null;
        }

        static bool DownloadUpdate()
        {
            try
            {
                var tempFileName = Environment.CurrentDirectory + @"\update.temp";
                using (WebClient webClient = new WebClient())
                {
                    webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
                    webClient.Headers.Add("Cache-Control", "no-cache");
                    webClient.DownloadFile("https://raw.githubusercontent.com/hesa2020/HesaElobuddy/master/eZ_Source/update.txt", tempFileName);
                }
                Thread.Sleep(1000);
                if(File.Exists(tempFileName))
                {
                    using (FileStream stream = new FileStream(tempFileName, FileMode.Open))
                    {
                        using (ZipArchive archive = new ZipArchive(stream))
                        {
                            archive.ExtractToDirectory(Environment.CurrentDirectory, true);
                        }
                    }
                    File.Delete(tempFileName);
                }
                return true;
            }
            catch(Exception ex)
            {

            }
            return false;
        }

        static void ShutdownEzBot()
        {
            Process[] processes = Process.GetProcessesByName("ezBot");
            foreach(var process in processes)
            {
                process.Kill();
                Thread.Sleep(500);
            }
        }

    }
}