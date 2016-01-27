using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension_Methods
{
    class test_class
    {
        public int firrst_int = 88;
        public int second_int = 77;
        public string test_string = "helloWorld";
        public List<int> test_list_of_int = new List<int>{ 4, 5, 6, 7 };
        Dictionary<string, bool> current_dictionary = new Dictionary<string, bool>();
 
        public test_class ()
        {
            current_dictionary.Add("first_key",true);
            current_dictionary.Add("second_key", true);
            current_dictionary.Add("third_key", true);
        }

    }
}
