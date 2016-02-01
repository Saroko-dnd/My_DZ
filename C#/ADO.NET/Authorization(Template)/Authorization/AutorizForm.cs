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
using System.Configuration;
//Name of connection string: ConnectionToAuthorizationDB
namespace Authorization
{
    public partial class AutorizForm : Form
    {
        

        public AutorizForm()
        {
            InitializeComponent();

            tbName.TextChanged += SpaceBarKiller.TextBoxTextChanged;
            tbName.KeyPress += SpaceBarKiller.TextBoxKeyPress;
            tbName.ForeColor = Color.Black; 
            tbPassword.TextChanged += SpaceBarKiller.TextBoxTextChanged;
            tbPassword.KeyPress += SpaceBarKiller.TextBoxKeyPress;
            tbPassword.ForeColor = Color.Black;
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
            DBConnector.CheckLoginPassword(tbName.Text, tbPassword.Text);
        }

    
    }
}

/*
The solution is laid in the following steps. You will not lose any data in your database and you should not delete your database file!

Pre-requisite: You must have installed SQL Server Management Studio (Full or Express)

Open SQL Server Management Studio
In the "Connect to Server" window (File->Connect object explorer) enter the following:
Server type    : Database Engine
Server name    : (localdb)\v11.0
Authentication : [Whatever you used when you created your local db. Probably Windows Authentication).
Click "Connect"
Expand the "Databases" folder in the Object Explorer (View->Object Explorer, F8)
Find your database. It should be named as the full path to your database (.mdf) file
You should see it says "(Pending Recovery)" at the end of the database name or when you try to expand the database it won't be able to and may or may not give you an error message.
This the issue! Your database has crashed essentially..
Right click on the database then select "Tasks -> Detach...".
In the detach window, select your database in the list and check the column that says "Drop Connections"
Click OK.
You should see the database disappear from the list of databases. Your problem should now be fixed. Go and run your application that uses your localdb.
After running your application, your database will re-appear in the list of databases - this is correct. It should not say "Pending recovery" any more since it should be working properly.
*/