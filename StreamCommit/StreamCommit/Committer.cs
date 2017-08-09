using LibGit2Sharp;
using System;
using System.Threading;

namespace StreamCommit
{
    public delegate void StatusChangedHandler(string newStatus);

    public class Committer : IDisposable
    {
        public event StatusChangedHandler StatusChanged;

        private string _path;

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                if (_path == value)
                    return;

                if (!Repository.IsValid(value))
                    throw new Exception($"Path does not lead to a valid Git repository: \"{value}\".");

                _repo = new Repository(value);
                _path = value;
            }
        }

        public bool Running { get; private set; }
        private string _status = "Not running";

        public string Status
        {
            get
            {
                return _status;
            }
            private set
            {
                if (_status == value)
                    return;
                _status = value;
                StatusChanged?.Invoke(_status);
            }
        }

        private int _commitInterval;

        public int CommitInterval
        {
            get
            {
                return _commitInterval;
            }
            set
            {
                if (_commitInterval == value)
                    return;
                _commitInterval = value;
                _timer.Interval = _commitInterval;
            }
        }

        private System.Windows.Forms.Timer _timer;
        private Repository _repo;

        private Settings _settings;

        public Committer(string path, int commitInterval)
        {
            _settings = Settings.Instance;
            Path = path;
            CommitInterval = commitInterval;
            _timer = new System.Windows.Forms.Timer
            {
                Interval = CommitInterval
            };
            _timer.Tick += CommitTimerElapsed;
        }

        public Committer()
        {
            _settings = Settings.Instance;
            _settings.PropertyChanged += SettingsChanged;
            _timer = new System.Windows.Forms.Timer();
            _timer.Tick += CommitTimerElapsed;
        }

        private void SettingsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.FolderToWatch))
            {
                Path = _settings.FolderToWatch;
            }

            if (e.PropertyName == nameof(Settings.CommitInterval))
            {
                CommitInterval = _settings.CommitInterval;
            }
        }

        public void StartMonitoring()
        {
            if (Running)
                return;

            _timer.Start();

            Running = true;
            Status = "Running";
        }

        public void StopMonitoring()
        {
            if (!Running)
                return;

            _timer.Stop();

            Running = false;
            Status = "Not running";
        }

        private void CommitTimerElapsed(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                PerformCommit();
                Thread.Sleep(1000);
                if (Running)
                {
                    Status = "Running";
                }
                else
                {
                    Status = "Not running";
                }
            }).Start();
        }

        #region Git

        private void PerformCommit()
        {
            Status = "Starting git commit";
            DateTime now = DateTime.Now;
            string timestamp = now.ToString("yyyyMMddHHmmss");
            Status = "Staging changes";
            if (!StageChanges())
            {
                Status = "Staging failed";
                return;
            }
            Status = "Creating commit";
            if (!CommitChanges(timestamp))
            {
                Status = "Nothing to commit";
                return;
            }
            Status = "Pushing changes";
            if (!PushChanges())
            {
                Status = "Pushing failed";
                return;
            }
            Status = "Successfully pushed changes";
        }

        private bool StageChanges()
        {
            try
            {
                RepositoryStatus status = _repo.RetrieveStatus();
                foreach (StatusEntry c in status.Untracked)
                {
                    _repo.Index.Add(c.FilePath);
                }

                Commands.Stage(_repo, "*");
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool CommitChanges(string commitMessage)
        {
            try
            {
                _repo.Commit(commitMessage, new Signature(_settings.GitUsername, _settings.GitEmail, DateTimeOffset.Now),
                    new Signature(_settings.GitUsername, _settings.GitEmail, DateTimeOffset.Now));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        private bool PushChanges()
        {
            try
            {
                var remote = _repo.Network.Remotes["origin"];
                var options = new PushOptions();
                options.CredentialsProvider = (url, user, cred) =>
                    new UsernamePasswordCredentials { Username = _settings.GitUsername, Password = _settings.GitPassword };
                Console.WriteLine("actually pushing");
                _repo.Network.Push(_repo.Branches["master"], options);
            }
            catch (LibGit2SharpException le)
            {
                Console.WriteLine(le.Message);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion Git

        public void Dispose()
        {
            if (_repo != null)
            {
                _repo.Dispose();
                _repo = null;
            }

            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }
    }
}