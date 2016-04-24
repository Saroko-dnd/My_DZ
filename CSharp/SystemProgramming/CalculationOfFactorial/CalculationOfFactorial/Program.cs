using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationOfFactorial
{
    class Program
    {
        static void Main(string[] args)
        {
            int Result = 0;
            try
            {
                Result = fact(Int32.Parse(args[0]));
            }
            catch
            {

            }
            Console.WriteLine(Result);
            Console.ReadLine();

        }

        public static int fact(int num)
        {
            return (num == 0) ? 1 : num * fact(num - 1);
        }
    }
}
