using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace first_test
{
    public class lesson
    {
        [XmlIgnore]
        public string name_of_group;
        public string current_topic;
        public string current_teacher;
        public List<mark> current_marks = new List<mark>();
        public DateTime date_of_lesson;

        public void add_mark(mark new_mark)
        {
            current_marks.Add(new_mark);
        }

        public lesson ()
        {

        }

        public lesson(string new_name_of_group, string new_topic, string new_teacher, DateTime new_date)
        {
            name_of_group = new_name_of_group;
            current_topic = new_topic;
            current_teacher = new_teacher;
            date_of_lesson = new_date;
        }
    }
}
