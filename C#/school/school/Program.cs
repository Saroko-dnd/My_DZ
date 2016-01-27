using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Schema;

namespace school
{
    class Program
    {
        static void Main(string[] args)
        {
            #region creating of necessary directories and files
            subject_storage test_list_of_subjects = new subject_storage();
            teacher_storage test_list_of_teachers = new teacher_storage();

            bool groups_directory_exist = false;
            bool subjects_directory_exist = false;
            bool teachers_directory_exist = false;
            DirectoryInfo current_dir_info = new DirectoryInfo(Environment.CurrentDirectory);
            foreach (DirectoryInfo cur_dir_info in current_dir_info.GetDirectories())
            {
                if (cur_dir_info.Name == "all_groups")
                {
                    groups_directory_exist = true;
                }
                if (cur_dir_info.Name == "all_subjects")
                {
                    subjects_directory_exist = true;
                }
                if (cur_dir_info.Name == "all_teachers")
                {
                    teachers_directory_exist = true;
                }
            }
            if (!groups_directory_exist)
            {
                Directory.CreateDirectory("all_groups");
            }
            if (!subjects_directory_exist)
            {
                Directory.CreateDirectory("all_subjects");
                XmlSerializer serializer_for_subjects = new XmlSerializer(typeof(subject_storage));
                FileStream stream_to_subjects = new FileStream(@"all_subjects\all_subjects.xml",
                     FileMode.Create, FileAccess.Write);
                serializer_for_subjects.Serialize(stream_to_subjects, test_list_of_subjects);
                stream_to_subjects.Close();
            }
            if (!teachers_directory_exist)
            {
                Directory.CreateDirectory("all_teachers");
                XmlSerializer serializer_for_teachers = new XmlSerializer(typeof(teacher_storage));
                FileStream stream_to_teachers = new FileStream(@"all_teachers\all_teachers.xml",
                     FileMode.Create, FileAccess.Write);
                serializer_for_teachers.Serialize(stream_to_teachers, test_list_of_teachers);
                stream_to_teachers.Close();
            }
            #endregion
            #region test of journal
            journal.loading_of_xsd_files();
            journal.load_groups();
            journal.load_subjects();
            journal.load_teachers();
            journal.add_subject("math");
            journal.add_subject("physics");
            journal.add_subject("biology");
            journal.save_subjects();
            journal.add_teacher("Nicholas");
            journal.add_teacher("David");
            journal.add_teacher("Kevin");
            journal.save_teachers();
            journal.add_group("first_group");
            journal.add_group("second_group");
            journal.add_group("third_group");
            journal.add_student("Jack", 0);
            journal.add_student("Eric", 0);
            journal.add_student("Richard", 0);
            journal.save_groups();
            journal test_journal = new journal(0, 0, 0, "Differential equations", new date());
            test_journal.add_mark(0,"5");
            test_journal.add_mark(1,"3");
            test_journal.add_mark(2,"6");
            test_journal.save_lesson();
            #endregion

        }
    }
}
