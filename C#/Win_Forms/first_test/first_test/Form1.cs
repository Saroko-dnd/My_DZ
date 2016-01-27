using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace first_test
{
    public partial class Form1 : Form
    {
        public static журнал current_journal;
        public static registration reg_form;
        public Form1()
        {
            InitializeComponent();
            this.Text = main_strings.hi_user_string;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool password_is_correct = false;
            bool file_exist = false;
            DirectoryInfo current_dir_info = new DirectoryInfo(Environment.CurrentDirectory);
            foreach (DirectoryInfo cur_dir_info in current_dir_info.GetDirectories())
            {
                if (cur_dir_info.Name == "teaching_staff")
                {
                    file_exist = true;
                }
            }
            if (!file_exist)
            {
                MessageBox.Show(main_strings.no_users_string);
            }
            else
            {
                XmlSerializer serializer_for_list_teachers = new XmlSerializer(typeof(List<teacher>));
                FileStream stream_to_teachers = new FileStream(@"teaching_staff\all_teachers.xml", FileMode.
                    Open, FileAccess.Read);
                List<teacher> buf_list = (List<teacher>)serializer_for_list_teachers.
                    Deserialize(stream_to_teachers);
                stream_to_teachers.Close();
                foreach (teacher current_teacher in buf_list)
                {
                    if (current_teacher.hash_of_password == this.textBox2.Text.GetHashCode() && current_teacher.
                        login == this.textBox1.Text)
                    {
                        password_is_correct = true;
                        MessageBox.Show(main_strings.password_accepted_string);
                        current_journal = new журнал(current_teacher);
                        break;
                    }
                }
                if (password_is_correct)
                {
                    this.textBox1.Text = "";
                    this.textBox2.Text = "";
                    журнал.login_form = this;
                    current_journal.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(main_strings.password_not_accepted_string);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reg_form = new registration();
            reg_form.ShowDialog();
        }
    }
}
