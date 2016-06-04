using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using TicketBookingInterfaceTest.MyResources;

namespace TicketBookingInterfaceTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ThreadPool.SetMinThreads(4, 4);

            //Заполняем базу данных, если она пустая
            using (StationsDataBase OurDBContext = new StationsDataBase(ValuableStringValues.ConnectionStringName))
            {
                if (OurDBContext.ListOfStations.Count() == 0)
                {
                    OurDBContext.ListOfStations.Add(new Station("Station_A"));
                    OurDBContext.ListOfStations.Add(new Station("Station_B"));
                    OurDBContext.RoutesAndTickets.Add(new RouteTicketsData("Station_A", "Station_B", 100, 1));
                    OurDBContext.RoutesAndTickets.Add(new RouteTicketsData("Station_B", "Station_A", 200, 2));
                    OurDBContext.SaveChanges();
                }

                List<String> ListOfDepartureStations = new List<string>();
                foreach (Station CurrentStationName in OurDBContext.ListOfStations)
                {
                    ListOfDepartureStations.Add(CurrentStationName.StationID);
                }

                DepartureStationsComboBox.ItemsSource = ListOfDepartureStations;
                int fff = DepartureStationsComboBox.SelectedIndex;
                DestinationStationsComboBox.ItemsSource = ListOfDepartureStations;

                List<string> TestListOfStations = new List<string>() {"Минск","Гродно","Молодечно","Гомель" };
                DepartureStationsComboBox.ItemsSource = TestListOfStations;

                ThreadPool.QueueUserWorkItem(o => SleepAndOpenListsForComboBoxes());

                DepartureStationsComboBox.DropDownOpened += ComboBoxMenuActivated;
                DepartureStationsComboBox.DropDownClosed += ComboBoxMenuDeactivated;

                DestinationStationsComboBox.DropDownOpened += ComboBoxMenuActivated;
                DestinationStationsComboBox.DropDownClosed += ComboBoxMenuDeactivated;
            }
        }

        private void ComboBoxMenuActivated(Object sender, EventArgs arguments)
        {
            (sender as ComboBox).Text = string.Empty;
            (sender as ComboBox).Foreground = Brushes.Black;
        }

        private void ComboBoxMenuDeactivated(Object sender, EventArgs arguments)
        {
            if ((sender as ComboBox).SelectedIndex < 0)
            {
                (sender as ComboBox).Text = Texts.ComboBoxInitText;
                (sender as ComboBox).Foreground = Brushes.LightGray;
            }
        }

        private void SleepAndOpenListsForComboBoxes()
        {
            /*Thread.Sleep(5000);
            Application.Current.Dispatcher.Invoke(new Action(() => DepartureStationsComboBox.IsDropDownOpen = true));
            Application.Current.Dispatcher.Invoke(new Action(() => DestinationStationsComboBox.IsDropDownOpen = true));*/
        }

        private void DepartureStationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartureStationsComboBox.SelectedIndex >= 0 )
            {

            }
        }

        private void DestinationStationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DestinationStationsComboBox.SelectedIndex >= 0)
            {

            }
        }
    }
}
