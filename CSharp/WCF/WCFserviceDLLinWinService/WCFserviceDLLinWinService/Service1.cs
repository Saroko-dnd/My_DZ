using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCFdriversInfoServiceDLL;

namespace WCFserviceDLLinWinService
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
            DriversInfoServiceHost = new ServiceHost(typeof(DiskInfoProvider));
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
