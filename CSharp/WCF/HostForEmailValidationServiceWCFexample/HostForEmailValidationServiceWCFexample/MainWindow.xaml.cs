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
using System.ServiceModel;
using EmailServiceWCFexample;

namespace HostForEmailValidationServiceWCFexample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public StringBuilder ConsoleStringBuilder = new StringBuilder();
        //создаем ServiceHost для класса проверяющего email адрес (тип службы) (1-й параметр) и указываем ссылку на эту службу (2-й параметр)
        public ServiceHost OurHostService = new ServiceHost(typeof(OurEmailValidator), new Uri("http://localhost:8080/"));
            
         
        public MainWindow()
        {
            InitializeComponent();

            //после запуска службы (Open()) ее больше нельзя настраивать
            OurHostService.Open();

            foreach (Uri uri in OurHostService.BaseAddresses)
            {
                ConsoleStringBuilder.AppendLine(uri.ToString());
            }
            ConsoleStringBuilder.AppendLine();
            ConsoleStringBuilder.AppendLine("Number of dispatchers listening : " + OurHostService.ChannelDispatchers.Count);
            foreach (System.ServiceModel.Dispatcher.ChannelDispatcher dispatcher in OurHostService.ChannelDispatchers)
            {
                ConsoleStringBuilder.AppendLine(dispatcher.Listener.Uri.ToString() + " " + dispatcher.BindingName);
            }
            ConsoleTextBox.Text = ConsoleStringBuilder.ToString();
        }
    }
}
