using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DierenAsiel.Logic;
using static DierenAsiel.Logic.Modes;

namespace DierenAsiel.UI
{
    public partial class Login : Form
    {
        IAuthenticationLogic authenticationLogic = new LoginAuthenticator(Mode.Normal);

        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (authenticationLogic.Login(TxtUsername.Text, TxtPassword.Text))
            {
                UserInterface userInterface = new UserInterface();
                userInterface.Show();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Inlog gegevens incorrect, weet je zeker dat je wachtwoord klopt?", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
