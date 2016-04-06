using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;

namespace WCFdualContractExample
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class TimeInfoProvider : ITimeInfo
    {
        public void RequestTime(int Period, int Number)
        {
            ThreadPool.QueueUserWorkItem(o => RunDateTimeThreadForClient(Period, Number, OperationContext.Current.GetCallbackChannel<IClientGetTime>()));
        }

        private static readonly int OneSecond = 1000;
        public static void RunDateTimeThreadForClient(int CurrentPeriod, int AmountOfTimes, IClientGetTime callback)
        {
            for (int counter = 0; counter < CurrentPeriod; ++counter)
            {
                Thread.Sleep(CurrentPeriod * OneSecond);
                DateTime TimeOnWCFserver = DateTime.Now;
                callback.ReceiveTime(TimeOnWCFserver);
            }
        }
    }
}
