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
using System.Threading;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.Sockets;

namespace FTPclient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool ServerReady = false;

        public MainWindow()
        {
            InitializeComponent();

            TicTacToe.DrawGameField(GameFieldTextBox);
            ChooseSymbolComboBox.ItemsSource = TicTacToe.GameSymbols;
            //ThreadPool.QueueUserWorkItem(o => ListenPort());
        }



        public void ReadData(TcpClient CurrentTestClient)
        {
            NetworkStream CurTestStream = CurrentTestClient.GetStream();
            while (true)
            {
                using (StreamWriter writer = new StreamWriter(CurTestStream, Encoding.ASCII))
                using (StreamReader reader = new StreamReader(CurTestStream, Encoding.ASCII))
                {
                    writer.WriteLine("220 Ready!");
                    writer.Flush();

                    string line = null;

                    while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                    {
                        MessageBox.Show(line);

                        writer.WriteLine("502 I DON'T KNOW");
                        writer.Flush();
                    }
                }
            }
        }

        public void ListenPort()
        {
            TcpListener TestListener = new TcpListener(IPAddress.Any, 21);
            TestListener.Start();
            while (true)
            {
                TcpClient ClientTCP = TestListener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(o => ReadData(ClientTCP));
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

        private void StartFtpRequestButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder LocalStringBuilder = new StringBuilder();
            LocalStringBuilder.AppendLine("\r\n\r\n\r\n");
            LocalStringBuilder.AppendLine("Third new line");
            string FileName = "ftp://" + GetLocalIPAddress() + "/GameInfo.txt";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FileName);
            request.Method = WebRequestMethods.Ftp.AppendFile;
            request.ContentLength = Encoding.ASCII.GetBytes(LocalStringBuilder.ToString()).Length;//игнорируется???
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(Encoding.ASCII.GetBytes(LocalStringBuilder.ToString()), 0, Encoding.ASCII.GetBytes(LocalStringBuilder.ToString()).Length);
            requestStream.Close();
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();

            //MessageBox.Show(NewReader.ReadToEnd() + " " + TestResponse.StatusDescription);
        }
    }


}
