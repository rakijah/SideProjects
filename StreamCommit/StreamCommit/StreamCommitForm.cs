using LibGit2Sharp;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StreamCommit
{
    public partial class StreamCommitForm : Form
    {
        public Settings Settings;
        private Committer _committer;
        private bool IsReadyToRun => !string.IsNullOrWhiteSpace(Settings.FolderToWatch) && Settings.HasCredentials;

        public StreamCommitForm()
        {
            InitializeComponent();
            Settings = new Settings();
            Settings.PropertyChanged += SettingsChanged;
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
            lblFolderToWatch.Text = Settings.FolderToWatchTopDirectory;
        }

        private void StatusChanged(string newStatus)
        {
            string newText = $"Status: {newStatus}";
            if (lblStatus.InvokeRequired)
            {
                lblStatus.BeginInvoke((MethodInvoker)delegate () { lblStatus.Text = newText; });
            }
            else
            {
                lblStatus.Text = newText;
            }
        }

        private void ToggleRun(object sender, EventArgs e)
        {
            if (!IsReadyToRun)
                return;

            if (_committer == null)
            {
                _committer = new Committer();
                _committer.Path = Settings.FolderToWatch;
                _committer.CommitInterval = Settings.CommitInterval;
            }

            if (_committer.Running)
            {
                _committer.StopMonitoring();
                btnCredentials.Enabled = true;
                btnFolderToWatch.Enabled = true;
                tbCommitInterval.Enabled = true;
            }
            else
            {
                _committer.StartMonitoring();
                btnCredentials.Enabled = false;
                btnFolderToWatch.Enabled = false;
                tbCommitInterval.Enabled = false;
            }

            btnToggleRun.Text = (_committer.Running ? "Stop" : "Start");
        }

        private void PickFolderToWatch(object sender, EventArgs e)
        {
            var result = fbdFolderToWatch.ShowDialog();
            if (result != DialogResult.OK)
                return;

            if (!Repository.IsValid(fbdFolderToWatch.SelectedPath))
            {
                MessageBox.Show($"\"{fbdFolderToWatch.SelectedPath}\" is not a valid Git repository.");
                return;
            }
            Settings.FolderToWatch = fbdFolderToWatch.SelectedPath;
            lblFolderToWatch.Text = Settings.FolderToWatchTopDirectory;
            UpdateFolderToWatch();
        }

        private void btnCredentials_Click(object sender, EventArgs e)
        {
            var result = new EnterCredentials().ShowDialog();
            lblLoggedIn.Text = $"User: {(string.IsNullOrWhiteSpace(Settings.GitUsername) ? "-" : Settings.GitUsername)}";
        }

        private void SettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_committer != null && _committer.Running)
                return;

            btnToggleRun.Enabled = IsReadyToRun;
        }

        private void tbCommitInterval_TextChanged(object sender, EventArgs e)
        {
            int tmp;
            if (int.TryParse(tbCommitInterval.Text, out tmp))
            {
                Settings.CommitInterval = tmp * 1000; //convert to ms
                tbCommitInterval.BackColor = Color.White;
            }
            else
            {
                tbCommitInterval.BackColor = Color.FromArgb(255, 200, 200);
            }
        }

        private void StreamCommitForm_Load(object sender, EventArgs e)
        {
            lblFolderToWatch.Text = Settings.FolderToWatchTopDirectory;
            lblLoggedIn.Text = $"User: {(string.IsNullOrWhiteSpace(Settings.GitUsername) ? "-" : Settings.GitUsername)}";
            tbCommitInterval.Text = (Settings.CommitInterval / 1000).ToString();
            _committer = new Committer();
            _committer.Path = Settings.FolderToWatch;
            _committer.CommitInterval = Settings.CommitInterval;
            _committer.StatusChanged += StatusChanged;

            btnToggleRun.Enabled = IsReadyToRun;
        }
    }
}