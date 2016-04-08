using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChatService
{

    [ServiceContract(CallbackContract = typeof(IClientCallback), SessionMode = SessionMode.Required)]
    public interface IChatService
    {
        [OperationContract(IsOneWay = false, IsInitiating = true, IsTerminating = false)]
        bool Join(string ClientName);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Message(string CurrentMessage, string ClientName);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void SendMessageTo(string CurrentMessage, string ClientReceiver, string ClientSender);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void CloseConnection(string ClientName);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void GetListOfClientsInChat();

    }

    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void AnotherClientJoin(string NewClientName);
        [OperationContract(IsOneWay = true)]
        void AnotherClientLeft(string NewClientName);
        [OperationContract(IsOneWay = true)]
        void ReceivePublicMessage(string NewMessage, string ClientName);
        [OperationContract(IsOneWay = true)]
        void ReceivePrivateMessage(string NewMessage, string ClientSenderName);
        [OperationContract(IsOneWay = true)]
        void ReceiveListOfClientsInChat(string[] AllClients);

    }


}
