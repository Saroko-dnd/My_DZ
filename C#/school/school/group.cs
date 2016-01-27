using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace school
{
    [XmlRoot(Namespace = "http://www.school.com/group", IsNullable = false)]
    public class group
    {
        public List<student> all_students = new List<student>();
        public string name_of_group;

        public void add_student(student new_student)
        {
            all_students.Add(new_student);
        }
        public void remove_student(int index_of_student)
        {
            all_students.RemoveAt(index_of_student);
        }
        public group ()
        {

        }
        public group (string new_name)
        {
            name_of_group = new_name;
        }
        public group(string new_name,List<student> new_students)
        {
            name_of_group = new_name;
            all_students = new_students;
        }
    }
}
