using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamCommit
{
    public partial class EnterCredentials : MetroForm
    {
        public EnterCredentials()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUsername.Text) || string.IsNullOrWhiteSpace(tbPassword.Text) || string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                MessageBox.Show("Please enter your git credentials.");
                return;
            }
            Settings.GitUsername = tbUsername.Text;
            Settings.GitPassword = tbPassword.Text;
            Settings.GitEmail = tbEmail.Text;
        }

        private void EnterCredentials_Load(object sender, EventArgs e)
        {
            tbUsername.Text = Settings.GitUsername;
            tbPassword.Text = Settings.GitPassword;
            tbEmail.Text = Settings.GitEmail;
        }
    }
}
