using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCF_ClientDataContract.ServiceReference1;

namespace WCF_ClientDataContract
{
    class Program
    {
        static void Main(string[] args)
        {
            MyMathClient proxy = new MyMathClient();

            MathResult mr = proxy.Total(35, 38);
            Console.WriteLine("Результат: {0} {1} {2} {3}", mr.sum, mr.subtr, mr.div, mr.mult);

            Console.WriteLine("Для завершения нажмите<ENTER>.\n\n");
            Console.ReadLine();
        }
    }
}
