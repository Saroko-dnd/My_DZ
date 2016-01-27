using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace school
{
    [XmlRoot(Namespace = "http://www.school.com/teacher_storage", IsNullable = false)]
    public class teacher_storage
    {
        public List<string> names = new List<string>();

        public void add_teacher(string new_teacher)
        {
            names.Add(new_teacher);
        }

        public void remove_teacher(int index_of_teacher)
        {
            names.RemoveAt(index_of_teacher);
        }

        public teacher_storage()
        {

        }

        public teacher_storage(List<string> new_names)
        {
            names = new_names;
        }
    }
}
