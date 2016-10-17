using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WCFServiceLibraryForProductsData;

namespace WCFHostForProductData
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(WCFFakeRepositoryForProducts));
            host.Open();
            Console.ReadKey();
            host.Close();
        }
    }
}
