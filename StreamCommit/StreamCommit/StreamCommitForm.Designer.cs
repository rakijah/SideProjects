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
            this.tbFolderToWatch = new MetroFramework.Controls.MetroTextBox();
            this.btnFolderToWatch = new MetroFramework.Controls.MetroButton();
            this.lblFolderToWatch = new MetroFramework.Controls.MetroLabel();
            this.btnToggleRun = new MetroFramework.Controls.MetroButton();
            this.fbdFolderToWatch = new System.Windows.Forms.FolderBrowserDialog();
            this.lblStatus = new MetroFramework.Controls.MetroLabel();
            this.btnCredentials = new MetroFramework.Controls.MetroButton();
            this.tbCommitInterval = new MetroFramework.Controls.MetroTextBox();
            this.lblCommitInterval = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // tbFolderToWatch
            // 
            // 
            // 
            // 
            this.tbFolderToWatch.CustomButton.Image = null;
            this.tbFolderToWatch.CustomButton.Location = new System.Drawing.Point(185, 1);
            this.tbFolderToWatch.CustomButton.Name = "";
            this.tbFolderToWatch.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.tbFolderToWatch.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbFolderToWatch.CustomButton.TabIndex = 1;
            this.tbFolderToWatch.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbFolderToWatch.CustomButton.UseSelectable = true;
            this.tbFolderToWatch.CustomButton.Visible = false;
            this.tbFolderToWatch.Lines = new string[0];
            this.tbFolderToWatch.Location = new System.Drawing.Point(29, 87);
            this.tbFolderToWatch.MaxLength = 32767;
            this.tbFolderToWatch.Name = "tbFolderToWatch";
            this.tbFolderToWatch.PasswordChar = '\0';
            this.tbFolderToWatch.ReadOnly = true;
            this.tbFolderToWatch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbFolderToWatch.SelectedText = "";
            this.tbFolderToWatch.SelectionLength = 0;
            this.tbFolderToWatch.SelectionStart = 0;
            this.tbFolderToWatch.ShortcutsEnabled = true;
            this.tbFolderToWatch.Size = new System.Drawing.Size(209, 25);
            this.tbFolderToWatch.TabIndex = 0;
            this.tbFolderToWatch.UseSelectable = true;
            this.tbFolderToWatch.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbFolderToWatch.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnFolderToWatch
            // 
            this.btnFolderToWatch.Location = new System.Drawing.Point(244, 87);
            this.btnFolderToWatch.Name = "btnFolderToWatch";
            this.btnFolderToWatch.Size = new System.Drawing.Size(29, 25);
            this.btnFolderToWatch.TabIndex = 1;
            this.btnFolderToWatch.Text = "...";
            this.btnFolderToWatch.UseSelectable = true;
            this.btnFolderToWatch.Click += new System.EventHandler(this.PickFolderToWatch);
            // 
            // lblFolderToWatch
            // 
            this.lblFolderToWatch.AutoSize = true;
            this.lblFolderToWatch.Location = new System.Drawing.Point(29, 60);
            this.lblFolderToWatch.Name = "lblFolderToWatch";
            this.lblFolderToWatch.Size = new System.Drawing.Size(91, 19);
            this.lblFolderToWatch.TabIndex = 2;
            this.lblFolderToWatch.Text = "Git repository:";
            // 
            // btnToggleRun
            // 
            this.btnToggleRun.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnToggleRun.Enabled = false;
            this.btnToggleRun.Location = new System.Drawing.Point(29, 169);
            this.btnToggleRun.Name = "btnToggleRun";
            this.btnToggleRun.Size = new System.Drawing.Size(98, 35);
            this.btnToggleRun.TabIndex = 3;
            this.btnToggleRun.Text = "Start";
            this.btnToggleRun.UseSelectable = true;
            this.btnToggleRun.Click += new System.EventHandler(this.ToggleRun);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblStatus.Location = new System.Drawing.Point(141, 177);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(132, 21);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status: Not running";
            // 
            // btnCredentials
            // 
            this.btnCredentials.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCredentials.Location = new System.Drawing.Point(186, 27);
            this.btnCredentials.Name = "btnCredentials";
            this.btnCredentials.Size = new System.Drawing.Size(91, 23);
            this.btnCredentials.TabIndex = 5;
            this.btnCredentials.Text = "Credentials...";
            this.btnCredentials.UseSelectable = true;
            this.btnCredentials.Click += new System.EventHandler(this.btnCredentials_Click);
            // 
            // tbCommitInterval
            // 
            // 
            // 
            // 
            this.tbCommitInterval.CustomButton.Image = null;
            this.tbCommitInterval.CustomButton.Location = new System.Drawing.Point(15, 1);
            this.tbCommitInterval.CustomButton.Name = "";
            this.tbCommitInterval.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.tbCommitInterval.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbCommitInterval.CustomButton.TabIndex = 1;
            this.tbCommitInterval.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbCommitInterval.CustomButton.UseSelectable = true;
            this.tbCommitInterval.CustomButton.Visible = false;
            this.tbCommitInterval.Lines = new string[0];
            this.tbCommitInterval.Location = new System.Drawing.Point(199, 118);
            this.tbCommitInterval.MaxLength = 32767;
            this.tbCommitInterval.Name = "tbCommitInterval";
            this.tbCommitInterval.PasswordChar = '\0';
            this.tbCommitInterval.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbCommitInterval.SelectedText = "";
            this.tbCommitInterval.SelectionLength = 0;
            this.tbCommitInterval.SelectionStart = 0;
            this.tbCommitInterval.ShortcutsEnabled = true;
            this.tbCommitInterval.Size = new System.Drawing.Size(39, 25);
            this.tbCommitInterval.TabIndex = 6;
            this.tbCommitInterval.UseSelectable = true;
            this.tbCommitInterval.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbCommitInterval.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbCommitInterval.TextChanged += new System.EventHandler(this.tbCommitInterval_TextChanged);
            // 
            // lblCommitInterval
            // 
            this.lblCommitInterval.AutoSize = true;
            this.lblCommitInterval.Location = new System.Drawing.Point(29, 120);
            this.lblCommitInterval.Name = "lblCommitInterval";
            this.lblCommitInterval.Size = new System.Drawing.Size(164, 19);
            this.lblCommitInterval.TabIndex = 7;
            this.lblCommitInterval.Text = "Commit interval (seconds):";
            // 
            // StreamCommitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 227);
            this.Controls.Add(this.lblCommitInterval);
            this.Controls.Add(this.tbCommitInterval);
            this.Controls.Add(this.btnCredentials);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnToggleRun);
            this.Controls.Add(this.lblFolderToWatch);
            this.Controls.Add(this.btnFolderToWatch);
            this.Controls.Add(this.tbFolderToWatch);
            this.Name = "StreamCommitForm";
            this.Text = "StreamCommit";
            this.Load += new System.EventHandler(this.StreamCommitForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox tbFolderToWatch;
        private MetroFramework.Controls.MetroButton btnFolderToWatch;
        private MetroFramework.Controls.MetroLabel lblFolderToWatch;
        private MetroFramework.Controls.MetroButton btnToggleRun;
        private System.Windows.Forms.FolderBrowserDialog fbdFolderToWatch;
        private MetroFramework.Controls.MetroLabel lblStatus;
        private MetroFramework.Controls.MetroButton btnCredentials;
        private MetroFramework.Controls.MetroTextBox tbCommitInterval;
        private MetroFramework.Controls.MetroLabel lblCommitInterval;
    }
}

