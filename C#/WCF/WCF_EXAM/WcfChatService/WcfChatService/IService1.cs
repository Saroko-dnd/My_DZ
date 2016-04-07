using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChatService
{

    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    public interface IChatService
    {
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Join(string ClientName);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Message(string CurrentMessage);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void SendMessageTo(string CurrentMessage, string ClientReceiver);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void CloseConnection(string ClientName);
    }

    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void AnotherClientJoin(string NewClientName);
        [OperationContract(IsOneWay = true)]
        void AnotherClientLeft(string NewClientName);
        [OperationContract(IsOneWay = true)]
        void ReceivePublicMessage(string NewMessage);
        [OperationContract(IsOneWay = true)]
        void ReceivePrivateMessage(string NewMessage, string ClientSenderName);
    }


}
