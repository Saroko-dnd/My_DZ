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
    public partial class new_password : Form
    {
        public журнал buf_for_journal;
        public ComponentResourceManager current_manager = new ComponentResourceManager(typeof(new_password));

        public new_password(журнал new_journal)
        {
            InitializeComponent();
            buf_for_journal = new_journal;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == this.textBox2.Text)
            {
                XmlSerializer serializer_for_list_teachers = new XmlSerializer(typeof(List<teacher>));
                FileStream stream_to_teachers = new FileStream(@"teaching_staff\all_teachers.xml", FileMode.
                    Open, FileAccess.Read);
                List<teacher> buf_list = (List<teacher>)serializer_for_list_teachers.
                    Deserialize(stream_to_teachers);
                stream_to_teachers.Close();
                File.Delete(@"teaching_staff\all_teachers.xml");
                for (int counter = 0; counter < buf_list.Count; ++counter)
                {
                    this.label1.Text = counter.ToString();
                    if (buf_list[counter].name == buf_for_journal.current_teacher.name &&
                        buf_list[counter].login == buf_for_journal.current_teacher.login &&
                        buf_list[counter].hash_of_password == buf_for_journal.current_teacher.hash_of_password)
                    {
                        FileStream stream_to_teachers_2 = new FileStream(@"teaching_staff\all_teachers.xml", FileMode.
                            OpenOrCreate, FileAccess.ReadWrite);
                        buf_list.RemoveAt(counter);
                        buf_for_journal.current_teacher.hash_of_password = this.textBox1.Text.GetHashCode();
                        buf_list.Add(buf_for_journal.current_teacher);
                        serializer_for_list_teachers.Serialize(stream_to_teachers_2, buf_list);
                        stream_to_teachers_2.Close();
                        MessageBox.Show(current_manager.GetString("password_success_string"));
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show(current_manager.GetString("password_rejected_string"));
            }
        }
    }
}
