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
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ClientSocketTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IPEndPoint ClientEndPoint;
        public int PortNumber = 5000;
        public Socket ClientListenSocket;
        public Socket ClientSendSocket;

        public MainWindow()
        {
            InitializeComponent();
            
            ClientListenPortTextBox.PreviewTextInput += CharsKiller.InputValidation;
            ClientListenPortTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            ServerPortTextBox.PreviewTextInput += CharsKiller.InputValidation;
            ServerPortTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;

            /*bool LocalIpWasFound = false;

            try
            {
                ClientEndPoint = new IPEndPoint(IPAddress.Parse(GetLocalIPAddress()), PortNumber);
                LocalIpWasFound = true;
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (LocalIpWasFound)
            {
                ClientListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientListenSocket.Bind(ClientEndPoint);
                ClientListenSocket.Listen(1);
                ThreadPool.SetMinThreads(2, 2);
            }*/
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception(MyResourses.Texts.LocalIpNotFound);
        }
    }
}
