using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiSteam
{
    class UserMenuItem : ToolStripMenuItem
    {
        public SteamUser User;

        public UserMenuItem(SteamUser User)
            :base(User.Nickname)
        {
            Text = User.Nickname;
            this.User = User;
            Click += OnClick;
        }

        public void OnClick(object sender, EventArgs e)
        {
            SteamManager.LogIn(User);
        }
    }
}
