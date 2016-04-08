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
        public static List<string> AllClientsInChat = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            this.Closing += ClientShutDown;

            ClientsInChatListBox.ItemsSource = AllClientsInChat;
            CallBackHandler.ListOfClientsInChat = ClientsInChatListBox;
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

        }

        private void SendPrivateMessageButton_Click(object sender, RoutedEventArgs e)
        {

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
