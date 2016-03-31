using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    public class SubstructClass : MyServiceInterfface
    {
        public int substract(int FirstNumber, int SecondNumber)
        {
            return FirstNumber - SecondNumber;
        }
    }
}
