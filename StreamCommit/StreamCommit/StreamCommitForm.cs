using LibGit2Sharp;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamCommit
{
    public partial class StreamCommitForm : MetroForm
    {
        private Committer _committer;

        public StreamCommitForm()
        {
            InitializeComponent();
            UpdateFolderToWatch();
            FormClosing += (o, e) =>
            {
                if (_committer != null)
                    _committer.Dispose();
            };
        }

        private void UpdateFolderToWatch()
        {
            if (string.IsNullOrWhiteSpace(Settings.FolderToWatch))
            {
                btnToggleRun.Enabled = false;
                return;
            }
                

            if (!Repository.IsValid(Settings.FolderToWatch))
            {
                MessageBox.Show("The folder you selected is not a valid Git repository.");
                Settings.FolderToWatch = "";
                return;
            }
            tbFolderToWatch.Text = Settings.FolderToWatch;

            _committer = new Committer(Settings.FolderToWatch, 10000);
            _committer.StatusChanged += StatusChanged;

            btnToggleRun.Enabled = true;
        }

        private void StatusChanged(string newStatus)
        {
            if(lblStatus.InvokeRequired)
            {
                lblStatus.BeginInvoke((MethodInvoker)delegate () { lblStatus.Text = newStatus; });
            }
            else
            {
                lblStatus.Text = $"Status: {newStatus}";
            }
        }

        private void ToggleRun(object sender, EventArgs e)
        {
            if (_committer == null)
            {
                return;
            }

            var btn = (MetroButton)sender;
            
            if (_committer.Running)
            {
                _committer.StopMonitoring();
            }
            else
            {
                _committer.StartMonitoring();
            }
            btn.Text = (_committer.Running ? "Stop" : "Start");
        }

        private void PickFolderToWatch(object sender, EventArgs e)
        {
            var result = fbdFolderToWatch.ShowDialog();
            if (result != DialogResult.OK)
                return;

            if(!string.IsNullOrWhiteSpace(fbdFolderToWatch.SelectedPath))
            {
                Settings.FolderToWatch = fbdFolderToWatch.SelectedPath;
                tbFolderToWatch.Text = Settings.FolderToWatch;
                UpdateFolderToWatch();
            }
        }

        private void btnCredentials_Click(object sender, EventArgs e)
        {
            if(_committer != null && _committer.Running)
            {
                MessageBox.Show("Please click \"Stop\" before entering your credentials.");
                return;
            }
            var result = new EnterCredentials().ShowDialog();
        }
    }
}
