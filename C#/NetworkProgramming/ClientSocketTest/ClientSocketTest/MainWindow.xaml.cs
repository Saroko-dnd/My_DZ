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
        public IPEndPoint ServerEndPoint;
        public int PortNumber = -1;
        public Socket ClientListenSocket = null;
        public Socket ClientSendSocket = null;
        public bool ClientShutDown = false;
        public StringBuilder MainBuilderForTextBox = new StringBuilder();
        public bool ConnectionEstablished = false;
        public bool ServerReady = false;
        public char CharSeparator = '#';

        public MainWindow()
        {
            InitializeComponent();
            
            ClientListenPortTextBox.PreviewTextInput += CharsKiller.InputValidation;
            ClientListenPortTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            ServerPortTextBox.PreviewTextInput += CharsKiller.InputValidation;
            ServerPortTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            IPofServerTextBox.PreviewTextInput += CharsKiller.InputValidationForIP;
            IPofServerTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
        }

        public void CloseSockets(object Sender,EventArgs e)
        {
            ClientShutDown = true;
            if (ClientListenSocket != null && ClientSendSocket != null)
            {
                ClientSendSocket.Send(Encoding.UTF8.GetBytes(MyResourses.Texts.ClientOff));
                ClientListenSocket.Close();
                ClientSendSocket.Close();
            }
        }

        public void InitAndIdentityMessaging()
        {
            try
            {
                ClientSendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientSendSocket.Connect(ServerEndPoint);
                //посылаем инитиируещее связь сообщение пока не получим подтверждение от сервера
                Application.Current.Dispatcher.Invoke(new Action(() => ClientSendSocket.Send(Encoding.UTF8.GetBytes(GetLocalIPAddress() + 
                    CharSeparator + PortNumber.ToString() + CharSeparator + ClientNameTextBox.Text))));
                while (!ServerReady)
                {
                    Thread.Sleep(100);
                }
                lock (MainBuilderForTextBox)
                {
                    MainBuilderForTextBox.AppendLine(MyResourses.Texts.ConnectionSuccess);
                    Application.Current.Dispatcher.Invoke(new Action(() => MessagesFromServerTextBox.Text = MainBuilderForTextBox.ToString()));
                }
            }
            catch (Exception CurrentException)
            {
                if (!ClientShutDown)
                {
                    MessageBox.Show(CurrentException.Message + "Client_init", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ListenServer()
        {
            try
            {
                byte[] BufForMessage = new byte[500];
                Socket ConnectionToServer = ClientListenSocket.Accept();
                ConnectionToServer.Receive(BufForMessage);
                if (Encoding.UTF8.GetString(BufForMessage).TrimEnd('\0') == MyResourses.Texts.ServerReady)
                {
                    ServerReady = true;
                }
            }
            catch (Exception CurrentException)
            {
                if (!ClientShutDown)
                {
                    MessageBox.Show(CurrentException.Message + "ClientListen", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionEstablished)
            {
                MessageBox.Show(MyResourses.Texts.ConnectionAlreadyExists, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                bool LocalIPwasFound = false;
                try
                {
                    PortNumber = Int32.Parse(ClientListenPortTextBox.Text);
                    ClientEndPoint = new IPEndPoint(IPAddress.Parse(GetLocalIPAddress()), PortNumber);
                    LocalIPwasFound = true;
                }
                catch (Exception CurrentException)
                {
                    MessageBox.Show(CurrentException.Message + "Client_1", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (LocalIPwasFound)
                {
                    bool DataCorrect = false;
                    try
                    {
                        ServerEndPoint = new IPEndPoint(IPAddress.Parse(IPofServerTextBox.Text), Int32.Parse(ServerPortTextBox.Text));
                        DataCorrect = true;
                    }
                    catch
                    {
                        MessageBox.Show(MyResourses.Texts.CheckInfoInTextBoxes, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if (LocalIPwasFound && DataCorrect)
                    {
                        ClientListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        ClientListenSocket.Bind(ClientEndPoint);
                        ClientListenSocket.Listen(1);
                        MainBuilderForTextBox.AppendLine(MyResourses.Texts.TryingToConnect);
                        MessagesFromServerTextBox.Text = MainBuilderForTextBox.ToString();
                        ThreadPool.SetMinThreads(2, 2);
                        ThreadPool.QueueUserWorkItem(o => InitAndIdentityMessaging());
                        ThreadPool.QueueUserWorkItem(o => ListenServer());
                        this.Closing += CloseSockets;
                    }
                }
            }
        }
    }
}
