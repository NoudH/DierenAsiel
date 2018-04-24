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

namespace DierenAsiel.UI
{
    public partial class Login : Form
    {
        ILogic logic = new LogicController();

        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (logic.Login(TxtUsername.Text, TxtPassword.Text))
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
