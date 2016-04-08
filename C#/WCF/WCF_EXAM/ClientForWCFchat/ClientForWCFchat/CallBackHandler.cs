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
        public static TextBox PublicChatTextBox;
        public static TextBox PrivateChatTextBox;

        public static InstanceContext CurrentContext = new InstanceContext(new CallBackHandler());
        public static ChatServiceClient Proxy = new ChatServiceClient(CurrentContext);

        public void AnotherClientJoin(string NewClientName)
        {
            lock (MainWindow.AllClientsInChat)
            {
                MainWindow.AllClientsInChat.Add(new ClientName(NewClientName));
                ListOfClientsInChat.Items.Refresh();
            }
        }

        public void AnotherClientLeft(string NewClientName)
        {
            lock (MainWindow.AllClientsInChat)
            {
                MainWindow.AllClientsInChat.Remove(MainWindow.AllClientsInChat.Where(res => res.CurrentClientName == NewClientName).First());
                ListOfClientsInChat.Items.Refresh();
            }
        }

        public void ReceivePrivateMessage(string NewMessage, string ClientSenderName)
        {
            lock (MainWindow.BuilderForPrivateChat)
            {
                MainWindow.BuilderForPrivateChat.AppendLine(ClientSenderName.ToUpper() + ": " + NewMessage);
                PrivateChatTextBox.Text = MainWindow.BuilderForPrivateChat.ToString();
            }
        }

        public void ReceivePublicMessage(string NewMessage, string ClientName)
        {
            lock (MainWindow.BuilderForPublicChat)
            {
                MainWindow.BuilderForPublicChat.AppendLine(ClientName.ToUpper() + ": " + NewMessage);
                PublicChatTextBox.Text = MainWindow.BuilderForPublicChat.ToString();
            }
        }

        public void ReceiveListOfClientsInChat(string[] AllClients)
        {
            lock (MainWindow.AllClientsInChat)
            {
                MainWindow.AllClientsInChat = AllClients.Select(res => new ClientName(res)).ToList();
                ListOfClientsInChat.ItemsSource = MainWindow.AllClientsInChat;
            }
        }
    }
}
