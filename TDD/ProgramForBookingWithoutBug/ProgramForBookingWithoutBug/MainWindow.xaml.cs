using ProgramForBookingWithoutBug.DataBaseClasses;
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
using ProgramForBookingWithoutBug.Resources;

namespace ProgramForBookingWithoutBug
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (ContextForBookingDataBase BookingDBcontext = new ContextForBookingDataBase(NamesOfVariables.ConnectionStringName))
            {
                List<string> AllStations = new List<string>();
                foreach (Station CurrentStation in BookingDBcontext.ListOfStations)
                {
                    AllStations.Add(CurrentStation.StationName);
                }

                DepartureStationsComboBox.ItemsSource = AllStations;
                DestinationStationsComboBox.ItemsSource = AllStations;
            }

            DepartureStationsComboBox.PreviewTextInput += ComboBoxPreviewTextInput;
            DepartureStationsComboBox.DropDownOpened += ComboBoxMenuActivated;
            DepartureStationsComboBox.DropDownClosed += ComboBoxMenuDeactivated;

            DestinationStationsComboBox.PreviewTextInput += ComboBoxPreviewTextInput;
            DestinationStationsComboBox.DropDownOpened += ComboBoxMenuActivated;
            DestinationStationsComboBox.DropDownClosed += ComboBoxMenuDeactivated;
        }

        private void ComboBoxTextChanged(Object sender, EventArgs arguments)
        {

        }

        private void ComboBoxPreviewTextInput(Object sender, EventArgs arguments)
        {
            if ((sender as ComboBox).Text == Texts.ComboBoxInitText)
            {
                (sender as ComboBox).Text = string.Empty;
                (sender as ComboBox).Foreground = Brushes.Black;
            }
        }

        private void ComboBoxMenuDeactivated(Object sender, EventArgs arguments)
        {
            if ((sender as ComboBox).SelectedIndex < 0 && (sender as ComboBox).Text == string.Empty)
            {
                (sender as ComboBox).Text = Texts.ComboBoxInitText;
                (sender as ComboBox).Foreground = Brushes.LightGray;
            }
        }

        private void ComboBoxMenuActivated(Object sender, EventArgs arguments)
        {
            if ((sender as ComboBox).Text == Texts.ComboBoxInitText)
            {
                (sender as ComboBox).Text = string.Empty;
            }
            (sender as ComboBox).Foreground = Brushes.Black;
        }

        private void DepartureStationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DestinationStationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
