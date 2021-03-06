﻿using System;
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
        public int StringCounter = 0;
        public int MaxAmountOfStrings = 100;
        public IPEndPoint ClientEndPoint;
        public IPEndPoint ServerEndPoint;
        public int PortNumber = -1;
        public int UDPPortNumberForTime = -1;
        public int UDPServerPort = 9016;
        public Socket ClientListenSocket = null;
        public Socket ClientSendSocket = null;
        public Socket ConnectionToServer = null;
        public UdpClient UdpClientForTimeSync = null;
        public bool ClientShutDown = false;
        public StringBuilder MainBuilderForTextBox = new StringBuilder();
        public bool ConnectionEstablished = false;
        public bool ServerReady = false;
        public char CharSeparator = '#';
        public bool ClientListenSocketWorking = false;
        public object SendMessageGate = new object();
        IPAddress ServerIP = null;
        

        public MainWindow()
        {
            InitializeComponent();

            ClientListenPortTextBox.PreviewTextInput += CharsKiller.InputValidation;
            ClientListenPortTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            ServerPortTextBox.PreviewTextInput += CharsKiller.InputValidation;
            ServerPortTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            IPofServerTextBox.PreviewTextInput += CharsKiller.InputValidationForIP;
            IPofServerTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            ClientNameTextBox.PreviewTextInput += CharsKiller.InputValidationNames;
            TargetClientNameTextBox.PreviewTextInput += CharsKiller.InputValidationNames;
            ClientUDPPortTextBox.PreviewTextInput += CharsKiller.InputValidation;
            ClientUDPPortTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            ServerUDPportTextBox.PreviewTextInput += CharsKiller.InputValidation;
            ServerUDPportTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            
            this.Closing += CloseSockets;
            ThreadPool.QueueUserWorkItem(o => ClearingConsole());
        }

        public void CloseSockets(object Sender,EventArgs e)
        {
            ClientShutDown = true;
            if (ClientListenSocket != null && ClientSendSocket != null)
            {
                if (ConnectionEstablished)
                {
                    try
                    {
                        lock (SendMessageGate)
                        {
                            ClientSendSocket.Send(Encoding.UTF8.GetBytes(MyResourses.Texts.ClientOff));
                        }
                    }
                    catch
                    {
                        MessageBox.Show(MyResourses.Texts.CantSendOffToServer, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    ClientListenSocket.Close();
                    ClientSendSocket.Close();
                    if (ConnectionToServer != null)
                        ConnectionToServer.Close();
                    UdpClientForTimeSync.Close();
                }
            }
        }

        public void ClearingConsole()
        {
            while (true)
            {
                lock (MainBuilderForTextBox)
                {
                    if (StringCounter > MaxAmountOfStrings)
                    {
                        StringCounter = 0;
                        MainBuilderForTextBox.Clear();
                    }
                }
                Thread.Sleep(1000);
            }
        }

        public void GetTimeFromServer()
        {
            try
            {
                IPEndPoint ClientUDPEndPoint = null;
                while (true)
                {                 
                    byte[] BufferForDateTime = UdpClientForTimeSync.Receive(ref ClientUDPEndPoint);
                    //Проверяем откуда сообщение
                    if (ClientUDPEndPoint.Address.ToString() == ServerIP.ToString())
                    {
                        string DateTimeString = Encoding.UTF8.GetString(BufferForDateTime).TrimEnd('\0');
                        Application.Current.Dispatcher.Invoke(new Action(() => ServerDateTimeLabel.Content = DateTimeString));
                    }
                }
            }
            catch (Exception CurrentException)
            {
                if (!ClientShutDown && ServerReady && ConnectionEstablished)
                {
                    MessageBox.Show(CurrentException.Message + "Client_init", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }     
        }

        public void InitAndIdentityMessaging()
        {
            try
            {
                ClientSendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    lock(SendMessageGate)
                    {
                        ClientSendSocket.Connect(ServerEndPoint);
                    }
                    ConnectionEstablished = true;
                }
                catch
                {
                    MessageBox.Show(MyResourses.Texts.ServerUnavailable, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    lock (MainBuilderForTextBox)
                    {
                        ++StringCounter;
                        MainBuilderForTextBox.AppendLine(MyResourses.Texts.ConnectionFail);
                        Application.Current.Dispatcher.Invoke(new Action(() => MessagesFromServerTextBox.Text = MainBuilderForTextBox.ToString()));
                    }
                    lock (SendMessageGate)
                    {
                        ClientSendSocket.Close();
                    }
                    ClientListenSocket.Close();
                    return;
                }
                //посылаем инитиируещее связь сообщение пока не получим подтверждение от сервера
                lock (SendMessageGate)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => ClientSendSocket.Send(Encoding.UTF8.GetBytes(GetLocalIPAddress() +
                        CharSeparator + PortNumber.ToString() + CharSeparator + ClientNameTextBox.Text + CharSeparator + UDPPortNumberForTime.ToString()))));
                }
                while (!ServerReady)
                {
                    if (!ConnectionEstablished)
                    {
                        return;
                    }
                    Thread.Sleep(100);
                }
                lock (MainBuilderForTextBox)
                {
                    ++StringCounter;
                    MainBuilderForTextBox.AppendLine(MyResourses.Texts.ConnectionSuccess);
                    Application.Current.Dispatcher.Invoke(new Action(() => MessagesFromServerTextBox.Text = MainBuilderForTextBox.ToString()));
                }
            }
            catch (Exception CurrentException)
            {
                if (!ClientShutDown && ServerReady && ConnectionEstablished)
                {
                    MessageBox.Show(CurrentException.Message + "Client_init", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ListenServer()
        {
            try
            {
                ConnectionToServer = ClientListenSocket.Accept();
                while (true)
                {
                    byte[] BufForMessage = new byte[500];
                    ConnectionToServer.Receive(BufForMessage);
                    if (Encoding.UTF8.GetString(BufForMessage).TrimEnd('\0') == MyResourses.Texts.ServerReady)
                    {
                        ServerReady = true;
                        UdpClientForTimeSync = new UdpClient(UDPPortNumberForTime);
                        ThreadPool.QueueUserWorkItem(o => GetTimeFromServer());
                    }
                    else if (Encoding.UTF8.GetString(BufForMessage).TrimEnd('\0') == MyResourses.Texts.ServerOff)
                    {
                        lock (MainBuilderForTextBox)
                        {
                            ++StringCounter;
                            MainBuilderForTextBox.AppendLine(MyResourses.Texts.ServerShutDown);
                            Application.Current.Dispatcher.Invoke(new Action(() => MessagesFromServerTextBox.Text = MainBuilderForTextBox.ToString()));
                        }
                        ServerReady = false;
                        ConnectionEstablished = false;
                        ConnectionToServer.Close();
                        ClientSendSocket.Close();
                        ClientListenSocket.Close();
                        UdpClientForTimeSync.Close();
                        return;
                    }
                    else
                    {
                        string MessageFromServer = Encoding.UTF8.GetString(BufForMessage).TrimEnd('\0');
                        string SenderName = MessageFromServer.Split(CharSeparator)[0];
                        string MessageItself = MessageFromServer.Split(CharSeparator)[1];
                        lock (MainBuilderForTextBox)
                        {
                            ++StringCounter;
                            MainBuilderForTextBox.AppendLine(MyResourses.Texts.MessageFrom + " " + SenderName);
                            MainBuilderForTextBox.AppendLine(MessageItself);
                            Application.Current.Dispatcher.Invoke(new Action(() => MessagesFromServerTextBox.Text = MainBuilderForTextBox.ToString()));
                        }
                    }
                }
            }
            catch (Exception CurrentException)
            {
                if (!ClientShutDown && ConnectionEstablished)
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
                    UDPPortNumberForTime = Int32.Parse(ClientUDPPortTextBox.Text);
                    UDPServerPort = Int32.Parse(ServerUDPportTextBox.Text);
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
                        lock (MainBuilderForTextBox)
                        {
                            ++StringCounter;
                            MainBuilderForTextBox.AppendLine(MyResourses.Texts.TryingToConnect);
                            MessagesFromServerTextBox.Text = MainBuilderForTextBox.ToString();
                        }
                        ServerIP = IPAddress.Parse(IPofServerTextBox.Text);
                        ThreadPool.SetMinThreads(2, 2);
                        ThreadPool.QueueUserWorkItem(o => InitAndIdentityMessaging());
                        ThreadPool.QueueUserWorkItem(o => ListenServer());
                    }
                }
            }
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionEstablished)
            {
                try
                {
                    lock (SendMessageGate)
                    {
                        ClientSendSocket.Send(Encoding.UTF8.GetBytes(MyResourses.Texts.ClientOff));
                    }
                }
                catch
                {
                    MessageBox.Show(MyResourses.Texts.CantSendOffToServer, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                ConnectionEstablished = false;
                ServerReady = false;

                ClientListenSocket.Close();
                ClientSendSocket.Close();
                ConnectionToServer.Close();
                UdpClientForTimeSync.Close();

                lock(MainBuilderForTextBox)
                {
                    ++StringCounter;
                    MainBuilderForTextBox.AppendLine(MyResourses.Texts.ConnectionOffByClient);
                    MessagesFromServerTextBox.Text = MainBuilderForTextBox.ToString();
                }
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.ConnectionNotExist, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionEstablished && ServerReady)
            {
                if (TargetClientNameTextBox.Text == string.Empty)
                {
                    MessageBox.Show(MyResourses.Texts.TargetUnknown, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        lock (SendMessageGate)
                        {
                            ClientSendSocket.Send(Encoding.UTF8.GetBytes(TargetClientNameTextBox.Text + CharSeparator + MessageToServerTextBox.Text));
                        }
                        lock (MainBuilderForTextBox)
                        {
                            ++StringCounter;
                            MainBuilderForTextBox.AppendLine(MyResourses.Texts.MessageWasSendToServer);
                            MessagesFromServerTextBox.Text = MainBuilderForTextBox.ToString();
                        }
                        MessageToServerTextBox.Text = string.Empty;
                    }
                    catch
                    {
                        MessageBox.Show(MyResourses.Texts.CantSendMessageConnectProblem, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.CantSendMessage, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
