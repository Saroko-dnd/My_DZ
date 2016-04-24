using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension_Methods
{
    class Program
    {

        static void Main(string[] args)
        {
            List<int> current_int_list = new List<int>();
            current_int_list.Add(2);
            current_int_list.Add(9);
            current_int_list.Add(7);
            string test_string = current_int_list.get_as_json();

            test_class test_object = new test_class();
            string test_string_2 = test_object.get_current_class_as_json();
            Console.ReadKey();
        }
    }
}
