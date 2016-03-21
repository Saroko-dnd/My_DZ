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

namespace ChatWithoutServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool NewIPwasAdded = false;
        public UdpClient ClientForSending = null;
        public UdpClient ClientListener = null;
        public IPAddress BroadCastIP = null;
        List<IPAddress> BroadcastIPlist = new List<IPAddress>();
        public char CharSeparator = '#';

        public MainWindow()
        {
            InitializeComponent();

            ClientNameTextBox.PreviewTextInput += CharsKiller.InputValidationNames;
            NewIPTextBox.PreviewTextInput += CharsKiller.InputValidationForIP;
            BroadcastGroupComboBox.ItemsSource = BroadcastIPlist;
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

        private void AddNewIPButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lock (BroadcastIPlist)
                {
                    IPAddress NewIPaddress = IPAddress.Parse(NewIPTextBox.Text);
                    foreach (IPAddress CurrentIPadress in BroadcastIPlist)
                    {
                        if (CurrentIPadress.ToString() == NewIPaddress.ToString())
                        {
                            MessageBox.Show(MyResourses.Texts.CantAddCurrentIP, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    BroadcastIPlist.Add(NewIPaddress);
                    NewIPwasAdded = true;
                }
            }
            catch
            {
                MessageBox.Show(MyResourses.Texts.CantParseIP, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }       
        }
    }
}
