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
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool access_denied = false;
            bool PasswordsSame = false;
            bool PasswordTooShort = false;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(localdb)\v11.0;Initial Catalog=Authorization_DB;Integrated Security=True;Pooling=False";
            connection.Open();

            string CommandString = @"SELECT Email FROM Users";

            SqlCommand CommandExtractEmail = new SqlCommand(CommandString, connection);

            SqlDataReader EmailReader = CommandExtractEmail.ExecuteReader();
            if (tbNewPas.Text.Length < 6)
            {
                PasswordTooShort = true;
            }
            else if (tbNewPas.Text == tbNewPas2.Text)
            {
                PasswordsSame = true;
                while (EmailReader.Read() != false)
                {
                    string EmailTemp = EmailReader.GetString(0).Replace(" ", "");
                    if (tbEmail.Text == EmailTemp)
                    {
                        access_denied = true;
                        SqlCommand CommandUpdate = new SqlCommand(@"UPDATE Users SET [Password]=@pas WHERE Email=@email",
                            connection);
                        CommandUpdate.Parameters.AddWithValue("@pas", tbNewPas.Text);
                        EmailReader.Close();
                        CommandUpdate.Parameters.AddWithValue("@email", EmailTemp);
                        CommandUpdate.ExecuteNonQuery();
                        MessageBox.Show("Your password cnanged!");
                        this.Close();
                        break;
                    }
                }
            }
            else if (PasswordTooShort)
            {
                MessageBox.Show("Password too short!");
            }
            else if (!PasswordsSame)
            {
                MessageBox.Show("Check entered passwords!");
            }
            else if (!access_denied)
            {
                MessageBox.Show("Check entered email!");
            }
        }
    }
}
