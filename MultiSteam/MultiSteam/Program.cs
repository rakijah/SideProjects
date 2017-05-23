using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiSteam
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if(AlreadyRunning()) //dont open 2 instances at once because the database file is in use
            {
                Application.Exit();
                return;
            }

            Database.Init();
            Config.Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SettingsForm());
        }

        static bool AlreadyRunning()
        {
            return Process.GetProcessesByName("MultiSteam").Length > 1; //length will be 1 because this application is running
        }
    }
}
