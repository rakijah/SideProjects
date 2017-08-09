using System;
using System.Windows.Forms;

namespace StreamCommit
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += (o, e) => { if (Settings.Instance != null) Settings.Instance.Save(); };
            Application.Run(new StreamCommitForm());
        }
    }
}