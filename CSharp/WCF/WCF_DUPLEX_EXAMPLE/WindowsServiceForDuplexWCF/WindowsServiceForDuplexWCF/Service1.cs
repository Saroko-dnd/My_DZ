using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WcfServiceDateTimeInfo; 

namespace WindowsServiceForDuplexWCF
{
    public partial class Service1 : ServiceBase
    {

        internal static ServiceHost DriversInfoServiceHost = null;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (DriversInfoServiceHost != null)
            {
                DriversInfoServiceHost.Close();
            }
            DriversInfoServiceHost = new ServiceHost(typeof(DateTimeInfoProvider));
            DriversInfoServiceHost.Open();
        }

        protected override void OnStop()
        {
            if (DriversInfoServiceHost != null)
            {
                DriversInfoServiceHost.Close();
                DriversInfoServiceHost = null;
            }
        }
    }
}
