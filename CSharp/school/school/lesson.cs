using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace school
{
    [XmlRoot(Namespace = "http://www.school.com/lesson", IsNullable = false)]
    public class lesson
    {
        public string current_topic;
        public string current_teacher;
        public List<mark> current_marks = new List<mark>();
        public date date_of_lesson;

        public void add_mark(mark new_mark)
        {
            current_marks.Add(new_mark);
        }

        public lesson ()
        {

        }

        public lesson (string new_topic, string new_teacher,date new_date)
        {
            current_topic = new_topic;
            current_teacher = new_teacher;
            date_of_lesson = new_date;
        }
    }
}
