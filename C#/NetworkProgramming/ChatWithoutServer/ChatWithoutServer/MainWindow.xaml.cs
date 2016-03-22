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
using System.Net.NetworkInformation;
using System.Threading;

namespace ChatWithoutServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static object RefreshConsoleGate = new object();
        public List<ClientAndMessage> AllMessages = new List<ClientAndMessage>();
        public bool ClientShutDown = false;
        public bool ClientWorking = false;
        public UdpClient ClientForSending = null;
        public IPEndPoint EndPointForMulticastIP = null;
        public UdpClient ClientListener = null;
        public int SendingPort = -1;
        public int RecevingPort = -1;
        public IPAddress MulticastIP = null;
        public char CharSeparator = '#';

        public MainWindow()
        {
            InitializeComponent();

            ClientNameTextBox.PreviewTextInput += CharsKiller.InputValidationNames;
            MulticastIPtextBox.PreviewTextInput += CharsKiller.InputValidationForIP;
            PortListenerTextBox.PreviewTextInput += CharsKiller.InputValidation;
            PortSendingTextBox.PreviewTextInput += CharsKiller.InputValidation;
            MessageTextBox.PreviewTextInput += CharsKiller.InputValidationNames;

            ChatListBox.ItemsSource = AllMessages;
        }

        public void ShutDown(object sender, EventArgs e)
        {
            ClientShutDown = true;
            if (ClientForSending != null)
            {
                ClientForSending.Close();
            }
            if (ClientListener != null)
            {
                ClientListener.Close();
            }
        }

        public static IPAddress GetBroadcastAddress(IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException(MyResourses.Texts.MaskAndIPnotMatch);

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }
            return new IPAddress(broadcastAddress);
        }

        public static IPAddress GetSubnetMask(IPAddress address)
        {
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (address.Equals(unicastIPAddressInformation.Address))
                        {
                            return unicastIPAddressInformation.IPv4Mask;
                        }
                    }
                }
            }
            throw new ArgumentException(MyResourses.Texts.CantFindSubNetMask);
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

        public void AddMessageToConsole(byte[] MessageInBytes)
        {
            try
            {
                string Message = Encoding.UTF8.GetString(MessageInBytes);
                lock (AllMessages)
                {
                    AllMessages.Add(new ClientAndMessage(Message.Split(CharSeparator)[0], Message.Split(CharSeparator)[1]));
                }
                lock (RefreshConsoleGate)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => ChatListBox.Items.Refresh()));
                }
            }
            catch (Exception CurrentException)
            {
                if (!ClientShutDown)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ListenForMessages()
        {
            try
            {
                IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 0);
                while (true)
                {
                    byte[] BufferForMessage = ClientListener.Receive(ref localEp);
                    ThreadPool.QueueUserWorkItem(o => AddMessageToConsole(BufferForMessage));
                }
            }
            catch (Exception CurrentException)
            {
                if (!ClientShutDown)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void StartClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ClientWorking)
            {
                try
                {
                    MulticastIP = IPAddress.Parse(MulticastIPtextBox.Text);
                    SendingPort = Int32.Parse(PortSendingTextBox.Text);
                    RecevingPort = Int32.Parse(PortListenerTextBox.Text);
                }
                catch
                {
                    MessageBox.Show(MyResourses.Texts.CheckEnteredParametres, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    MulticastIP = null;
                    SendingPort = -1;
                    RecevingPort = -1;
                    return;
                }
                try
                {
                    ClientListener = new UdpClient();
                    ClientListener.ExclusiveAddressUse = false;
                    IPEndPoint localEp = new IPEndPoint(IPAddress.Any, SendingPort);
                    ClientListener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    ClientListener.ExclusiveAddressUse = false;
                    ClientListener.Client.Bind(localEp);
                    ClientListener.JoinMulticastGroup(MulticastIP);

                    ClientForSending = new UdpClient();
                    ClientForSending.JoinMulticastGroup(MulticastIP);
                    ClientForSending.MulticastLoopback = true;
                    EndPointForMulticastIP = new IPEndPoint(MulticastIP, SendingPort);
                }
                catch (Exception CurrentException)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    if (ClientListener != null)
                    {
                        ClientListener.Close();
                        ClientListener = null;
                    }
                    if (ClientForSending != null)
                    {
                        ClientForSending.Close();
                        ClientForSending = null;
                    }
                    EndPointForMulticastIP = null;
                    return;
                }
                ThreadPool.QueueUserWorkItem(o => ListenForMessages());
                ClientStatusLabel.Content = MyResourses.Texts.Working;
                ClientWorking = true;
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.ClientAlreadyRunning, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SendMessage()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(new Action(() => ClientForSending.Send(Encoding.UTF8.GetBytes(ClientNameTextBox.Text + CharSeparator + MessageTextBox.Text), 
                    Encoding.UTF8.GetBytes(ClientNameTextBox.Text + CharSeparator + MessageTextBox.Text).Length, EndPointForMulticastIP)));
            }
            catch (Exception CurrentException)
            {
                if (!ClientShutDown)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientWorking)
            {
                if(ClientNameTextBox.Text != string.Empty)
                {
                    ThreadPool.QueueUserWorkItem(o => SendMessage());
                }
                else
                {
                    MessageBox.Show(MyResourses.Texts.ClientNameError, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.ClinentNotWorkingYet, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
