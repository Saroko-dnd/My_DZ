using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceDateTimeInfo
{
    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    public interface IDateTimeInfo
    {
        [OperationContract(IsOneWay = true)]
        void ReturnTime(int Period, int Number);
    }

    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveTime(DateTime CurrentTime);
    }
}
