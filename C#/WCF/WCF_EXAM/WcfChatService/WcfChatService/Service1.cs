using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WcfChatService
{
    //NOTES:
    //ICommunicationObject использовать для закрытия службы (приводим IClientCallback к ICommunicationObject и закрываем)
    public class ChatServiceProvider : IChatService
    {
        public static List<ClientJoinInfo> AllClientsCallbacks = new List<ClientJoinInfo>();
        public void Join(string ClientName)
        {
            lock (AllClientsCallbacks)
            {
                AllClientsCallbacks.Add(new ClientJoinInfo(ClientName, OperationContext.Current.GetCallbackChannel<IClientCallback>()));
            }
        }
        public void Message(string CurrentMessage)
        {
            List<int> PositionsForRemove = new List<int>();
            lock (AllClientsCallbacks)
            {
                int counter = 0;
                foreach (ClientJoinInfo CurrentClientInfo in AllClientsCallbacks)
                {
                    try
                    {
                        CurrentClientInfo.CurrentClientCallback.ReceivePublicMessage(CurrentMessage);
                    }
                    catch
                    {
                        //Удаляем из списка недоступного клиента
                        PositionsForRemove.Add(counter);
                    }
                    finally
                    {
                        ++counter;
                    }
                }
                foreach (int CurrentRemovePosition in PositionsForRemove)
                {
                    AllClientsCallbacks[CurrentRemovePosition].CurrentClientCallback = null;
                }
                AllClientsCallbacks = AllClientsCallbacks.Where(res => res.CurrentClientCallback != null).ToList();
            }
        }
        public void SendMessageTo(string CurrentMessage, string ClientReceiver)
        {
            lock (AllClientsCallbacks)
            {
                AllClientsCallbacks.Where(res => res.CurrentClientName == ClientReceiver).First().CurrentClientCallback.ReceivePrivateMessage(CurrentMessage, ClientReceiver);
            }
        }

        public void CloseConnection(string ClientName)
        {
            lock (AllClientsCallbacks)
            {
                AllClientsCallbacks.Remove(AllClientsCallbacks.Where(res => res.CurrentClientName == ClientName).First());
            }
        }

    }

}
