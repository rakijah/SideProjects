namespace StreamCommit
{
    partial class StreamCommitForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFolderToWatch = new System.Windows.Forms.Button();
            this.lblGitRepository = new System.Windows.Forms.Label();
            this.btnToggleRun = new System.Windows.Forms.Button();
            this.fbdFolderToWatch = new System.Windows.Forms.FolderBrowserDialog();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCredentials = new System.Windows.Forms.Button();
            this.tbCommitInterval = new System.Windows.Forms.TextBox();
            this.lblCommitInterval = new System.Windows.Forms.Label();
            this.lblLoggedIn = new System.Windows.Forms.Label();
            this.lblFolderToWatch = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFolderToWatch
            // 
            this.btnFolderToWatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolderToWatch.Location = new System.Drawing.Point(272, 40);
            this.btnFolderToWatch.Name = "btnFolderToWatch";
            this.btnFolderToWatch.Size = new System.Drawing.Size(61, 25);
            this.btnFolderToWatch.TabIndex = 1;
            this.btnFolderToWatch.Text = "Choose...";
            this.btnFolderToWatch.Click += new System.EventHandler(this.PickFolderToWatch);
            // 
            // lblGitRepository
            // 
            this.lblGitRepository.AutoSize = true;
            this.lblGitRepository.Location = new System.Drawing.Point(12, 46);
            this.lblGitRepository.Name = "lblGitRepository";
            this.lblGitRepository.Size = new System.Drawing.Size(80, 13);
            this.lblGitRepository.TabIndex = 2;
            this.lblGitRepository.Text = "Git repository:";
            this.lblGitRepository.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnToggleRun
            // 
            this.btnToggleRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnToggleRun.Enabled = false;
            this.btnToggleRun.Location = new System.Drawing.Point(9, 108);
            this.btnToggleRun.Name = "btnToggleRun";
            this.btnToggleRun.Size = new System.Drawing.Size(74, 21);
            this.btnToggleRun.TabIndex = 3;
            this.btnToggleRun.Text = "Start";
            this.btnToggleRun.Click += new System.EventHandler(this.ToggleRun);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.Location = new System.Drawing.Point(89, 108);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(183, 21);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status: Not running";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCredentials
            // 
            this.btnCredentials.Location = new System.Drawing.Point(12, 12);
            this.btnCredentials.Name = "btnCredentials";
            this.btnCredentials.Size = new System.Drawing.Size(91, 23);
            this.btnCredentials.TabIndex = 5;
            this.btnCredentials.Text = "Credentials...";
            this.btnCredentials.Click += new System.EventHandler(this.btnCredentials_Click);
            // 
            // tbCommitInterval
            // 
            this.tbCommitInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbCommitInterval.Location = new System.Drawing.Point(182, 77);
            this.tbCommitInterval.Name = "tbCommitInterval";
            this.tbCommitInterval.Size = new System.Drawing.Size(39, 22);
            this.tbCommitInterval.TabIndex = 6;
            this.tbCommitInterval.TextChanged += new System.EventHandler(this.tbCommitInterval_TextChanged);
            // 
            // lblCommitInterval
            // 
            this.lblCommitInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCommitInterval.AutoSize = true;
            this.lblCommitInterval.Location = new System.Drawing.Point(12, 79);
            this.lblCommitInterval.Name = "lblCommitInterval";
            this.lblCommitInterval.Size = new System.Drawing.Size(141, 13);
            this.lblCommitInterval.TabIndex = 7;
            this.lblCommitInterval.Text = "Commit interval (seconds):";
            // 
            // lblLoggedIn
            // 
            this.lblLoggedIn.AutoSize = true;
            this.lblLoggedIn.Location = new System.Drawing.Point(109, 17);
            this.lblLoggedIn.Name = "lblLoggedIn";
            this.lblLoggedIn.Size = new System.Drawing.Size(40, 13);
            this.lblLoggedIn.TabIndex = 8;
            this.lblLoggedIn.Text = "User: -";
            // 
            // lblFolderToWatch
            // 
            this.lblFolderToWatch.AutoSize = true;
            this.lblFolderToWatch.Location = new System.Drawing.Point(89, 46);
            this.lblFolderToWatch.Name = "lblFolderToWatch";
            this.lblFolderToWatch.Size = new System.Drawing.Size(11, 13);
            this.lblFolderToWatch.TabIndex = 9;
            this.lblFolderToWatch.Text = "-";
            this.lblFolderToWatch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StreamCommitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 138);
            this.Controls.Add(this.lblFolderToWatch);
            this.Controls.Add(this.lblLoggedIn);
            this.Controls.Add(this.lblCommitInterval);
            this.Controls.Add(this.tbCommitInterval);
            this.Controls.Add(this.btnCredentials);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnToggleRun);
            this.Controls.Add(this.lblGitRepository);
            this.Controls.Add(this.btnFolderToWatch);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "StreamCommitForm";
            this.Text = "StreamCommit";
            this.Load += new System.EventHandler(this.StreamCommitForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnFolderToWatch;
        private System.Windows.Forms.Label lblGitRepository;
        private System.Windows.Forms.Button btnToggleRun;
        private System.Windows.Forms.FolderBrowserDialog fbdFolderToWatch;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnCredentials;
        private System.Windows.Forms.TextBox tbCommitInterval;
        private System.Windows.Forms.Label lblCommitInterval;
        private System.Windows.Forms.Label lblLoggedIn;
        private System.Windows.Forms.Label lblFolderToWatch;
    }
}

