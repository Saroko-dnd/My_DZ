using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;

namespace first_test
{
    public partial class registration : Form
    {
        public ComponentResourceManager current_manager = new ComponentResourceManager(typeof(registration));

        public registration()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void reg_button_Click(object sender, EventArgs e)
        {
            if (this.textBox4.Text != this.textBox3.Text)
            {
                MessageBox.Show(current_manager.GetString("incorrect_password_string"));
            }
            else
            {
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
                    Directory.CreateDirectory("teaching_staff");
                    FileStream stream_to_teachers = new FileStream(@"teaching_staff\all_teachers.xml", FileMode.Create,
                        FileAccess.Write);
                    XmlSerializer serializer_for_list_teachers = new XmlSerializer(typeof(List<teacher>));
                    List<teacher> list_of_teachers = new List<teacher>();
                    teacher new_teacher = new teacher(this.textBox1.Text, this.textBox2.Text,
                        this.textBox4.Text);
                    list_of_teachers.Add(new_teacher);
                    serializer_for_list_teachers.Serialize(stream_to_teachers, list_of_teachers);
                    stream_to_teachers.Close();
                    MessageBox.Show(current_manager.GetString("registration_success_string"));
                    this.Close();
                }
                else
                {
                    bool teacher_already_exist = false;
                    FileStream stream_to_teachers = new FileStream(@"teaching_staff\all_teachers.xml", FileMode.OpenOrCreate,
                        FileAccess.ReadWrite);
                    XmlSerializer serializer_for_list_teachers = new XmlSerializer(typeof(List<teacher>));
                    List<teacher> list_of_teachers = (List<teacher>)serializer_for_list_teachers.
                        Deserialize(stream_to_teachers);
                    stream_to_teachers.Close();
                    File.Delete(@"teaching_staff\all_teachers.xml");
                    FileStream stream_to_teachers_2 = new FileStream(@"teaching_staff\all_teachers.xml", FileMode.OpenOrCreate,
                        FileAccess.ReadWrite);
                    foreach (teacher current_teacher in list_of_teachers)
                    {
                        if (current_teacher.name == this.textBox1.Text && current_teacher.login == this.textBox2.Text
                            && current_teacher.hash_of_password == this.textBox4.Text.GetHashCode())
                        {
                            teacher_already_exist = true;
                        }
                    }
                    if (!teacher_already_exist)
                    {
                        list_of_teachers.Add(new teacher(this.textBox1.Text, this.textBox2.Text,
                            this.textBox4.Text));
                        serializer_for_list_teachers.Serialize(stream_to_teachers_2, list_of_teachers);
                        MessageBox.Show(current_manager.GetString("registration_success_string"));
                        stream_to_teachers_2.Close();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(current_manager.GetString("user_exist_string"));
                        stream_to_teachers_2.Close();
                    }
                }
            }
        }

        private void registration_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
