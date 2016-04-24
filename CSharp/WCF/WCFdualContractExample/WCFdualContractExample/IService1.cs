using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFdualContractExample
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IClientGetTime))]
    public interface ITimeInfo
    {

        [OperationContract(IsOneWay = true)]
        void RequestTime(int Period, int Number);
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    public interface IClientGetTime
    {
        [OperationContract]
        void ReceiveTime(DateTime CurrentDateTime);
    }
}
