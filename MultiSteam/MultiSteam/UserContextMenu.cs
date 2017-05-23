using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiSteam
{
    class UserContextMenu : ContextMenuStrip
    {
        Form Owner;
        ToolStripMenuItem dropDownAccounts = new ToolStripMenuItem("Accounts");

        public UserContextMenu(Form Owner)
        {
            this.Owner = Owner;
            Setup();
        }

        public void Setup()
        {
            Items.Clear();
            Items.Add(dropDownAccounts);
            Items.Add("Settings...", null, (s, e) =>
            {
                Owner.Show();
            });
            Items.Add("Exit", null, (s, e) =>
            {
                if(Owner is SettingsForm)
                {
                    (Owner as SettingsForm).ShouldClose = true;
                    Owner.Close();
                }
            });
        }

        public void RefreshAccountList()
        {
            dropDownAccounts.DropDownItems.Clear();
            foreach (var user in SettingsForm.Users)
            {
                dropDownAccounts.DropDownItems.Add(
                        new UserMenuItem(user)
                    );
            }
        }
    }
}
