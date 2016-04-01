using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCF_ClientProxy.ServiceReference1;


namespace WCF_ClientProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            MyMathClient proxy = new MyMathClient();

            int result = proxy.Add(35, 57);
            Console.WriteLine("Сумма = {0}", result);
            Console.WriteLine("Для завершения нажмите<ENTER>.\n\n");
            Console.ReadLine();
        }
    }
}
