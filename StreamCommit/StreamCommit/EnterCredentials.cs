using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace StreamCommit
{
    public partial class EnterCredentials : MetroForm
    {
        private Settings _settings;

        public EnterCredentials()
        {
            InitializeComponent();
            _settings = Settings.Instance;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUsername.Text) || string.IsNullOrWhiteSpace(tbPassword.Text) || string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                MessageBox.Show("Please enter your git credentials.");
                return;
            }
            _settings.GitUsername = tbUsername.Text;
            _settings.GitPassword = tbPassword.Text;
            _settings.GitEmail = tbEmail.Text;
        }

        private void EnterCredentials_Load(object sender, EventArgs e)
        {
            tbUsername.Text = _settings.GitUsername;
            tbPassword.Text = _settings.GitPassword;
            tbEmail.Text = _settings.GitEmail;
        }
    }
}