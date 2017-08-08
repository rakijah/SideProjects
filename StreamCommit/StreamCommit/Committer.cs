using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamCommit
{
    public delegate void StatusChangedHandler(string newStatus);

    public class Committer : IDisposable
    {
        public event StatusChangedHandler StatusChanged;

        public string Path { get; private set; }
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
        private FileSystemWatcher _fsw;
        private int _commitInterval;
        private System.Windows.Forms.Timer _timer;
        private Repository _repo;

        private List<string> _changedFiles = new List<string>();

        public Committer(string path, int commitInterval)
        {
            if (!Repository.IsValid(path))
                throw new Exception($"Path does not lead to a valid Git repository: \"{path}\".");

            Path = path;
            _repo = new Repository(Path);

            _commitInterval = commitInterval;
        }

        public void StartMonitoring()
        {
            if (Running)
                return;

            _timer = new System.Windows.Forms.Timer
            {
                Interval = _commitInterval
            };
            _timer.Tick += CommitTimerElapsed;
            _timer.Start();

            _fsw = new FileSystemWatcher(Path)
            {
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };
            _fsw.Created += FileUpdated;
            _fsw.Changed += FileUpdated;
            _fsw.Deleted += FileUpdated;
            _fsw.Renamed += FileRenamed;

            Running = true;
            Status = "Running";
        }

        private void CommitTimerElapsed(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                PerformCommit();
                Thread.Sleep(1000);
                if(Running)
                {
                    Status = "Running";
                }else
                {
                    Status = "Not running";
                }
                
            }).Start();
            
        }

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
                foreach(StatusEntry c in status.Untracked)
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
                _repo.Commit(commitMessage, new Signature(Settings.GitUsername, Settings.GitEmail, DateTimeOffset.Now),
                    new Signature(Settings.GitUsername, Settings.GitEmail, DateTimeOffset.Now));
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
                    new UsernamePasswordCredentials { Username = Settings.GitUsername, Password = Settings.GitPassword };
                Console.WriteLine("actually pushing");
                _repo.Network.Push(_repo.Branches["master"], options);
            }
            catch(LibGit2SharpException le)
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

        private void FileRenamed(object sender, RenamedEventArgs e)
        {
            FileUpdated(sender, new FileSystemEventArgs(WatcherChangeTypes.Renamed, e.FullPath, e.Name));
        }

        private void FileUpdated(object sender, FileSystemEventArgs e)
        {
            if (!_changedFiles.Contains(e.FullPath))
                _changedFiles.Add(e.FullPath);

            
        }

        public void StopMonitoring()
        {
            if (!Running)
                return;

            if(_fsw != null)
            {
                _fsw.Dispose();
                _fsw = null;
            }

            if(_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }

            Running = false;
            Status = "Not running";
        }

        public void Dispose()
        {
            if(_repo != null)
            {
                _repo.Dispose();
                _repo = null;
            }
        }
    }
}
