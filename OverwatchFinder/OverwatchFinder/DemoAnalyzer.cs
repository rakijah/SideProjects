using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DemoInfo;
using ICSharpCode.SharpZipLib.BZip2;

namespace OverwatchFinder
{
    static class DemoAnalyzer
    {
        private static string _ArchiveFileName = "ow.bz2";
        private static string _FileName = "ow.dem";
        private static DemoParser _dp;
        private static ProgressBar progressBar;
        public static Dictionary<int, PlayerStats[]> DemoStats = new Dictionary<int, PlayerStats[]>();
        public static bool Finished = false;

        public static void StartDownload(string URL)
        {
            DeleteFiles();

            Console.Write("Downloading... ");
            progressBar = new ProgressBar();
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                wc.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                wc.DownloadFileTaskAsync(new Uri(URL), _ArchiveFileName);
            }
        }

        public static void DeleteFiles()
        {
            if (File.Exists(_ArchiveFileName))
                File.Delete(_ArchiveFileName);

            if (File.Exists(_FileName))
                File.Delete(_FileName);
        }

        private static void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            progressBar.Dispose();
            Console.WriteLine("finished.");
            Console.Write("Uncompressing... ");
            using (var fStream = File.Create(_FileName))
            {
                BZip2.Decompress(File.OpenRead(_ArchiveFileName), fStream, true);
            }
            Console.WriteLine("finished.");
            Console.Write("Parsing demo... ");
            using (var demoStream = File.OpenRead(_FileName))
            {
                _dp = new DemoParser(demoStream);
                _dp.RoundStart += _dp_RoundStart;
                _dp.ParseHeader();
                _dp.ParseToEnd();
                _dp.Dispose();
            }
            Console.WriteLine("finished.");
            Finished = true;
        }

        private static int dp_round;

        private static void _dp_RoundStart(object sender, RoundStartedEventArgs e)
        {
            dp_round = _dp.TScore + _dp.CTScore;
            if (DemoStats.ContainsKey(dp_round))
                DemoStats.Remove(dp_round);

            PlayerStats[] roundStats = new PlayerStats[_dp.PlayingParticipants.Count()];
            int i = 0;
            foreach (Player p in _dp.PlayingParticipants)
            {
                roundStats[i++] = new PlayerStats(p.Name, p.SteamID.ToString(), p.AdditionaInformations.Kills, p.AdditionaInformations.Assists, p.AdditionaInformations.Deaths);
            }
            DemoStats.Add(dp_round, roundStats);
        }

        //To avoid spamming/blocking
        static int counter = 0;

        private static void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (++counter % 20 == 0)
            {
                progressBar.Report(e.ProgressPercentage / 100.0);
            }
        }
    }
}
