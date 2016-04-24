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

        public bool Join(string ClientName)
        {
            lock (AllClientsCallbacks)
            {

                if (AllClientsCallbacks.Where(res => res.CurrentClientName == ClientName).FirstOrDefault() != null)
                {
                    return false;
                }
                else
                {
                    List<int> PositionsForRemove = new List<int>();
                    int counter = 0;
                    foreach (ClientJoinInfo CurrentClientInfo in AllClientsCallbacks)
                    {
                        try
                        {
                            CurrentClientInfo.CurrentClientCallback.AnotherClientJoin(ClientName);
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
                        foreach (int CurrentRemovePosition in PositionsForRemove)
                        {
                            AllClientsCallbacks[CurrentRemovePosition].CurrentClientCallback = null;
                        }
                        AllClientsCallbacks = AllClientsCallbacks.Where(res => res.CurrentClientCallback != null).ToList();
                    }
                    AllClientsCallbacks.Add(new ClientJoinInfo(ClientName, OperationContext.Current.GetCallbackChannel<IClientCallback>()));
                    return true;
                }
            }
        }

        public void Message(string CurrentMessage, string ClientName)
        {
            lock (AllClientsCallbacks)
            {
                List<int> PositionsForRemove = new List<int>();
                int counter = 0;
                foreach (ClientJoinInfo CurrentClientInfo in AllClientsCallbacks)
                {
                    try
                    {
                        CurrentClientInfo.CurrentClientCallback.ReceivePublicMessage(CurrentMessage, ClientName);
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

        public void SendMessageTo(string CurrentMessage, string ClientReceiver, string ClientSender)
        {
            lock (AllClientsCallbacks)
            {
                ClientJoinInfo CurrentReceiver = AllClientsCallbacks.Where(res => res.CurrentClientName == ClientReceiver).FirstOrDefault();
                if (CurrentReceiver != null)
                {
                    CurrentReceiver.CurrentClientCallback.ReceivePrivateMessage(CurrentMessage, ClientSender);
                }
            }
        }

        public void CloseConnection(string ClientName)
        {
            lock (AllClientsCallbacks)
            {
                AllClientsCallbacks.Remove(AllClientsCallbacks.Where(res => res.CurrentClientName == ClientName).First());
                List<int> PositionsForRemove = new List<int>();
                int counter = 0;
                foreach (ClientJoinInfo CurrentClientInfo in AllClientsCallbacks)
                {
                    try
                    {
                        CurrentClientInfo.CurrentClientCallback.AnotherClientLeft(ClientName);
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
                    foreach (int CurrentRemovePosition in PositionsForRemove)
                    {
                        AllClientsCallbacks[CurrentRemovePosition].CurrentClientCallback = null;
                    }
                    AllClientsCallbacks = AllClientsCallbacks.Where(res => res.CurrentClientCallback != null).ToList();
                }
            }
        }

        public void GetListOfClientsInChat()
        {
            lock (AllClientsCallbacks)
            {
                OperationContext.Current.GetCallbackChannel<IClientCallback>().ReceiveListOfClientsInChat(AllClientsCallbacks.Select(res => res.CurrentClientName).ToArray());

            }
        }
    }

}
