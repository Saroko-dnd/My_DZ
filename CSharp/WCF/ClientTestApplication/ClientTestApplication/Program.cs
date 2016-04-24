using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ClientTestApplication
{
    class Program
    {
        public static ChannelFactory<MyServiceInterfface> TestChannelFactory =  new ChannelFactory<MyServiceInterfface>(new NetHttpBinding(), new EndpointAddress("http://localhost:8080/TestService/EndPoint_1"));
        static void Main(string[] args)
        {
            MyServiceInterfface TestChannel = TestChannelFactory.CreateChannel();
            Console.WriteLine(TestChannel.substract(100,25));
            Console.ReadKey();
        }
    }
}
