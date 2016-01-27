using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace school
{
    class journal
    {
        static void settings_validation_event_handler(object sender, ValidationEventArgs e)
        {
            Console.WriteLine("Validation Error: {0}", e.Message);
            Console.ReadKey();
        }

        public static XmlReaderSettings settings = new XmlReaderSettings();
        public static void loading_of_xsd_files()
        {
            settings.Schemas.Add("http://www.school.com/group", @"validate\xml_scheme_for_group.xsd");
            settings.Schemas.Add("http://www.school.com/subject_storage", @"validate\xml_scheme_for_subjects.xsd");
            settings.Schemas.Add("http://www.school.com/teacher_storage", @"validate\xml_scheme_for_teachers.xsd");
            settings.Schemas.Add("http://www.school.com/lesson", @"validate\xml_scheme_for_lesson.xsd");
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += new ValidationEventHandler(settings_validation_event_handler);
        }

        public static List<group> all_groups = new List<group>();
        public static subject_storage all_subjects;
        public static teacher_storage all_teachers;

        public static void add_student(string new_name_of_student, int index_of_group)
        {
            all_groups[index_of_group].add_student(new student(new_name_of_student));
        }

        public static void remove_student(int index_of_student, int index_of_group)
        {
            all_groups[index_of_group].remove_student(index_of_student);
        }

        public static void load_groups()
        {
            DirectoryInfo current_dir_info = new DirectoryInfo(@"all_groups");
            XmlSerializer serializer_for_group = new XmlSerializer(typeof(group));
            foreach (DirectoryInfo cur_dir_info in current_dir_info.GetDirectories())
            {
                XmlReader reader_for_group = XmlReader.Create(cur_dir_info.FullName + @"\all_students.xml",
                    settings);
                all_groups.Add((group)serializer_for_group.Deserialize(reader_for_group));
                reader_for_group.Close();
            }
        }

        public static void add_group(string new_name_of_group)
        {
            XmlSerializer serializer_for_group = new XmlSerializer(typeof(group));
            Directory.CreateDirectory(@"all_groups\" + new_name_of_group + @"\all_lessons");
            FileStream stream_to_group_file = new FileStream(@"all_groups\" + new_name_of_group
                + @"\all_students.xml", FileMode.CreateNew, FileAccess.Write);
            serializer_for_group.Serialize(stream_to_group_file, new group(new_name_of_group));
            stream_to_group_file.Close();
            all_groups.Add(new group(new_name_of_group));
        }

        public static void remove_group(int index_of_group)
        {
            Directory.Delete(@"all_groups\" + all_groups[index_of_group].name_of_group);
            all_groups.RemoveAt(index_of_group);
        }

        public static void save_groups()
        {
            XmlSerializer serializer_for_groups = new XmlSerializer(typeof(group));
            foreach (group current_group in all_groups)
            {
                File.Delete(@"all_groups\" + current_group.name_of_group + @"\all_students.xml");
                FileStream stream_to_group_file = new FileStream(@"all_groups\" + current_group.name_of_group
                    + @"\all_students.xml", FileMode.CreateNew, FileAccess.Write);
                serializer_for_groups.Serialize(stream_to_group_file, current_group);
                stream_to_group_file.Close();
            }
        }

        public static void load_subjects()
        {
            XmlReader reader_for_subjects = XmlReader.Create(@"all_subjects\all_subjects.xml", settings);
            XmlSerializer serializer_for_subject = new XmlSerializer(typeof(subject_storage));
            all_subjects = (subject_storage)serializer_for_subject.Deserialize(reader_for_subjects);
            reader_for_subjects.Close();
        }

        public static void add_subject(string new_name_of_subject)
        {
            all_subjects.add_subject(new_name_of_subject);
        }

        public static void remove_subject(int index_of_subject)
        {
            all_subjects.remove_subject(index_of_subject);
        }

        public static void save_subjects()
        {
            XmlSerializer serializer_for_subjects = new XmlSerializer(typeof(subject_storage));
            File.Delete(@"all_subjects\all_subjects.xml");
            FileStream stream_to_subjects_file = new FileStream(@"all_subjects\all_subjects.xml", 
                FileMode.CreateNew, FileAccess.Write);
            serializer_for_subjects.Serialize(stream_to_subjects_file, all_subjects);
            stream_to_subjects_file.Close();
        }

        public static void load_teachers()
        {
            XmlReader reader_for_teachers = XmlReader.Create(@"all_teachers\all_teachers.xml", settings);
            XmlSerializer serializer_for_teacher = new XmlSerializer(typeof(teacher_storage));
            all_teachers = (teacher_storage)serializer_for_teacher.Deserialize(reader_for_teachers);
            reader_for_teachers.Close();
        }

        public static void add_teacher(string new_name_of_teacher)
        {
            all_teachers.add_teacher(new_name_of_teacher);
        }

        public static void remove_teacher(int index_of_teacher)
        {
            all_teachers.remove_teacher(index_of_teacher);
        }

        public static void save_teachers()
        {
            XmlSerializer serializer_for_teachers = new XmlSerializer(typeof(teacher_storage));
            File.Delete(@"all_teachers\all_teachers.xml");
            FileStream stream_to_teachers_file = new FileStream(@"all_teachers\all_teachers.xml",
                FileMode.CreateNew, FileAccess.Write);
            serializer_for_teachers.Serialize(stream_to_teachers_file, all_teachers);
            stream_to_teachers_file.Close();
        }

        public List<lesson> previous_lessons = new List<lesson>();
        public int index_of_current_group;
        public int index_of_current_subject;
        public lesson current_lesson = new lesson();

        public void add_mark(int index_of_student, string new_mark)
        {
            current_lesson.add_mark(new mark(new_mark,all_groups[index_of_current_group].
                all_students[index_of_student].name));
        }

        public void add_student(string new_name_of_student)
        {
            all_groups[index_of_current_group].add_student(new student(new_name_of_student));
        }

        public void remove_student(int index_of_student)
        {
            all_groups[index_of_current_group].remove_student(index_of_student);
        }

        public void save_lesson()
        {
            XmlSerializer serializer_for_lesson = new XmlSerializer(typeof(lesson));
            FileStream current_dir_info = new FileStream(@"all_groups\" + 
                all_groups[index_of_current_group].name_of_group
                + @"\all_lessons\" + all_subjects.names[index_of_current_subject] + @"\lesson_"
                + (previous_lessons.Count()+1).ToString()+".xml", FileMode.OpenOrCreate, FileAccess.Write);
            serializer_for_lesson.Serialize(current_dir_info, current_lesson);
            current_dir_info.Close();
        }

        public journal()
        {
            
        }

        public journal(int new_index_of_current_group,int index_of_new_subject, int index_of_new_teacher,
            string new_topic, date new_date)
        {
            index_of_current_subject = index_of_new_subject;
            index_of_current_group = new_index_of_current_group;
            current_lesson.current_teacher = all_teachers.names[index_of_new_teacher];
            current_lesson.current_topic = new_topic;
            current_lesson.date_of_lesson = new_date;

            Directory.CreateDirectory(@"all_groups\" + all_groups[index_of_current_group].name_of_group
                + @"\all_lessons\" + all_subjects.names[index_of_new_subject]);
            DirectoryInfo current_dir_info = new DirectoryInfo(@"all_groups\" + 
                all_groups[index_of_current_group].name_of_group
                + @"\all_lessons\" + all_subjects.names[index_of_new_subject]);
            XmlSerializer our_serializer = new XmlSerializer(typeof(lesson));
            foreach (FileInfo current_file_info in current_dir_info.GetFiles())
            {
                XmlReader reader_for_lessons = XmlReader.Create(current_file_info.FullName, settings);
                previous_lessons.Add((lesson)our_serializer.Deserialize(reader_for_lessons));
                reader_for_lessons.Close();
            }
        }
    }
}
