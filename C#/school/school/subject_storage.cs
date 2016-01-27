using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace school
{
    [XmlRoot(Namespace = "http://www.school.com/subject_storage", IsNullable = false)]
    public class subject_storage
    {
        public List<string> names = new List<string>();

        public void add_subject(string new_subject)
        {
            names.Add(new_subject);
        }

        public void remove_subject(int index_of_subject)
        {
            names.RemoveAt(index_of_subject);
        }

        public subject_storage()
        {

        }

        public subject_storage(List<string> new_names)
        {
            names = new_names;
        }
    }
}
