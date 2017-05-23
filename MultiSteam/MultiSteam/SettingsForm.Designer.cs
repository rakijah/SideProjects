namespace MultiSteam
{
    partial class SettingsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.labelUsers = new System.Windows.Forms.Label();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.cbShowPassword = new System.Windows.Forms.CheckBox();
            this.tbNickname = new System.Windows.Forms.TextBox();
            this.labelNickname = new System.Windows.Forms.Label();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.labelSteamPath = new System.Windows.Forms.Label();
            this.tbSteamPath = new System.Windows.Forms.TextBox();
            this.btnBrowseSteamPath = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbUsers
            // 
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.Location = new System.Drawing.Point(7, 22);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(121, 199);
            this.lbUsers.TabIndex = 0;
            this.lbUsers.SelectedIndexChanged += new System.EventHandler(this.lbUsers_SelectedIndexChanged);
            // 
            // labelUsers
            // 
            this.labelUsers.AutoSize = true;
            this.labelUsers.Location = new System.Drawing.Point(4, 6);
            this.labelUsers.Name = "labelUsers";
            this.labelUsers.Size = new System.Drawing.Size(34, 13);
            this.labelUsers.TabIndex = 1;
            this.labelUsers.Text = "Users";
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(23, 227);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(28, 28);
            this.btnAddUser.TabIndex = 2;
            this.btnAddUser.Text = "+";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(177, 38);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(113, 20);
            this.tbUsername.TabIndex = 3;
            this.tbUsername.Validated += new System.EventHandler(this.tbUsername_Validated);
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(174, 22);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(55, 13);
            this.labelUsername.TabIndex = 4;
            this.labelUsername.Text = "Username";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(174, 61);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 6;
            this.labelPassword.Text = "Password";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(177, 77);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(113, 20);
            this.tbPassword.TabIndex = 5;
            this.tbPassword.Validated += new System.EventHandler(this.tbPassword_Validated);
            // 
            // cbShowPassword
            // 
            this.cbShowPassword.AutoSize = true;
            this.cbShowPassword.Checked = true;
            this.cbShowPassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowPassword.Location = new System.Drawing.Point(188, 103);
            this.cbShowPassword.Name = "cbShowPassword";
            this.cbShowPassword.Size = new System.Drawing.Size(102, 17);
            this.cbShowPassword.TabIndex = 7;
            this.cbShowPassword.Text = "Show Password";
            this.cbShowPassword.UseVisualStyleBackColor = true;
            this.cbShowPassword.CheckedChanged += new System.EventHandler(this.cbShowPassword_CheckedChanged);
            // 
            // tbNickname
            // 
            this.tbNickname.Location = new System.Drawing.Point(177, 149);
            this.tbNickname.Name = "tbNickname";
            this.tbNickname.Size = new System.Drawing.Size(113, 20);
            this.tbNickname.TabIndex = 8;
            this.tbNickname.Validated += new System.EventHandler(this.tbNickname_Validated);
            // 
            // labelNickname
            // 
            this.labelNickname.AutoSize = true;
            this.labelNickname.Location = new System.Drawing.Point(174, 133);
            this.labelNickname.Name = "labelNickname";
            this.labelNickname.Size = new System.Drawing.Size(55, 13);
            this.labelNickname.TabIndex = 9;
            this.labelNickname.Text = "Nickname";
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(80, 227);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(28, 28);
            this.buttonRemove.TabIndex = 10;
            this.buttonRemove.Text = "-";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "MultiSteam";
            this.notifyIcon.Visible = true;
            // 
            // labelSteamPath
            // 
            this.labelSteamPath.AutoSize = true;
            this.labelSteamPath.Location = new System.Drawing.Point(4, 258);
            this.labelSteamPath.Name = "labelSteamPath";
            this.labelSteamPath.Size = new System.Drawing.Size(94, 13);
            this.labelSteamPath.TabIndex = 12;
            this.labelSteamPath.Text = "Path to Steam.exe";
            // 
            // tbSteamPath
            // 
            this.tbSteamPath.Location = new System.Drawing.Point(7, 274);
            this.tbSteamPath.Name = "tbSteamPath";
            this.tbSteamPath.ReadOnly = true;
            this.tbSteamPath.Size = new System.Drawing.Size(253, 20);
            this.tbSteamPath.TabIndex = 13;
            this.tbSteamPath.TextChanged += new System.EventHandler(this.tbSteamPath_TextChanged);
            // 
            // btnBrowseSteamPath
            // 
            this.btnBrowseSteamPath.Location = new System.Drawing.Point(266, 274);
            this.btnBrowseSteamPath.Name = "btnBrowseSteamPath";
            this.btnBrowseSteamPath.Size = new System.Drawing.Size(24, 19);
            this.btnBrowseSteamPath.TabIndex = 14;
            this.btnBrowseSteamPath.Text = "...";
            this.btnBrowseSteamPath.UseVisualStyleBackColor = true;
            this.btnBrowseSteamPath.Click += new System.EventHandler(this.btnBrowseSteamPath_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(134, 22);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(28, 28);
            this.btnMoveUp.TabIndex = 15;
            this.btnMoveUp.Text = "▲";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(134, 61);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(28, 28);
            this.btnMoveDown.TabIndex = 16;
            this.btnMoveDown.Text = "▼";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 300);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnBrowseSteamPath);
            this.Controls.Add(this.tbSteamPath);
            this.Controls.Add(this.labelSteamPath);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.labelNickname);
            this.Controls.Add(this.tbNickname);
            this.Controls.Add(this.cbShowPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.labelUsers);
            this.Controls.Add(this.lbUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbUsers;
        private System.Windows.Forms.Label labelUsers;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.CheckBox cbShowPassword;
        private System.Windows.Forms.TextBox tbNickname;
        private System.Windows.Forms.Label labelNickname;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label labelSteamPath;
        private System.Windows.Forms.TextBox tbSteamPath;
        private System.Windows.Forms.Button btnBrowseSteamPath;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
    }
}

