using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ClientForWCFduplex.DateTimeInfoWCFduplex;

namespace ClientForWCFduplex
{
    class Program
    {
        static void Main(string[] args)
        {
            CallbackHandler.Proxy.ReturnTime(5,5);
            Console.ReadKey();
        }
    }

    public class CallbackHandler : IDateTimeInfoCallback
    {
        public static InstanceContext CurrentContext = new InstanceContext(new CallbackHandler());
        public static DateTimeInfoClient Proxy = new DateTimeInfoClient(CurrentContext);

        public void ReceiveTime(DateTime TimeFromWCF)
        {
            Console.WriteLine(TimeFromWCF.ToString());
        }
    }
}
