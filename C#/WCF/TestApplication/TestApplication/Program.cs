using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Type serviceType = typeof(SubstructClass);
            ServiceHost host = new ServiceHost(serviceType);
            host.AddServiceEndpoint(typeof(MyServiceInterfface), new NetHttpBinding(), "http://localhost:8080/TestService/EndPoint_1");
            host.Open();

            Console.ReadKey();
            host.Close();
        }
    }
}
