using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ClientReceiveScreenShot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

        public int PortForTcpListener = -1;
        public int ServerPort = 6000;
        public IPAddress ServerIP = null;
        public TcpListener ReceiverOfScreenshots = null;
        public TcpClient SenderOfInitMessage = null;
        public bool ClientShutDown = true;
        public NetworkStream StreamForInitMessage = null;
        public NetworkStream StreamForScreenshots = null;
        public Bitmap BitmapForScreenshot = null;
        public char CharSeparator = '#';
        public int FailTimeout = 2000;

        [DllImport("gdi32")]
        public static extern int DeleteObject(IntPtr ipObj);

        public MainWindow()
        {
            InitializeComponent();

            TCPportNumberTextBox.PreviewTextInput += CharsKiller.OnlyNumbers;
            ServerPortTextBox.PreviewTextInput += CharsKiller.InputValidationForIP;

            this.Closing += ShutDown;
        }

        public void ShutDown(Object sender, EventArgs e)
        {
            if (!ClientShutDown)
            {
                ClientShutDown = true;
                if (ReceiverOfScreenshots != null)
                {
                    ReceiverOfScreenshots.Stop();
                    ReceiverOfScreenshots = null;
                }
                if (StreamForScreenshots != null)
                {
                    StreamForScreenshots.Close();
                    StreamForScreenshots = null;
                }
                if (StreamForInitMessage != null)
                {
                    StreamForInitMessage.Close();
                    StreamForInitMessage = null;
                }
                if (SenderOfInitMessage != null)
                {
                    SenderOfInitMessage.Close();
                    SenderOfInitMessage = null;
                }
            }
        }

        public void SendInitMessage()
        {
            SenderOfInitMessage = new TcpClient();
            try
            {
                SenderOfInitMessage.Connect(new IPEndPoint(ServerIP, ServerPort));
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(MyResourses.Texts.ConnectionFailed, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                ShutDown(null, null);
                return;
            }
            try
            {
                StreamForInitMessage = SenderOfInitMessage.GetStream();
                StreamForInitMessage.WriteTimeout = FailTimeout;
                StreamForInitMessage.Write(Encoding.UTF8.GetBytes(GetLocalIPAddress() + CharSeparator + PortForTcpListener.ToString()), 0, 
                    Encoding.UTF8.GetBytes(GetLocalIPAddress() + CharSeparator + PortForTcpListener.ToString()).Length);
                StreamForInitMessage.Close();
                SenderOfInitMessage.Close();
                SenderOfInitMessage = null;
                StreamForInitMessage = null;
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                ShutDown(null, null);
                return;
            }
        }

        public void ReceivingScreenshotsFromServer()
        {
            try
            {
                IPEndPoint SenderIPEndPoint = null;

                StreamForScreenshots = ReceiverOfScreenshots.AcceptTcpClient().GetStream();
                byte[] BufferForBitmap = new byte[9000000];
                while (true)
                {
                    StreamForScreenshots.Read(BufferForBitmap,0, BufferForBitmap.Length);
                    if (BitmapForScreenshot != null)
                    {
                        BitmapForScreenshot.Dispose();
                    }
                    using (MemoryStream ms = new MemoryStream(BufferForBitmap))
                    {
                        Bitmap Screenshot = (Bitmap)Image.FromStream(ms);
                        BitmapForScreenshot = new Bitmap(Screenshot);
                        Screenshot.Dispose();
                        IntPtr PointerToBitmap = BitmapForScreenshot.GetHbitmap();
                        Action MyDelegate = new Action(() => ScreenShotImage.Source = Imaging.CreateBitmapSourceFromHBitmap(
                            PointerToBitmap,
                            IntPtr.Zero,
                            Int32Rect.Empty,
                            BitmapSizeOptions.FromEmptyOptions()));
                        Application.Current.Dispatcher.Invoke(MyDelegate);
                        DeleteObject(PointerToBitmap);
                    }
                }
            }
            catch (Exception CurrentException)
            {
                if (!ClientShutDown)
                {
                    MessageBox.Show(CurrentException.Message + "CLIENT_RUN", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        //Метод возвращает скриншот экрана для Image control
        private static BitmapSource CopyScreen()
        {
            using (var screenBmp = new Bitmap(
                (int)SystemParameters.PrimaryScreenWidth,
                (int)SystemParameters.PrimaryScreenHeight,
                PixelFormat.Format32bppArgb))
            {
                using (var bmpGraphics = Graphics.FromImage(screenBmp))
                {
                    bmpGraphics.CopyFromScreen(0, 0, 0, 0, screenBmp.Size);
                    IntPtr PointerToBitmap = screenBmp.GetHbitmap();
                    BitmapSource Result = Imaging.CreateBitmapSourceFromHBitmap(
                        PointerToBitmap,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                    DeleteObject(PointerToBitmap);
                    return Result;
                }
            }
        }

        private void ClientStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientShutDown)
            {
                try
                {
                    PortForTcpListener = Int32.Parse(TCPportNumberTextBox.Text);
                    ServerIP = IPAddress.Parse(ServerPortTextBox.Text);
                }
                catch
                {
                    MessageBox.Show(MyResourses.Texts.CheckEnteredParametres, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    PortForTcpListener = -1;
                    ServerIP = null;
                    return;
                }
                if (PortForTcpListener > 0)
                {
                    try
                    {
                        ClientShutDown = false;
                        ReceiverOfScreenshots = new TcpListener(PortForTcpListener);
                        ReceiverOfScreenshots.Start();
                        ThreadPool.QueueUserWorkItem(o => ReceivingScreenshotsFromServer());
                        ThreadPool.QueueUserWorkItem(o => SendInitMessage());
                    }
                    catch (Exception CurrentException)
                    {
                        MessageBox.Show(CurrentException.Message + "CLIENT_START", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        if (ReceiverOfScreenshots != null)
                        {
                            ShutDown(null, null);
                        }
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(MyResourses.Texts.CheckEnteredParametres, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    PortForTcpListener = -1;
                    ServerIP = null;
                    return;
                }
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.ConnectionAlreadyExist, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ClientShutDown)
            {
                ShutDown(null, null);
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.DisconnectImpossible, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
