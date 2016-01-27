using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.ComponentModel;

namespace first_test
{
    public partial class form_for_lesson : Form
    {
        public group current_group;
        public subject current_subject;
        public int current_index_of_theme;
        public List<ComboBox> all_marks = new List<ComboBox>();
        public teacher current_teacher;
        public lesson current_lesson;
        public ComponentResourceManager current_manager = new ComponentResourceManager
            (typeof(form_for_lesson));

        public form_for_lesson(group new_group, subject new_subject, int index_of_theme, DateTime date,
            teacher new_teacher)
        {
            InitializeComponent();
            current_teacher = new_teacher;
            this.date_label.Text = lesson_form.date_string + date.Day.ToString() + "." + date.Month.ToString() +
                "." + date.Year.ToString();
            current_group = new_group;
            current_subject = new_subject;
            current_index_of_theme = index_of_theme;
            this.current_name_of_group.Text = current_group.name_of_group;
            this.journal_table.ColumnCount = 2;
            this.journal_table.RowCount = current_group.all_students.Count + 1;
            Label buf_for_label = new Label();
            buf_for_label.Text = main_strings.students_string;
            this.journal_table.Controls.Add(buf_for_label,0,0);
            Label buf_for_label_2 = new Label();
            buf_for_label_2.Text = main_strings.marks_string;
            this.journal_table.Controls.Add(buf_for_label_2,1,0);
            int counter_of_students = 1;
            foreach (student current_student in current_group.all_students)
            {
                Label student_name_label = new Label();
                student_name_label.AutoSize = true;
                student_name_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, 
                    System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                student_name_label.Text = current_student.name;
                this.journal_table.Controls.Add(student_name_label, 0, counter_of_students);
                ++counter_of_students;
            }
            string [] list_of_marks = new string[] {"1","2","3","4","5","6","7","8","9","10","N","nothing"};
            for (int counter = 0; counter < current_group.all_students.Count; ++counter)
            {
                ComboBox buf_for_list_of_marks = new ComboBox();
                buf_for_list_of_marks.Items.AddRange(list_of_marks);
                all_marks.Add(buf_for_list_of_marks);
                this.journal_table.Controls.Add(buf_for_list_of_marks,1,counter + 1);
            }
            current_lesson = new lesson(current_group.name_of_group, current_subject.all_themes[current_index_of_theme],
                current_teacher.name, date);
            this.subject_label.Text = current_manager.GetString("subject_string") + current_subject.name;
            this.theme_label.Text = current_manager.GetString("theme_string") + current_subject.all_themes[current_index_of_theme];
        }

        private void save_lesson_button_Click(object sender, EventArgs e)
        {
            bool directory_for_current_subject_exist = false;
            DirectoryInfo current_dir_info = new DirectoryInfo(@"all_groups\" + current_group.name_of_group + @"\all_lessons");
            foreach (DirectoryInfo cur_dir_info in current_dir_info.GetDirectories())
            {
                if (cur_dir_info.Name == current_subject.name)
                {
                    directory_for_current_subject_exist = true;
                }
            }
            if (!directory_for_current_subject_exist)
            {
                Directory.CreateDirectory(@"all_groups\" + current_group.name_of_group + @"\all_lessons\" + current_subject.name);
            }
            FileStream stream_to_lesson = new FileStream(@"all_groups\" + current_group.name_of_group +
                @"\all_lessons\" + current_subject.name + @"\" + current_subject.all_themes[current_index_of_theme] + 
                ".xml", FileMode.Create, FileAccess.Write);
            XmlSerializer serializer_for_lesson = new XmlSerializer(typeof(lesson));
            int index_of_marked_student = 0;
            foreach (ComboBox cur_combo_box in all_marks)
            {
                if (cur_combo_box.SelectedItem != null)
                {
                    current_lesson.add_mark(new mark(cur_combo_box.SelectedItem.ToString(), current_group.
                        all_students[index_of_marked_student].name));
                }
                else
                {
                    current_lesson.add_mark(new mark(@"nothing", current_group.
                        all_students[index_of_marked_student].name));
                }
                ++index_of_marked_student;
            }
            serializer_for_lesson.Serialize(stream_to_lesson, current_lesson);
            stream_to_lesson.Close();
            MessageBox.Show(lesson_form.save_string);
            this.Close();
        }
    }
}
