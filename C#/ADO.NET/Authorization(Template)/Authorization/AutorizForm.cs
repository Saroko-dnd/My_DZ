using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Authorization
{
    public partial class AutorizForm : Form
    {
        

        public AutorizForm()
        {
            InitializeComponent();
        }


        private void AutorizForm_Load(object sender, EventArgs e)
        {


        }

        void tbPassword_GotFocus(object sender, EventArgs e)
        {
            if (tbPassword.Text == "Пароль")
            {
                tbPassword.ForeColor = Color.Black;
                tbPassword.Text = "";
                tbPassword.PasswordChar = '*';
            }
        }

        void tbPassword_LostFocus(object sender, EventArgs e)
        {
            if (tbPassword.Text.Length == 0)
            {
                tbPassword.ForeColor = Color.LightGray;
                tbPassword.Text = "Пароль";
                tbPassword.PasswordChar ='\0';
            }
        }

        void tbName_LostFocus(object sender, EventArgs e)
        {
            if (tbName.Text.Length==0)
           {
            tbName.ForeColor = Color.LightGray;
            tbName.Text = "Имя пользователя";
           }
        }

        void tbName_GotFocus(object sender, EventArgs e)
        {
            if (tbName.Text == "Имя пользователя")
            {
                tbName.ForeColor = Color.Black;
                tbName.Text = "";
            }
        }      

        private void linkPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PasswordForm frmPass = new PasswordForm();
            frmPass.ShowDialog();
        }

        private void linkReg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistrForm frmReg = new RegistrForm();
            frmReg.ShowDialog();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(localdb)\v11.0;Initial Catalog=Authorization_DB;Integrated Security=True;Pooling=False";
            connection.Open();

            string CommandString = @"INSERT INTO [Users] ([Name],[Password],[Email]) VALUES ('Fox','1111','Fox@mail.by')";
            SqlCommand command_test = new SqlCommand(CommandString, connection);
            command_test.CommandText = @"SELECT TOP 1 Name FROM Users";
            MessageBox.Show(command_test.ExecuteScalar().ToString());
            connection.Close();
        }
    }

}
