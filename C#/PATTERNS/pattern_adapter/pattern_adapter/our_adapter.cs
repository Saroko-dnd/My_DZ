using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pattern_adapter
{
    class our_adapter
    {
        public static void print_something()
        {
            our_interface.print();
        }
        public static int increase_this_int(int current_int)
        {
            return our_interface.increase_number(current_int);
        }
    }
}
