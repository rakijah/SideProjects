using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiSteam
{
    public static class SteamManager
    {
        public static bool IsRunning
        {
            get
            {
                return SteamProcess != null;
            }
        }

        public static Process SteamProcess
        {
            get
            {
                var proc = Process.GetProcessesByName("steam");
                if (proc.Length == 0) return null;

                return proc[0];
            }
        }

        public static void Terminate()
        {
            SteamProcess.Kill();
        }

        public static void LogIn(SteamUser User)
        {
            if (!Config.Exists("SteamPath"))
            {
                MessageBox.Show("Steam.exe path not set.");
                return;
            }

            if (IsRunning)
            {
                SteamProcess.Kill();
                while (IsRunning)
                {
                    Thread.Sleep(100);
                }
            }

            string arguments = string.Format("-login {0} {1}", User.Username, User.Password);
            Process.Start(Config.GetString("SteamPath"), arguments);
        }
    }
}
