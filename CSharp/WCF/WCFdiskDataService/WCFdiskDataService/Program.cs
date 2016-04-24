using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

using System.Windows;
using System.IO;
using System.Threading;

namespace WCFdiskDataService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost DiskInfoProviderHost = new ServiceHost(typeof(DiskInfoProvider));
            DiskInfoProviderHost.Open();

            Console.ReadKey();

            DiskInfoProviderHost.Close();
        }
    }
}
