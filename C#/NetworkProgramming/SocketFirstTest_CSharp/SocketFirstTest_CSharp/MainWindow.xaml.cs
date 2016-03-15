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
        public Socket ClientSocket;
        public StringBuilder BuilderForTextBox = new StringBuilder();
        public Socket ServerSocket;
        public IPEndPoint ServerEndPoint = new IPEndPoint(IPAddress.Parse("192.168.6.88"),1500);
        public IPEndPoint FirstClientEndPoint = new IPEndPoint(IPAddress.Parse("192.168.6.88"), 1501);
        public IPEndPoint SecondClientEndPoint = new IPEndPoint(IPAddress.Parse("192.168.6.88"), 1502);
        public bool ServerShutDown = false;
        public string ClientTest = "I first client";

        public MainWindow()
        {
            InitializeComponent();
            ServerSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            ServerSocket.Bind(ServerEndPoint);
            ServerSocket.Listen(5);
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ThreadPool.QueueUserWorkItem(o => RunServer());
            ThreadPool.QueueUserWorkItem(o => RunClient());
        }

        public void RunServer()
        {
            byte[] BufferForMessage = new byte[5000];
            while (!ServerShutDown)
            {
                Socket BufSocket = ServerSocket.Accept();
                BuilderForTextBox.AppendLine(BufSocket.RemoteEndPoint.ToString());
                BufSocket.Receive(BufferForMessage);
                BuilderForTextBox.AppendLine(BufferForMessage.ToString());
                BufSocket.Receive(BufferForMessage);
                ConsoleTextBox.Text = BuilderForTextBox.ToString();
               // Thread.Sleep(1000);
            }           
        }

        public void RunClient()
        {
            ClientSocket.Connect(ServerEndPoint);
            ClientSocket.SendTo(System.Text.Encoding.UTF8.GetBytes(ClientTest), ServerEndPoint);
           // Thread.Sleep(1000);
        }
    }
}
