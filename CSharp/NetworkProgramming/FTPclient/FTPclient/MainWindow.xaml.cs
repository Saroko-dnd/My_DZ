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
        public static bool DowloadThreadWorking = false;
        public bool ServerReady = false;
        public static List<Button> GameFieldButtons = new List<Button>();

        public MainWindow()
        {
            InitializeComponent();

            System.Net.ServicePointManager.DefaultConnectionLimit = 10;

            GameFieldButtons.Add(Button_1);
            GameFieldButtons.Add(Button_2);
            GameFieldButtons.Add(Button_3);
            GameFieldButtons.Add(Button_4);
            GameFieldButtons.Add(Button_5);
            GameFieldButtons.Add(Button_6);
            GameFieldButtons.Add(Button_7);
            GameFieldButtons.Add(Button_8);
            GameFieldButtons.Add(Button_9);

            foreach (Button CurrentButton in GameFieldButtons)
            {
                CurrentButton.Click += TicTacToe.Play;
                CurrentButton.Content = string.Empty;
            }

            TicTacToe.GameStatusLabel = GameStatusLabel;
            TicTacToe.SelectorOfChars = ChooseSymbolComboBox;

            ThreadPool.SetMinThreads(4,4);
            //StartFtpRequestButton.Click += StartFtpRequestButton_Click;

            ChooseSymbolComboBox.ItemsSource = TicTacToe.GameSymbols;
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

        public static void StartGetDataFromFTPserver(string CharToPutInButton)
        {
            DowloadThreadWorking = true;
            while (true)
            {
                try
                {
                    string FileName = "ftp://" + GetLocalIPAddress() + "/GameInfo.txt";
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FileName);
                    request.KeepAlive = false;
                    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    StreamReader NewReaser = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
                    string FileTxt = NewReaser.ReadToEnd();
                    string Answer = FileTxt.Split('\r','\n')[FileTxt.Split('\r', '\n').Length - 3];
                    string[] TestStrings = FileTxt.Split('\r', '\n');
                    if (Answer == "new")
                    {
                        TicTacToe.FirstTime = true;
                        TicTacToe.WaitForAnswer = false;
                        TicTacToe.NumberOfLastPressedButton = -1;
                        Application.Current.Dispatcher.Invoke(new Action(() => RefresgGameField()));
                        DowloadThreadWorking = false;
                        break;
                    }
                    else if (Int32.Parse(Answer.Split(' ')[0]) != TicTacToe.NumberOfLastPressedButton)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() => GameFieldButtons.Where(res => res.Name.EndsWith(Answer.Split(' ')[0])).First().Content = CharToPutInButton));
                        Application.Current.Dispatcher.Invoke(new Action(() => TicTacToe.CheckWin()));
                        TicTacToe.WaitForAnswer = false;
                    }

                    NewReaser.Close();
                    response.Close();
                }
                catch (Exception CurrentException)
                {
                    //игнорируем использование файла другим процессом
                    if (!CurrentException.Message.Contains("451"))
                    {
                        MessageBox.Show(CurrentException.Message + "DOWNLOADING", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        DowloadThreadWorking = false;
                        break;
                    }
                }
                Thread.Sleep(1000);
            }


            //MessageBox.Show(NewReader.ReadToEnd() + " " + TestResponse.StatusDescription);
        }

        private void WaitForOpponentButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChooseSymbolComboBox.SelectedIndex >= 0)
            {
                TicTacToe.FirstTime = false;
                TicTacToe.WaitForAnswer = true;
                string CharToPutInButton = ChooseSymbolComboBox.SelectedItem.ToString();
                if (CharToPutInButton == "X")
                {
                    ThreadPool.QueueUserWorkItem(o => StartGetDataFromFTPserver("0"));
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(o => StartGetDataFromFTPserver("X"));
                }
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.YouMustSelectChar, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartNewGameButton_Click(object sender, RoutedEventArgs e)
        {
            TicTacToe.StartNewGame();//Сообщаем второму клиенту о том, чтобы он был готов начать новую игру
        }

        public static void RefresgGameField()
        {
            foreach (Button CurrentButton in GameFieldButtons)
            {
                CurrentButton.Content = string.Empty;
            }
        }
    }


}
