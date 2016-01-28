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
            bool access_denied_1 = true;
            bool access_denied_2 = true;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(localdb)\v11.0;Initial Catalog=Authorization_DB;Integrated Security=True;Pooling=False";
            connection.Open();

            string CommandString = @"SELECT Name,Password,Info_id FROM Users";
            SqlCommand command_test = new SqlCommand(CommandString, connection);

            SqlDataReader reader = command_test.ExecuteReader();

            while (reader.Read()!=false)
            {
               string NameTemp = reader.GetString(0).Replace(" ","");
               string PasswordTemp = reader.GetString(1).Replace(" ","");
               if (tbName.Text == NameTemp)
               {
                   access_denied_1 = false;
               }
               if (tbPassword.Text == PasswordTemp)
               {
                   access_denied_2 = false;
               }
               if (!access_denied_1 && !access_denied_2)
               {
                   MessageBox.Show("Welcome to program user " + NameTemp + "!");
                   command_test.CommandText = @"SELECT FirstName,LastName FROM UsersInfo WHERE Id="
                        + reader[2].ToString();
                   reader.Close();
                   SqlDataReader reader_2 = command_test.ExecuteReader();
                   reader_2.Read();
                   string FirstNameBuf = reader_2.GetString(0).Replace(" ","");
                   string LastNameBuf = reader_2.GetString(1).Replace(" ","");
                   MainForm NewMainForm = new MainForm(FirstNameBuf, LastNameBuf);
                   NewMainForm.ShowDialog();
                   break;
               }
               else
               {
                   access_denied_1 = true;
                   access_denied_2 = true;
               }
            }
            if (access_denied_1 && access_denied_2)
            {
                MessageBox.Show("Access denied!");
            }
            connection.Close();
        }
    }
}
