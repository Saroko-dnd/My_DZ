using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WindowsService2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string [] args)
        {
            ServiceHost sh = new ServiceHost(typeof(MyMath));

            sh.AddServiceEndpoint(typeof(InterfaceMyMath), new WSHttpBinding(), "http://localhost:8082/MyMath/Ep1");
            sh.Open();
            Console.WriteLine(" For end press <ENTER>\n");
            Console.ReadLine();
            sh.Close();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service1() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }

}
