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
using ClientForDiskDataProvider.DriversInfoService;

namespace ClientForDiskDataProvider
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public bool ClientBusy = false;
        private object SafeProgramClosingGate = new object();
        private object StatusChangeGate = new object();
        public bool ShutDown = false;

        public MainWindow()
        {
            InitializeComponent();
            ThreadPool.SetMinThreads(4,4);
            this.Closing += OnClientShutDown;
        }

        public void OnClientShutDown(object sender, EventArgs e)
        {
            ShutDown = true;
            lock (StatusChangeGate)
            {
                StatusLabel.Content = MyResourses.Texts.StatusClosing;
            }
            lock (SafeProgramClosingGate)
            {

            }
        }

        public void GetDiskInfoFromService()
        {
            //Без прокси
            /*ChannelFactory<IDiskInfo> TestChannelFactory = new ChannelFactory<IDiskInfo>(new NetHttpBinding(), new EndpointAddress("http://localhost:8080/DiskInfoService/EndPoint_1"));
            IDiskInfo ChannelToService = TestChannelFactory.CreateChannel();*/
            //С прокси
            lock (SafeProgramClosingGate)
            {
                DiskInfoClient ClientProxy = new DiskInfoClient(MyResourses.Texts.EndPoint_2);
                try
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => ConsoleTextBox.Text = ClientProxy.GetDriversData()));
                    ClientProxy.Close();
                }
                catch (FaultException CurrentException)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    ClientProxy.Abort();
                }
                catch (CommunicationException CurrentException)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    ClientProxy.Abort();
                }
                catch (TimeoutException CurrentException)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    ClientProxy.Abort();
                }
                finally
                {
                    lock (StatusChangeGate)
                    {
                        if (!ShutDown)
                        Application.Current.Dispatcher.Invoke(new Action(() => StatusLabel.Content = MyResourses.Texts.StatusFree));
                        ClientBusy = false;
                    }
                }
            }
        }

        public void GetDriveInfoFromService(string DriveName)
        {
            lock (SafeProgramClosingGate)
            {
                //Ниже использован рекомендуемый способ закрытия прокси клиента
                DiskInfoClient ClientProxy = new DiskInfoClient(MyResourses.Texts.EndPoint_1);
                try
                {

                    MainDriveInfo CurrentDriveInfo = ClientProxy.GetOneDriveData(DriveName);
                    if (CurrentDriveInfo != null)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() => DriveInfoLabel.Content = CurrentDriveInfo.Name + " " + MyResourses.Texts.Free + " " +
                            CurrentDriveInfo.AvailableSpace + " " + MyResourses.Texts.GigaByte + " " + MyResourses.Texts.From + " " + CurrentDriveInfo.TotalSpace + " " + MyResourses.Texts.GigaByte));
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() => DriveInfoLabel.Content = MyResourses.Texts.CantFindDriveByName));
                    }
                    ClientProxy.Close();
                }
                catch (FaultException CurrentException)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    ClientProxy.Abort();
                }
                catch (CommunicationException CurrentException)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    ClientProxy.Abort();
                }
                catch (TimeoutException CurrentException)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    ClientProxy.Abort();
                }
                finally
                {
                    lock (StatusChangeGate)
                    {
                        if (!ShutDown)
                            Application.Current.Dispatcher.Invoke(new Action(() => StatusLabel.Content = MyResourses.Texts.StatusFree));
                        ClientBusy = false;
                    }
                }
            }
        }

        private void GetOneDriveInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ClientBusy)
            {
                ClientBusy = true;
                StatusLabel.Content = MyResourses.Texts.StatusBusy;
                string CurrentDriveName = DriveNameTextBox.Text;
                ThreadPool.QueueUserWorkItem(o => GetDriveInfoFromService(CurrentDriveName));
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.ProgramBusyError, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetAllDriversInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ClientBusy)
            {
                ClientBusy = true;
                StatusLabel.Content = MyResourses.Texts.StatusBusy;
                ThreadPool.QueueUserWorkItem(o => GetDiskInfoFromService());
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.ProgramBusyError, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
