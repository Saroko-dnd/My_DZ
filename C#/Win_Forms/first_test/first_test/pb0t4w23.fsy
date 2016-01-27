using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.ComponentModel;

namespace first_test
{
    public partial class журнал : Form
    {
        public static Form1 login_form;
        public teacher current_teacher;
        public static List<subject> main_list_of_subjects;
        public List<lesson> lessons_for_view = new List<lesson>();
        public ComponentResourceManager current_manager = new ComponentResourceManager(typeof(журнал));

        public журнал(teacher new_teacher)
        {
            InitializeComponent();
            current_teacher = new_teacher;
            this.label2.Text = new_teacher.name;
            XmlSerializer serializer_for_subjects = new XmlSerializer(typeof(List<subject>));
            FileStream stream_to_subjects = new FileStream(@"all_subjects\all_subjects.xml",
                     FileMode.Open, FileAccess.Read);
            main_list_of_subjects = (List<subject>)serializer_for_subjects.
                Deserialize(stream_to_subjects);
            stream_to_subjects.Close();
            DirectoryInfo current_dir_info = new DirectoryInfo(@"all_groups");
            foreach (DirectoryInfo cur_dir_info in current_dir_info.GetDirectories())
            {
                this.list_of_groups.Items.Add(cur_dir_info.Name);
            }
        }

        private void журнал_FormClosed(object sender, FormClosedEventArgs e)
        {
            login_form.Close();
        }

        private void change_password_button_Click(object sender, EventArgs e)
        {
            new_password window_for_new_password = new new_password(this);
            window_for_new_password.ShowDialog();
        }

        private void start_lesson_button_Click(object sender, EventArgs e)
        {
            if (this.themes_list.SelectedIndex >= 0 && this.time_list.SelectedIndex >= 0 &&
                this.list_of_groups.SelectedIndex >= 0)
            {
                FileStream stream_to_choosed_group = new FileStream(@"all_groups\" + this.list_of_groups.SelectedItem
                     + @"\" + this.list_of_groups.SelectedItem + ".xml", FileMode.Open, FileAccess.Read);
                XmlSerializer serializer_for_subjects = new XmlSerializer(typeof(group));
                group buf_for_group = (group)serializer_for_subjects.Deserialize(stream_to_choosed_group);
                stream_to_choosed_group.Close();
                string[] buf_for_date = ((string)this.time_list.SelectedItem).Split(new string[] { ":", " ", "-" },
                    StringSplitOptions.RemoveEmptyEntries);
                DateTime main_buf_for_date = new DateTime(this.dateTimePicker1.Value.Year, this.dateTimePicker1.
                    Value.Month,this.dateTimePicker1.Value.Day, Int32.Parse(buf_for_date[0]), Int32.
                    Parse(buf_for_date[1]),0);
                form_for_lesson current_lesson = new form_for_lesson(buf_for_group, main_list_of_subjects
                    [this.subjects_list.SelectedIndex], this.themes_list.SelectedIndex, main_buf_for_date,
                    current_teacher);
                current_lesson.ShowDialog();
            }
            else
            {
                MessageBox.Show(main_strings.fields_error);
            }
        }

        private void subjects_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.themes_list.Items.Clear();
            foreach (string current_theme in main_list_of_subjects[this.subjects_list.SelectedIndex].all_themes)
            {
                this.themes_list.Items.Add(current_theme);
            }
        }

        private void list_of_groups_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.subjects_list.Items.Clear();
            foreach (subject current_subject in main_list_of_subjects)
            {
                this.subjects_list.Items.Add(current_subject.name);
            }
        }

