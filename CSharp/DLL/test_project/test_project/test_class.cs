using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_project
{
    public class test_class
    {
        public int current_int;
        public void print(int number_for_print)
        {
            Console.WriteLine("method print works" + " " + number_for_print.ToString());
            Console.WriteLine("current int of this object" + " " + current_int.ToString());
        }

        public test_class(int new_int)
        {
            current_int = new_int;
        }

        public test_class()
        {
            current_int = 0;
        }

    }
}
