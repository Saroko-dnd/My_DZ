using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsServiceTest
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        private void WriteCurrentMessage(string Message)
        {
            StreamWriter CurrentWriter = null; 
            try
            {
                CurrentWriter = new StreamWriter("MyLog.txt", true);
                CurrentWriter.WriteLine(DateTime.Now.ToString() + Message);
            }
            catch
            {
                CurrentWriter = new StreamWriter("Errors.txt", true);
                CurrentWriter.WriteLine(DateTime.Now.ToString() + "Не удалось создать запись в MyLog.txt");
            }
            finally
            {
                if (CurrentWriter != null)
                {
                    CurrentWriter.Close();
                }
            }
        }



        protected override void OnStart(string[] args)
        {
            WriteCurrentMessage("Служба запустилась");
        }

        protected override void OnStop()
        {
            WriteCurrentMessage("Служба прекратила работу");
        }
    }
}
