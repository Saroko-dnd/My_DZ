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
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SocketFirstTest_CSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// //SERVER side
    public partial class MainWindow : Window
    {
        public StringBuilder MainStringBuilder = new StringBuilder(); 
        public Socket ServerSocket;
        public List<ClientMessage> AllClientsMessages = new List<ClientMessage>();
        public List<Socket> AllConnectedSockets = new List<Socket>();
        public IPEndPoint ServerEndPoint;
        public bool ServerShutDown = false;
        public char CharSeparator = '#';
        public int PortNumber = 9015;
        public bool InstanceOfServerAlreadyRunning = false;
        public Mutex OnlyOneServerGate = null;

        public MainWindow()
        {
            InitializeComponent();


            Mutex.TryOpenExisting(MyResourses.Texts.MutexName, out OnlyOneServerGate);

            if (OnlyOneServerGate == null)
            {
                OnlyOneServerGate = new Mutex(true, MyResourses.Texts.MutexName);

                bool LocalIpWasFound = false;

                try
                {
                    ServerEndPoint = new IPEndPoint(IPAddress.Parse(GetLocalIPAddress()), PortNumber);
                    LocalIpWasFound = true;
                }
                catch (Exception CurrentException)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (LocalIpWasFound)
                {
                    ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ServerSocket.Bind(ServerEndPoint);
                    ServerSocket.Listen(10);
                    ThreadPool.SetMinThreads(2, 2);
                    ThreadPool.QueueUserWorkItem(o => RunServer());
                    this.Closing += ShutDownServer;
                }
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.ServerAlreadyRunning, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                InstanceOfServerAlreadyRunning = true;
                this.Close();
            }
        }

        public void SendMessagesToClient(BoolClass CloseCurrentSocketsGate,Socket SocketForMessaging, IPEndPoint EndPointForMessaging, string CurrentClientName)
        {
            try
            {
                StringBuilder BuilderForTextBox = new StringBuilder();
                lock(CloseCurrentSocketsGate)
                {
                    if (!CloseCurrentSocketsGate.SocketClosed)
                    {
                        SocketForMessaging.Connect(EndPointForMessaging);
                        //посылаем клиенту сигнал что сервер готов к приему сообщений
                        SocketForMessaging.Send(Encoding.UTF8.GetBytes(MyResourses.Texts.ServerAnswer));
                    }
                    else
                    {
                        return;
                    }
                }

                //Посылаем клиенту предназначенные ему сообщения и удаляем их
                while (true)
                {
                    lock (AllClientsMessages)
                    {
                        List<ClientMessage> DeleteList = new List<ClientMessage>();
                        foreach (ClientMessage CurrentMessage in AllClientsMessages)
                        {
                            if (CurrentMessage.ClientName == CurrentClientName)
                            {
                                BuilderForTextBox.AppendLine(MyResourses.Texts.ServerSendMessage);
                                BuilderForTextBox.Append(" " + CurrentClientName);
                                lock (CloseCurrentSocketsGate)
                                {
                                    if (!CloseCurrentSocketsGate.SocketClosed)
                                    {
                                        SocketForMessaging.Send(Encoding.UTF8.GetBytes(CurrentMessage.Message));
                                    }
                                    else
                                    {
                                        return;
                                    }                                 
                                }
                                DeleteList.Add(CurrentMessage);
                                lock (MainStringBuilder)
                                {
                                    MainStringBuilder.AppendLine(BuilderForTextBox.ToString());
                                    Application.Current.Dispatcher.Invoke(new Action(() => ConsoleTextBox.Text = MainStringBuilder.ToString()));
                                }
                                BuilderForTextBox.Clear();
                            }
                        }
                        foreach (ClientMessage CurrentMessage in DeleteList)
                        {
                            AllClientsMessages.Remove(CurrentMessage);
                        }
                    }
                    Thread.Sleep(200);
                }
            }
            catch (Exception CurrentException)
            {
                if (!ServerShutDown)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
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

        public void ShutDownServer(Object sender,EventArgs e)
        {
            if (!InstanceOfServerAlreadyRunning)
            {
                ServerShutDown = true;
                ServerSocket.Close();
                lock (AllConnectedSockets)
                {
                    foreach (Socket CurrentSocket in AllConnectedSockets)
                    {
                        CurrentSocket.Close();
                    }
                }
                OnlyOneServerGate.Close();
            }
        }

        public void RunServer()
        {
            try
            {
                while (true)
                {
                    Socket BufSocket = ServerSocket.Accept();
                    lock (AllConnectedSockets)
                    {
                        AllConnectedSockets.Add(BufSocket);
                    }
                    ThreadPool.QueueUserWorkItem(o => RunConnectionWithClient(BufSocket));
                }
            }
            catch (Exception CurrentException)
            {
                if (!ServerShutDown)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void RunConnectionWithClient(Socket SocketForClient)
        {
            try
            {
                BoolClass CloseCurrentSocketsGate = new BoolClass();
                StringBuilder BuilderForTextBox = new StringBuilder();
                byte[] BufferForMessage = new byte[500];
                string ClientListenSocket = string.Empty;
                SocketForClient.Receive(BufferForMessage);
                ClientListenSocket = Encoding.UTF8.GetString(BufferForMessage).TrimEnd('\0');
                string CurrentClientName = ClientListenSocket.Split(CharSeparator)[2];

                Socket SocketForMessaging = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                lock (AllConnectedSockets)
                {
                    AllConnectedSockets.Add(SocketForMessaging);
                }


                ThreadPool.QueueUserWorkItem(o => SendMessagesToClient(CloseCurrentSocketsGate, SocketForMessaging, new IPEndPoint(IPAddress.Parse(ClientListenSocket.Split(CharSeparator)[0]),
                    Int32.Parse(ClientListenSocket.Split(CharSeparator)[1])), ClientListenSocket.Split(CharSeparator)[2]));
                BuilderForTextBox.AppendLine(MyResourses.Texts.ConnectionFound);
                BuilderForTextBox.AppendLine(SocketForClient.RemoteEndPoint.ToString());
                lock (MainStringBuilder)
                {
                    MainStringBuilder.AppendLine(BuilderForTextBox.ToString());
                    Application.Current.Dispatcher.Invoke(new Action(() => ConsoleTextBox.Text = MainStringBuilder.ToString()));
                }
                while (true)
                {
                    BuilderForTextBox.Clear();
                    SocketForClient.Receive(BufferForMessage);
                    string MessageText = Encoding.UTF8.GetString(BufferForMessage).TrimEnd('\0');
                    if (MessageText == MyResourses.Texts.ClientOff)
                    {
                        lock (CloseCurrentSocketsGate)
                        {
                            CloseCurrentSocketsGate.SocketClosed = true;
                            SocketForClient.Close();
                            SocketForMessaging.Close();
                        }
                        lock (AllConnectedSockets)
                        {
                            AllConnectedSockets.Remove(SocketForMessaging);
                            AllConnectedSockets.Remove(SocketForClient);
                        }
                        BuilderForTextBox.AppendLine(MyResourses.Texts.ClientOffServerLogMessage);
                        BuilderForTextBox.Append(" " + CurrentClientName);
                        lock (MainStringBuilder)
                        {
                            MainStringBuilder.AppendLine(BuilderForTextBox.ToString());
                            Application.Current.Dispatcher.Invoke(new Action(() => ConsoleTextBox.Text = MainStringBuilder.ToString()));
                        }
                        return;
                    }
                    else if (MessageText == ClientListenSocket)
                    {
                        //do nothing, wait for proper message
                    }
                    else
                    {
                        if (MessageText.Split(CharSeparator).Length > 1)
                        {
                            BuilderForTextBox.AppendLine(MyResourses.Texts.ServerGetMessage);
                            BuilderForTextBox.AppendLine(CurrentClientName);
                            lock (AllClientsMessages)
                            {
                                AllClientsMessages.Add(new ClientMessage(MessageText.Split(CharSeparator)[0], MessageText.Split(CharSeparator)[1]));
                            }
                            lock (MainStringBuilder)
                            {
                                MainStringBuilder.AppendLine(BuilderForTextBox.ToString());
                                Application.Current.Dispatcher.Invoke(new Action(() => ConsoleTextBox.Text = MainStringBuilder.ToString()));
                            }
                        }
                    }
                }
            }
            catch (Exception CurrentException)
            {
                if (!ServerShutDown)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
