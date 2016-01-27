using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pattern_adapter
{
    class our_interface
    {
        public static void print()
        {
            Console.WriteLine("print");
        }

        public static int increase_number(int new_int)
        {
            return new_int + 1;
        }

    }
}
