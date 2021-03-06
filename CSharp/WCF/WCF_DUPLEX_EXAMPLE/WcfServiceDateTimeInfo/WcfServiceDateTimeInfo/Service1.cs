﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WcfServiceDateTimeInfo
{
    public class DateTimeInfoProvider : IDateTimeInfo
    {
        private static readonly int OneSecond = 1000;

        public void ReturnTime(int Period, int Number)
        {
            IClientCallback TestCallback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            //null reference в  ThreadPool.QueueUserWorkItem
            IClientCallback CurrentClientCallback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            ThreadPool.QueueUserWorkItem(o => MessagingToClient(Period, Number, CurrentClientCallback));
        }

        public static void MessagingToClient(int Period, int Number, IClientCallback CurrentCallback)
        {
            for (int counter = 0; counter < Period; ++counter)
            {
                Thread.Sleep(Period * OneSecond);
                DateTime TimeOnWCFserver = DateTime.Now;
                CurrentCallback.ReceiveTime(TimeOnWCFserver);
            }
        }
    }
}
