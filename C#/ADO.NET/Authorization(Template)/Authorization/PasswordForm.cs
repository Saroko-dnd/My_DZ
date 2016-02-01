using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SqlClient;

namespace Authorization
{
    public partial class PasswordForm : Form
    {
      
        public PasswordForm()
        {
            InitializeComponent();

            tbEmail.KeyPress += SpaceBarKiller.TextBoxKeyPress;
            tbEmail.TextChanged += SpaceBarKiller.TextBoxTextChanged;
            tbNewPas.KeyPress += SpaceBarKiller.TextBoxKeyPress;
            tbNewPas.TextChanged += SpaceBarKiller.TextBoxTextChanged;
            tbNewPas2.KeyPress += SpaceBarKiller.TextBoxKeyPress;
            tbNewPas2.TextChanged += SpaceBarKiller.TextBoxTextChanged;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tbNewPas.Text.Length < 6)
            {
                MessageBox.Show("Password too short!");
            }
            else if (tbNewPas.Text != tbNewPas2.Text)
            {
                MessageBox.Show("Check entered passwords!");
            }
            else
            {
                if (!DBConnector.CreateNewPassword(tbNewPas.Text,tbEmail.Text))
                {
                    MessageBox.Show("Check entered email!");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
