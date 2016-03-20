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

namespace ClientReceiveScreenShot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public TcpListener ReceiverOfScreenshots = new TcpListener(6000);
        public bool ClientShutDown = false;
        public NetworkStream StreamForScreenshots;
        public Bitmap BitmapForScreenshot = null;

        [DllImport("gdi32")]
        public static extern int DeleteObject(IntPtr ipObj);

        public MainWindow()
        {
            InitializeComponent();
            this.Closing += ShutDown;
            ReceiverOfScreenshots.Start();
            ThreadPool.QueueUserWorkItem(o => ReceivingScreenshotsFromServer());
        }

        public void ShutDown(Object sender, EventArgs e)
        {
            ClientShutDown = true;
            ReceiverOfScreenshots.Stop();
            StreamForScreenshots.Close();
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
                    MessageBox.Show(CurrentException.Message + "CLIENT", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
