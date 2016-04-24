using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strings_of_file_class
{
    class my_exception : Exception
    {
        public string current_exception;
        public my_exception (string new_exception)
        {
            current_exception = new_exception;
        }
    }
}
