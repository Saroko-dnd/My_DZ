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
using System.Collections.Generic;
using System.Text;

namespace ServerSendingScreenshots
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<TcpClient> AllClientsConnections = new List<TcpClient>();
        public List<TcpClient> AllTCPclients = new List<TcpClient>();
        public static int PortSender = 6001;
        public static int PortListener = 6000;
        public TcpListener ClientWaitingListener = new TcpListener(PortListener);
        public TcpClient ScreenshotSender = new TcpClient();
        public NetworkStream SendingStream;
        public bool ServerShutDown = false;
        public static IntPtr PointerToBitmap;
        public char CharSeparator = '#';

        [DllImport("gdi32")]
        public static extern int DeleteObject(IntPtr ipObj);


        public MainWindow()
        {
            InitializeComponent();
            
            this.Closing += ShutDown;
            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.QueueUserWorkItem(o => ScreenshotSendingToClients());
            ThreadPool.QueueUserWorkItem(o => CollectClients());
        }

        public void ShutDown(Object sender, EventArgs e)
        {
            ServerShutDown = true;
            ScreenshotSender.Close();
            SendingStream.Close();
            ClientWaitingListener.Stop();
            lock (AllTCPclients)
            {
                lock (AllClientsConnections)
                {
                    foreach (TcpClient CurClient in AllTCPclients)
                    {
                        CurClient.Close();
                    }
                    AllTCPclients.Clear();
                    foreach (TcpClient CurClient in AllClientsConnections)
                    {
                        CurClient.Close();
                    }
                    AllClientsConnections.Clear();
                }
            }
        }

        public void AddClientEndPoint(TcpClient CurrentClient)
        {
            try
            {
                byte[] BufferForEndPointInfo = new byte[500];
                NetworkStream CurrentNetworkStream = null;
                CurrentNetworkStream = CurrentClient.GetStream();
                CurrentNetworkStream.Read(BufferForEndPointInfo, 0, BufferForEndPointInfo.Length);
                string EndPointInfo = Encoding.UTF8.GetString(BufferForEndPointInfo).TrimEnd('\0');
                TcpClient NewClient = new TcpClient();
                try
                {
                    NewClient.Connect(new IPEndPoint(IPAddress.Parse(EndPointInfo.Split(CharSeparator)[0]), Int32.Parse(EndPointInfo.Split(CharSeparator)[1])));
                    lock (AllClientsConnections)
                    {
                        AllClientsConnections.Add(NewClient);
                    }
                    lock (AllTCPclients)
                    {
                        AllTCPclients.Remove(CurrentClient);
                    }
                }
                catch
                {
                    MessageBox.Show(MyResourses.Texts.FailToConnect, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    NewClient.Close();
                }
                finally
                {
                    CurrentNetworkStream.Close();
                }
            }
            catch (Exception CurrentException)
            {
                if (!ServerShutDown)
                {
                    MessageBox.Show(CurrentException.Message + "SERVER_INFO_ENDPOINT", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void CollectClients()
        {
            try
            {
                ClientWaitingListener.Start();
                while (true)
                {
                    TcpClient NewClient = ClientWaitingListener.AcceptTcpClient();
                    lock (AllTCPclients)
                    {
                        AllTCPclients.Add(NewClient);
                    }
                    ThreadPool.QueueUserWorkItem(o => AddClientEndPoint(NewClient));
                }
            }
            catch (Exception CurrentException)
            {
                if (!ServerShutDown)
                {
                    MessageBox.Show(CurrentException.Message + "SERVER_COLLECT_CLIENTS", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ScreenshotSendingToClients()
        {
            try
            {
                byte[] ScreenshotByteArray;
                while (true)
                {
                    ScreenshotByteArray = CopyScreen();
                    List<TcpClient> ListToDelete = new List<TcpClient>();
                    lock (AllClientsConnections)
                    {
                        foreach (TcpClient CurrentClient in AllClientsConnections)
                        {
                            try
                            {
                                SendingStream = CurrentClient.GetStream();
                                SendingStream.Write(ScreenshotByteArray, 0, ScreenshotByteArray.Length);
                            }
                            catch (Exception CurrentException)
                            {
                                ListToDelete.Add(CurrentClient);
                            }
                        }
                        foreach (TcpClient CurClient in ListToDelete)
                        {
                            CurClient.Close();
                            AllClientsConnections.Remove(CurClient);
                        }
                    }
                    DeleteObject(PointerToBitmap);
                    Thread.Sleep(1000);
                }
            }
            catch (Exception CurrentException)
            {
                if (!ServerShutDown)
                {
                    MessageBox.Show(CurrentException.Message + "SERVER", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    //Application.Current.Dispatcher.Invoke(new Action(() => this.Close()));
                }
            }
        }

        public static BitmapSource ConvertBitmap(Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                          source.GetHbitmap(),
                          IntPtr.Zero,
                          Int32Rect.Empty,
                          BitmapSizeOptions.FromEmptyOptions());
        }

        public static Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            Bitmap bitmap;
            using (var outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);
            }
            return bitmap;
        }

        private static byte[] CopyScreen()
        {
            using (var screenBmp = new Bitmap(
                (int)SystemParameters.PrimaryScreenWidth,
                (int)SystemParameters.PrimaryScreenHeight,
                PixelFormat.Format32bppArgb))
            {
                using (var bmpGraphics = Graphics.FromImage(screenBmp))
                {
                    bmpGraphics.CopyFromScreen(0, 0, 0, 0, screenBmp.Size);
                    PointerToBitmap = screenBmp.GetHbitmap();
                    BitmapSource ScreenSource = Imaging.CreateBitmapSourceFromHBitmap(
                        PointerToBitmap,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                    Bitmap ScreenShot = BitmapFromSource(ScreenSource);
                    Bitmap bm3 = new Bitmap(ScreenShot);
                    ScreenShot.Dispose();
                    byte[] BufForScreenshot;
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        bm3.Save(imgStream, System.Drawing.Imaging.ImageFormat.Bmp);
                        BufForScreenshot = imgStream.ToArray();
                    }
                    bm3.Dispose();
                    bmpGraphics.Dispose();
                    return BufForScreenshot;
                }
            }
        }
    }
}
