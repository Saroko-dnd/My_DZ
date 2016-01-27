using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first_test
{
    public class mark
    {
        public string current_mark;
        public string name_of_owner;

        public mark()
        {

        }

        public mark (string new_mark,string new_name)
        {
            current_mark = new_mark;
            name_of_owner = new_name;
        }
    }
}
