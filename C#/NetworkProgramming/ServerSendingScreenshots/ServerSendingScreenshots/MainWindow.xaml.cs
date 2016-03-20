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

namespace ServerSendingScreenshots
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IPEndPoint TestClientIPEndPoint = new IPEndPoint(IPAddress.Parse("192.168.100.3"), 6000);
        public TcpClient ScreenshotSender = new TcpClient();
        public NetworkStream SendingStream;
        public bool ServerShutDown = false;
        public static IntPtr PointerToBitmap;

        [DllImport("gdi32")]
        public static extern int DeleteObject(IntPtr ipObj);


        public MainWindow()
        {
            InitializeComponent();
            
            this.Closing += ShutDown;
            ScreenshotSender.Connect(TestClientIPEndPoint);
            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.QueueUserWorkItem(o => ScreenshotSendingToClients());
        }

        public void ShutDown(Object sender, EventArgs e)
        {
            ServerShutDown = true;
            ScreenshotSender.Close();
            SendingStream.Close();
        }

        public void ScreenshotSendingToClients()
        {
            try
            {
                IPEndPoint ClientTestEndPoint = new IPEndPoint(IPAddress.Parse("192.168.100.3"), 6000);
                ImageConverter converter = new ImageConverter();
                SendingStream = ScreenshotSender.GetStream();
                byte[] ScreenshotByteArray;
                while (true)
                {
                    ScreenshotByteArray = CopyScreen();
                    SendingStream.Write(ScreenshotByteArray, 0, ScreenshotByteArray.Length);
                    Thread.Sleep(1000);
                    DeleteObject(PointerToBitmap);
                }
            }
            catch (Exception CurrentException)
            {
                if (!ServerShutDown)
                {
                    MessageBox.Show(CurrentException.Message + "SERVER", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
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
