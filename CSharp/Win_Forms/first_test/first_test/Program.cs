using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace first_test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool groups_directory_exist = false;
            bool subjects_directory_exist = false;
            DirectoryInfo current_dir_info = new DirectoryInfo(Environment.CurrentDirectory);
            foreach (DirectoryInfo cur_dir_info in current_dir_info.GetDirectories())
            {
                if (cur_dir_info.Name == "all_subjects")
                {
                    subjects_directory_exist = true;
                }
                if (cur_dir_info.Name == "all_groups")
                {
                    groups_directory_exist = true;
                }
            }
            if (!subjects_directory_exist)
            {
                List<subject> test_list_of_subjects = new List<subject>();
                test_list_of_subjects.Add(new subject("mathematics", new List<string> { "math_theme_1", "math_theme_2", "math_theme_3" }));
                test_list_of_subjects.Add(new subject("physics", new List<string> { "phys_theme_1", "phys_theme_2", "phys_theme_3" }));
                test_list_of_subjects.Add(new subject("biology", new List<string> { "bio_theme_1", "bio_theme_2", "bio_theme_3" }));
                Directory.CreateDirectory("all_subjects");
                XmlSerializer serializer_for_subjects = new XmlSerializer(typeof(List<subject>));
                FileStream stream_to_subjects = new FileStream(@"all_subjects\all_subjects.xml",
                     FileMode.Create, FileAccess.Write);
                serializer_for_subjects.Serialize(stream_to_subjects, test_list_of_subjects);
                stream_to_subjects.Close();
            }
            if (!groups_directory_exist)
            {
                group test_group = new group("TEST_GROUP_1", new List<student> {new student("first_student_1"),
                   new student("second_student_1"),new student("third_student_1")});
                group test_group_2 = new group("TEST_GROUP_2", new List<student> {new student("first_student_2"),
                   new student("second_student_2"),new student("third_student_2")});
                Directory.CreateDirectory(@"all_groups\" + test_group.name_of_group + @"\all_lessons");
                Directory.CreateDirectory(@"all_groups\" + test_group_2.name_of_group + @"\all_lessons");
                FileStream stream_to_groups = new FileStream(@"all_groups\" + test_group.name_of_group +
                    @"\" + test_group.name_of_group + ".xml", FileMode.Create, FileAccess.Write);
                XmlSerializer serializer_for_groups = new XmlSerializer(typeof(group));
                serializer_for_groups.Serialize(stream_to_groups, test_group);
                stream_to_groups.Close();
                FileStream stream_to_groups_2 = new FileStream(@"all_groups\" + test_group_2.name_of_group +
                    @"\" + test_group_2.name_of_group + ".xml", FileMode.Create, FileAccess.Write);
                serializer_for_groups.Serialize(stream_to_groups_2, test_group_2);
                stream_to_groups_2.Close();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
