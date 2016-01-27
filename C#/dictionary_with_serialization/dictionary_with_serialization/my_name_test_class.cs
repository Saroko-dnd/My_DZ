using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dictionary_with_serialization
{
    public class my_name_test_class
    {
        public string first_name;
        public string second_name;

        public my_name_test_class()
        {

        }
        public my_name_test_class(string new_first_name,string new_second_name)
        {
            first_name = new_first_name;
            second_name = new_second_name;
        }
    }
}
