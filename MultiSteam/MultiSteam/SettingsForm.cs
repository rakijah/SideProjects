using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiSteam2
{
    public partial class SettingsForm : Form
    {
        private UserContextMenu contextMenu;
        public bool ShouldClose { get; set; } = false;
        public static List<SteamUser> Users = new List<SteamUser>();

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate { Hide(); }));

            contextMenu = new UserContextMenu(this);
            RefreshList();
            tbSteamPath.Text = Config.GetString("SteamPath");
            notifyIcon.ContextMenuStrip = contextMenu;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            SteamUser.CreateNew("user", "", "nick");
            RefreshList();
            lbUsers.SelectedIndex = lbUsers.Items.Count - 1;
        }

        private void RefreshList()
        {
            ReadUsers();
            lbUsers.DataSource = null;
            lbUsers.DataSource = Users;
            lbUsers.DisplayMember = "Nickname";
            contextMenu.RefreshAccountList();
        }

        private void ReadUsers()
        {
            Users.Clear();
            var reader = Database.Query("SELECT * FROM {0} ORDER BY orderindex ASC", Database.USER_TABLE);
            while (reader.Read())
            {
                var user = new SteamUser(
                    (int)reader.GetInt64(0), //id
                    reader.GetString(1), //username
                    reader.GetString(2), //password
                    reader.GetString(3) //nickname
                    );
                Users.Add(user);
            }
            FixOrders();
        }

        /// <summary>
        /// Sets every users order to their index in the list.
        /// Fixes/prevents missing numbers in sequence.
        /// </summary>
        private void FixOrders()
        {
            for(int i = 0; i < Users.Count; i++)
            {
                Users[i].Order = i;
            }
        }

        private void lbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lbUsers.SelectedItem != null)
            {
                var user = lbUsers.SelectedItem as SteamUser;
                tbUsername.Text = user.Username;

                tbPassword.Text = user.Password;
                cbShowPassword.Checked = false;

                tbNickname.Text = user.Nickname;
                
            }else
            {
                tbUsername.Text = "";
                tbPassword.Text = "";
                tbNickname.Text = "";
            }
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(cbShowPassword.Checked)
            {
                tbPassword.PasswordChar = '\0';
            }
            else
            {
                tbPassword.PasswordChar = '●';
            }
        }

        #region text boxes
        private void tbUsername_Validated(object sender, EventArgs e)
        {
            if (tbUsername.Text.Equals("")) return;

            var user = lbUsers.SelectedItem as SteamUser;
            user.Username = tbUsername.Text;
        }

        private void tbPassword_Validated(object sender, EventArgs e)
        {
            if (tbPassword.Text.Equals("")) return;

            var user = lbUsers.SelectedItem as SteamUser;
            user.Password = tbPassword.Text;
        }

        private void tbNickname_Validated(object sender, EventArgs e)
        {
            if (tbNickname.Text.Equals("")) return;

            var user = lbUsers.SelectedItem as SteamUser;
            user.Nickname = tbNickname.Text;

            //refresh the user list and keep the selected index
            var index = lbUsers.SelectedIndex;
            RefreshList();
            lbUsers.SelectedIndex = index;
        }
        #endregion
        
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (lbUsers.SelectedItem == null) return;

            int curIndex = lbUsers.SelectedIndex;

            long id = (lbUsers.SelectedItem as SteamUser).ID;
            SteamUser.Remove(id);
            RefreshList();

            if(curIndex != 0)
            {
                lbUsers.SelectedIndex = curIndex - 1;
            }
        }

        private void btnBrowseSteamPath_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            DialogResult dr = ofd.ShowDialog();
            if(dr == DialogResult.OK)
            {
                tbSteamPath.Text = ofd.FileName;
            }
        }

        private void tbSteamPath_TextChanged(object sender, EventArgs e)
        {
            Config.Set("SteamPath", tbSteamPath.Text);
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ShouldClose)
            {
                //hide the tray icon to prevent it from staying visible after exiting
                notifyIcon.Visible = false;
            }
            else //minimize the settings window instead of actually exiting
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            var current = lbUsers.SelectedItem as SteamUser;
            if (current == null) return;

            //swap with the user above this user, i.e. index - 1
            var index = Users.IndexOf(current);
            if (index == 0) return;

            var next = Users[index - 1];

            //swap orders;
            long tmp = current.Order;
            current.Order = next.Order;
            next.Order = tmp;

            RefreshList();
            lbUsers.SelectedIndex = index - 1;
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            var current = lbUsers.SelectedItem as SteamUser;
            if (current == null) return;

            //swap with the user below this user, i.e. index + 1
            var index = Users.IndexOf(current);
            if (index == Users.Count - 1) return;

            var next = Users[index + 1];

            //swap orders;
            long tmp = current.Order;
            current.Order = next.Order;
            next.Order = tmp;

            RefreshList();
            lbUsers.SelectedIndex = index + 1;
        }
    }
}