        private void show_lessons_button_Click(object sender, EventArgs e)
        {
            if (this.list_of_groups.SelectedIndex >= 0 && this.subjects_list.SelectedIndex >= 0 &&
                !this.lessons_of_current_teacher_checkBox.Checked)
            {
                lessons_for_view.Clear();
                XmlSerializer serializer_for_lesson = new XmlSerializer(typeof(lesson));
                bool directory_for_current_subject_exist = false;
                DirectoryInfo current_dir_info = new DirectoryInfo(@"all_groups\TEST_GROUP\all_lessons");
                foreach (DirectoryInfo cur_dir_info in current_dir_info.GetDirectories())
                {
                    if (cur_dir_info.Name == this.subjects_list.SelectedItem.ToString())
                    {
                        directory_for_current_subject_exist = true;
                    }
                }
                if (directory_for_current_subject_exist)
                {
                    DirectoryInfo info_about_lessons = new DirectoryInfo(@"all_groups\" + this.list_of_groups.SelectedItem
                        + @"\all_lessons\" + this.subjects_list.SelectedItem);
                    this.list_of_lessons.Items.Clear();
                    foreach (FileInfo cur_file_info in info_about_lessons.GetFiles())
                    {
                        FileStream stream_to_lesson = new FileStream(cur_file_info.FullName,FileMode.Open,
                            FileAccess.Read);
                        lessons_for_view.Add((lesson)serializer_for_lesson.Deserialize(stream_to_lesson));
                        stream_to_lesson.Close();
                        this.list_of_lessons.Items.Add(Path.GetFileNameWithoutExtension(cur_file_info.Name));
                    }
                }
            }
            else if (this.lessons_of_current_teacher_checkBox.Checked)
            {
                lessons_for_view.Clear();
                DirectoryInfo current_dir_info = new DirectoryInfo(@"all_groups");
                List<string> buf_for_group_names = new List<string>();
                List<lesson> buf_for_lessons = new List<lesson>();
                XmlSerializer serializer_for_lesson = new XmlSerializer(typeof(lesson));
                foreach (DirectoryInfo dir_info in current_dir_info.GetDirectories())
                {
                    buf_for_group_names.Add(dir_info.Name);
                }
                foreach (string current_name_of_group in buf_for_group_names)
                {
                    DirectoryInfo current_dir_info_2 = new DirectoryInfo(@"all_groups\" + current_name_of_group +
                        @"\all_lessons");
                    foreach (DirectoryInfo cur_dir_info_2 in current_dir_info_2.GetDirectories())
                    {
                        foreach (FileInfo current_lesson in cur_dir_info_2.GetFiles())
                        {
                            XmlDocument buf_for_lesson_xml = new XmlDocument();
                            buf_for_lesson_xml.Load(cur_dir_info_2.FullName + @"\" + current_lesson.Name);
                            XmlNode buf_for_teacher_name = null;
                            buf_for_teacher_name = buf_for_lesson_xml.SelectSingleNode(@"lesson/current_teacher");
                            if (buf_for_teacher_name.InnerText == current_teacher.name)
                            {
                                FileStream stream_to_choosed_lesson = new FileStream(cur_dir_info_2.FullName + @"\" +
                                    current_lesson.Name, FileMode.Open, FileAccess.Read);
                                if (buf_for_lessons.Count == 0)
                                {
                                    buf_for_lessons.Add((lesson)serializer_for_lesson.
                                        Deserialize(stream_to_choosed_lesson));
                                    buf_for_lessons[0].name_of_group = current_name_of_group;
                                }
                                else
                                {
                                    bool lesson_was_inserted = false;
                                    lesson buf_lesson = (lesson)serializer_for_lesson.
                                        Deserialize(stream_to_choosed_lesson);
                                    buf_lesson.name_of_group = current_name_of_group;
                                    for (int count_index = 0; count_index < buf_for_lessons.Count; ++count_index)
                                    {
                                        if (buf_lesson.date_of_lesson > buf_for_lessons[count_index].date_of_lesson)
                                        {
                                            lesson_was_inserted = true;
                                            buf_for_lessons.Insert(count_index, buf_lesson);
                                            break;
                                        }
                                    }
                                    if (!lesson_was_inserted)
                                    {
                                        buf_for_lessons.Add(buf_lesson);
                                    }
                                }
                                stream_to_choosed_lesson.Close();
                            }
                        }
                    }
                }
                this.list_of_lessons.Items.Clear();
                foreach (lesson cur_lesson in buf_for_lessons)
                {
                    this.list_of_lessons.Items.Add(cur_lesson.current_topic + " " + cur_lesson.date_of_lesson.
                        ToString());
                }
                lessons_for_view = buf_for_lessons;
            }
            else
            {
                MessageBox.Show(main_strings.show_lessons_error);
            }
        }

        private void list_of_lessons_SelectedIndexChanged(object sender, EventArgs e)
        {
                lesson buf_for_lesson = lessons_for_view[this.list_of_lessons.SelectedIndex];
                this.lesson_data_table.Controls.Clear();
                this.lesson_data_table.RowCount = buf_for_lesson.current_marks.Count + 1;
                Label buf_for_label = new Label();
                buf_for_label.Text = main_strings.students_string;
                this.lesson_data_table.Controls.Add(buf_for_label, 0, 0);
                Label buf_for_label_2 = new Label();
                buf_for_label_2.Text = main_strings.marks_string;
                this.lesson_data_table.Controls.Add(buf_for_label_2, 1, 0);
                this.name_of_group_for_table.Text = lessons_for_view[this.list_of_lessons.SelectedIndex].name_of_group;
                for (int counter = 1; counter <= buf_for_lesson.current_marks.Count; ++counter)
                {
                    Label buf_for_student = new Label();
                    buf_for_student.AutoSize = true;
                    buf_for_student.Text = buf_for_lesson.current_marks[counter - 1].name_of_owner;
                    Label buf_for_mark = new Label();
                    buf_for_mark.AutoSize = true;
                    buf_for_mark.Text = buf_for_lesson.current_marks[counter - 1].current_mark;
                    this.lesson_data_table.Controls.Add(buf_for_student, 0, counter);
                    this.lesson_data_table.Controls.Add(buf_for_mark, 1, counter);
                }
        }

        private void change_user_button_Click(object sender, EventArgs e)
        {
            login_form.Show();
            this.Hide();
        }
    }
}
