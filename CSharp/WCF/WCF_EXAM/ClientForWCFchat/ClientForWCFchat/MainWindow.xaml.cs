using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientForWCFchat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<ClientName> AllClientsInChat = new List<ClientName>();
        public static StringBuilder BuilderForPublicChat = new StringBuilder();
        public static StringBuilder BuilderForPrivateChat = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();

            this.Closing += ClientShutDown;

            ClientsInChatListBox.ItemsSource = AllClientsInChat;
            CallBackHandler.ListOfClientsInChat = ClientsInChatListBox;
            CallBackHandler.PublicChatTextBox = PublicChatTextBox;
            CallBackHandler.PrivateChatTextBox = PrivateChatTextBox;
        }

        private void ClientShutDown(object sender, EventArgs e)
        {
            if (ClientNameTextBox.IsReadOnly)
            {
                CallBackHandler.Proxy.CloseConnection(ClientNameTextBox.Text);
            }
        }

        private void SendpublicMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientNameTextBox.IsReadOnly)
            {
                CallBackHandler.Proxy.Message(PublicMessageTextBox.Text, ClientNameTextBox.Text);
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.NotInChatError, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SendPrivateMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReceiverNameTextBox.Text != string.Empty)
            {
                CallBackHandler.Proxy.SendMessageTo(PrivateMessageTextBox.Text, ReceiverNameTextBox.Text, ClientNameTextBox.Text);
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.ReceiverUnknown, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void JoinChatButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientNameTextBox.IsReadOnly)
            {
                MessageBox.Show(MyResourses.Texts.YouAlreadyInChatError, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (ClientNameTextBox.Text != string.Empty)
                {
                    if (!CallBackHandler.Proxy.Join(ClientNameTextBox.Text))
                    {
                        MessageBox.Show(MyResourses.Texts.InvalidClientNameError, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        ClientNameTextBox.IsReadOnly = true;
                        CallBackHandler.Proxy.GetListOfClientsInChat();
                    }
                }
                else
                {
                    MessageBox.Show(MyResourses.Texts.ClientNameEmptyError, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
