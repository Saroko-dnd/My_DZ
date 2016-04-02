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
using System.IO;
using System.ServiceModel;
using System.Threading;
using ClientForDiskDataProvider.DiskInfoService;

namespace ClientForDiskDataProvider
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ThreadPool.SetMinThreads(4,4);
            ThreadPool.QueueUserWorkItem(o => GetDiskInfoFromService());
        }

        public void GetDiskInfoFromService()
        {
            //Без прокси
            /*ChannelFactory<IDiskInfo> TestChannelFactory = new ChannelFactory<IDiskInfo>(new NetHttpBinding(), new EndpointAddress("http://localhost:8080/DiskInfoService/EndPoint_1"));
            IDiskInfo ChannelToService = TestChannelFactory.CreateChannel();*/
            //С прокси
            DiskInfoClient ClientProxy = new DiskInfoClient();
            while (true)
            {
                Application.Current.Dispatcher.Invoke(new Action(() => ConsoleTextBox.Text = ClientProxy.GetDriversData()));
                Thread.Sleep(2000);
            }
        }
    }
}
