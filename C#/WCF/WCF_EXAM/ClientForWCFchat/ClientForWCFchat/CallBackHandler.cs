using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientForWCFchat.WCFchat;
using System.Windows.Controls;
using System.ServiceModel;

namespace ClientForWCFchat
{
    public class CallBackHandler : IChatServiceCallback
    {

        public static ListBox ListOfClientsInChat;

        public static InstanceContext CurrentContext = new InstanceContext(new CallBackHandler());
        public static ChatServiceClient Proxy = new ChatServiceClient(CurrentContext);

        public void AnotherClientJoin(string NewClientName)
        {
            lock (MainWindow.AllClientsInChat)
            {
                MainWindow.AllClientsInChat.Add(NewClientName);
                ListOfClientsInChat.Items.Refresh();
            }
        }

        public void AnotherClientLeft(string NewClientName)
        {
            lock (MainWindow.AllClientsInChat)
            {
                MainWindow.AllClientsInChat.Remove(MainWindow.AllClientsInChat.Where(res => res == NewClientName).First());
                ListOfClientsInChat.Items.Refresh();
            }
        }

        public void ReceivePrivateMessage(string NewMessage, string ClientSenderName)
        {
            throw new NotImplementedException();
        }

        public void ReceivePublicMessage(string NewMessage)
        {
            throw new NotImplementedException();
        }

        public void ReceiveListOfClientsInChat(string[] AllClients)
        {
            lock (MainWindow.AllClientsInChat)
            {                
                MainWindow.AllClientsInChat = AllClients.ToList();
                ListOfClientsInChat.Items.Refresh();
            }
        }
    }
}
