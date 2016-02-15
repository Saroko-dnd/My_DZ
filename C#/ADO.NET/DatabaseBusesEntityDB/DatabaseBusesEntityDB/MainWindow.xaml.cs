using DatabaseBusesEntityDB.DBContext;
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

namespace DatabaseBusesEntityDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                /*TransportDBContext OurContext = new TransportDBContext("TransportConnectionString");
                OurContext.VehicleTypes.Add(new TypeOfVehicle() { TypeName = "трамвай" });
                OurContext.VehicleTypes.Add(new TypeOfVehicle() { TypeName = "автобус" });
                OurContext.VehicleTypes.Add(new TypeOfVehicle() { TypeName = "троллейбус" });
                OurContext.SaveChanges();*/
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        private async void AddTypeButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = true;
            try
            {
                TransportTypeAddProgressBar.Visibility = Visibility.Visible;
                AddTypeLabel.Foreground = Brushes.Black;
                AddTypeLabel.Content = MyResourses.Texts.AddDataOperationInProgress;
                TransportDBContext OurContext = new TransportDBContext("TransportConnectionString");
                OurContext.VehicleTypes.Add(new TypeOfVehicle() { TypeName = TransportTypeTextBox.Text });
                TransportTypeTextBox.Clear();
                await OurContext.SaveChangesAsync();
            }
            catch (Exception CurrentException)
            {
                success = false;
                MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                TransportTypeAddProgressBar.Visibility = Visibility.Hidden;
                if (success)
                {
                    AddTypeLabel.Foreground = Brushes.Green;
                    AddTypeLabel.Content = MyResourses.Texts.AddDataOperationFinished;
                }
                else
                {
                    AddTypeLabel.Foreground = Brushes.Red;
                    AddTypeLabel.Content = MyResourses.Texts.AddDataOperationFailed;
                }
            }
        }
    }
}
