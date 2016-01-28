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
         
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool access_denied = false;
            bool PasswordsSame = false;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(localdb)\v11.0;Initial Catalog=Authorization_DB;Integrated Security=True;Pooling=False";
            connection.Open();

            string CommandString = @"SELECT Name FROM Users";

            SqlCommand NewCommand = new SqlCommand(CommandString, connection);

            SqlDataReader UsersReader = NewCommand.ExecuteReader();
            if (tbPas.Text != tbPas2.Text)
            {
                access_denied = true;
                MessageBox.Show("Passwords which you entered are not the same!");
            }
            else if (tbPas.Text.Length < 6)
            {
                access_denied = true;
                MessageBox.Show("Your password too short!");
            }
            else if (!access_denied)
            {
                while (UsersReader.Read() != false)
                {
                    if (tbLogin.Text == UsersReader.GetString(0).Replace(" ",""))
                    {
                        access_denied = true;
                        MessageBox.Show("Your login already exists!");
                    }
                }
                UsersReader.Close();
                if (!access_denied)
                {
                    NewCommand.CommandText = @"INSERT INTO UsersInfo (LastName, FirstName, Code) VALUES (@NewLastName, @NewFirstName, @ZeroValue)";

                    if (tbName.Text.Length == 0)
                        NewCommand.Parameters.AddWithValue("@NewFirstName", "-");
                    else
                        NewCommand.Parameters.AddWithValue("@NewFirstName", tbName.Text);
                    if (tbSurName.Text.Length == 0)
                        NewCommand.Parameters.AddWithValue("@NewLastName", "-");
                    else
                        NewCommand.Parameters.AddWithValue("@NewLastName", tbSurName.Text);
                    NewCommand.Parameters.AddWithValue("@ZeroValue",0);
                    NewCommand.ExecuteNonQuery();
                    NewCommand.CommandText = @"SELECT MAX(Id) FROM UsersInfo";
                    int InfoIdBuf = Int32.Parse(NewCommand.ExecuteScalar().ToString());
                    NewCommand.CommandText = @"INSERT INTO Users VALUES (@NewName, @NewPassword, @NewEmail, @NewInfoId)";
                    NewCommand.Parameters.Clear();
                    NewCommand.Parameters.AddWithValue("@NewName", tbLogin.Text);
                    NewCommand.Parameters.AddWithValue("@NewPassword", tbPas.Text);
                    NewCommand.Parameters.AddWithValue("@NewEmail", tbEmail.Text);
                    NewCommand.Parameters.AddWithValue("@NewInfoId", InfoIdBuf);
                    NewCommand.ExecuteNonQuery();
                    MessageBox.Show("New data added to database...");
                }
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
