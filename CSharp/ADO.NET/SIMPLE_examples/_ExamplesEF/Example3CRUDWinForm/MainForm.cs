using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Добавление пространства имен 
// для методов Load() и ToBindingList()
using System.Data.Entity;
using System.Data.Entity.Core;

namespace Example3CRUDWinForm
{
    public partial class MainForm : Form
    {
        PhoneStoreEntities db;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            db = new PhoneStoreEntities();
            // Загрузка данных из БД
            db.Users.Load();
            
            dataGridView1.DataSource = db.Users.Local.ToBindingList();
            
            // настройка выделения всей строки
            dataGridView1.MultiSelect =false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // Добавление в базу нового пользователя

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbLogin.Text == String.Empty || tbEmail.Text == String.Empty)
            {
                MessageBox.Show("Текстовые поля не заполнены !");
                return;
            }
            Users user = new Users
            {
                Login = tbLogin.Text,
                Password=new Random().Next(10000,99999).ToString(),
                Email = tbEmail.Text
            };

            db.Users.Add(user);
            db.SaveChanges();
            dataGridView1.Refresh();

            tbLogin.Text = String.Empty;
            tbEmail.Text = String.Empty;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                long Id = (long)dataGridView1.SelectedRows[0].Cells[0].Value;
                // Определить ключ для искомой сущности.
                Users selUser = db.Users.Find(Id);
                

                 if (selUser!=null)
                 {
                     db.Users.Remove(selUser);
                     db.SaveChanges();
                     dataGridView1.Refresh();
                 }
                
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbLogin.Text == String.Empty || tbEmail.Text == String.Empty)
            {
                MessageBox.Show("Текстовые поля не заполнены !");
                return;
            }

            if (dataGridView1.SelectedRows.Count != 0)
            {
                long Id = (long)dataGridView1.SelectedRows[0].Cells[0].Value;
                // Определить ключ для искомой сущности.
                Users selUser = db.Users.Find(Id);
                
                if (selUser != null)
                {
                    selUser.Login = tbLogin.Text;
                    selUser.Email = tbEmail.Text;
                }

                db.SaveChanges();
                dataGridView1.Refresh();
            }
        }
    }
}
