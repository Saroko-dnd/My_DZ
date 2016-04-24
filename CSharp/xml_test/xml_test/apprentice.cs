using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xml_test
{
    [Serializable]
    public class apprentice
    {
        public List<string> things_in_schoolbag;

        public string name = null;
        public int age = 0;
        public int GPA = 0;

        public apprentice()
        { }
        public apprentice (string new_name,int new_age, int new_GPA, List<string> new_things)
        {
            name = new_name;
            age = new_age;
            GPA = new_GPA;
            things_in_schoolbag = new_things;
        }
    }
}
