﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCFserverForBrakingSSH
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost OurHost = new ServiceHost(typeof(OurServer));
            OurHost.Open();

            Console.ReadKey();

            OurHost.Close();
        }
    }
}
