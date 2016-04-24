using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SqlClient;

namespace Authorization
{
    public partial class RegistrForm : Form
    {

     
        public RegistrForm()
        {
            InitializeComponent();

            tbEmail.KeyPress += SpaceBarKiller.TextBoxKeyPress;
            tbEmail.TextChanged += SpaceBarKiller.TextBoxTextChanged;
            tbLogin.KeyPress += SpaceBarKiller.TextBoxKeyPress;
            tbLogin.TextChanged += SpaceBarKiller.TextBoxTextChanged;
            tbName.KeyPress += SpaceBarKiller.TextBoxKeyPress;
            tbName.TextChanged += SpaceBarKiller.TextBoxTextChanged;
            tbPas.KeyPress += SpaceBarKiller.TextBoxKeyPress;
            tbPas.TextChanged += SpaceBarKiller.TextBoxTextChanged;
            tbPas2.KeyPress += SpaceBarKiller.TextBoxKeyPress;
            tbPas2.TextChanged += SpaceBarKiller.TextBoxTextChanged;
            tbSurName.KeyPress += SpaceBarKiller.TextBoxKeyPress;
            tbSurName.TextChanged += SpaceBarKiller.TextBoxTextChanged;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tbEmail.Text.Length == 0)
            {
                MessageBox.Show(@"Вы не указали свой Email!",
                    @"Отказано в создании учетной записи.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (tbLogin.Text.Length == 0)
            {
                MessageBox.Show(@"Вы не указали свой Логин!",
                    @"Отказано в создании учетной записи.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (tbPas.Text != tbPas2.Text)
            {
                MessageBox.Show(@"Неверно введен во второй раз пароль!",
                    @"Отказано в создании учетной записи.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (tbPas.Text.Length < 6 || tbPas2.Text.Length < 6)
            {
                MessageBox.Show(@"Пароль должен содержать не менее 6 символов!",
                    @"Отказано в создании учетной записи.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DBConnector.CreateNewUser(tbLogin.Text,tbPas.Text,tbEmail.Text,tbName.Text,tbSurName.Text);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
