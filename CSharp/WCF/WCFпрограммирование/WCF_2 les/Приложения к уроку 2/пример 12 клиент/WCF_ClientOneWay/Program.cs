using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCF_ClientOneWay.ServiceReference1;

namespace WCF_ClientOneWay
{
    class client
    {
        static void Main(string[] args)
        {
            ReplyClient proxy = new ReplyClient();
            //proxy.FastReply();
            proxy.SlowReply();

            Console.WriteLine("Введите числовое значение ");
            int value = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Вы ввели {0}", value);
            proxy.Close();
        }
    }
}

