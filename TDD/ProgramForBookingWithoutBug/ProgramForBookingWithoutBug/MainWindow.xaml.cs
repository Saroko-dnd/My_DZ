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
using System.Windows.Media.Animation;
using System.Threading;

namespace ProgramForBookingWithoutBug
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool NoNeedForComboBoxTextChangedEvent = false;
        private bool ProgramBusy = false;
        private Train CurrentTrain = null;
        private string CurrentTrainInfo = string.Empty;

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

            ThreadPool.SetMinThreads(4, 4);

            BookingDBworker.ApplicationMainWindow = this;
        }

        private void ComboBoxTextChanged(Object sender, EventArgs arguments)
        {
            if (NoNeedForComboBoxTextChangedEvent)
            {
                NoNeedForComboBoxTextChangedEvent = false;
            }
            else
            {
                if ((sender as ComboBox).Text == string.Empty)
                {
                    (sender as ComboBox).Text = Texts.ComboBoxInitText;
                    (sender as ComboBox).Foreground = Brushes.LightGray;
                }
            }
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
            NoNeedForComboBoxTextChangedEvent = true;
            if ((sender as ComboBox).SelectedIndex < 0 && (sender as ComboBox).Text == string.Empty)
            {
                (sender as ComboBox).Text = Texts.ComboBoxInitText;
                (sender as ComboBox).Foreground = Brushes.LightGray;
            }
        }

        private void ComboBoxMenuActivated(Object sender, EventArgs arguments)
        {
            NoNeedForComboBoxTextChangedEvent = true;
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

        private void FindTrainButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProgramBusy)
            {
                MessageBox.Show(Texts.ErrorProgramBusy, Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (DepartureStationsComboBox.Text != Texts.ComboBoxInitText && DestinationStationsComboBox.Text != Texts.ComboBoxInitText)
                {
                    BookTicketButton.Visibility = Visibility.Collapsed;
                    Storyboard StoryBoardForSearchAnimation = this.FindResource("LoadingAnimationStoryBoard") as Storyboard;
                    ProgramStateLabel.Foreground = Brushes.Orange;
                    ProgramStateLabel.Content = Texts.LabelProgramStateSearch;
                    SearchDataAnimatedEllipse.Visibility = Visibility.Visible;
                    StoryBoardForSearchAnimation.Begin();
                    string DepartureStation = DepartureStationsComboBox.Text;
                    string DestinationStation = DestinationStationsComboBox.Text;
                    ThreadPool.QueueUserWorkItem(o => BookingDBworker.SearchTrain(DepartureStation, DestinationStation));
                }
                else
                {
                    MessageBox.Show(Texts.ErrorNotEnoughData, Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Оповещаетпользователя о результатах поиска поезда.  (Совершает необходимые изменения визуальных элементов главного окна приложения)
        /// Кроме того 'освобождает' программу для следующего запроса на поиск поезда.
        /// </summary>
        /// <param name="FoundTrain"></param>
        public void SearchOver(Train FoundTrain, bool DepartureStationFound, bool DestinationStationFound)
        {
            CurrentTrain = FoundTrain;
            Storyboard StoryBoardForSearchAnimation = null;
            StoryBoardForSearchAnimation = this.FindResource("LoadingAnimationStoryBoard") as Storyboard;
            StoryBoardForSearchAnimation.Stop();
            SearchDataAnimatedEllipse.Visibility = Visibility.Collapsed;

            if (!DepartureStationFound)
            {
                DepartureStationNotFoundLabel.Visibility = Visibility.Visible;
            }
            else
            {
                DepartureStationNotFoundLabel.Visibility = Visibility.Collapsed;
            }
            if (!DestinationStationFound)
            {
                DestinationStationNotFoundLabel.Visibility = Visibility.Visible;
            }
            else
            {
                DestinationStationNotFoundLabel.Visibility = Visibility.Collapsed;
            }

            ShowInfoAboutFoundTrainToUser(FoundTrain);
            ProgramBusy = false;
        }

        private void ShowInfoAboutFoundTrainToUser(Train FoundTrain)
        {
            if (FoundTrain != null)
            {
                ProgramStateLabel.Foreground = Brushes.Green;
                ProgramStateLabel.Content = Texts.LabelProgramStateReady;
                CurrentTrainInfo = FoundTrain.TrainName + " " + FoundTrain.Stations.First().StationName + " - " + FoundTrain.Stations.Last().StationName;
                TrainInfoLabel.Content = CurrentTrainInfo;
                TrainInfoLabel.Foreground = Brushes.Blue;
                AmountOfTicketsLabel.Content = FoundTrain.AmountOfFreeTickets.ToString();
                AmountOfTicketsLabel.Foreground = Brushes.Blue;
                if (CurrentTrain.AmountOfFreeTickets > 0)
                {
                    BookTicketButton.Visibility = Visibility.Visible;
                }
            }
            else
            {
                ProgramStateLabel.Foreground = Brushes.Red;
                ProgramStateLabel.Content = Texts.LabelProgramStateTrainNotFound;
                TrainInfoLabel.Content = Texts.LabelsForValuesErrorText;
                TrainInfoLabel.Foreground = Brushes.Red;
                AmountOfTicketsLabel.Content = Texts.LabelsForValuesErrorText;
                AmountOfTicketsLabel.Foreground = Brushes.Red;
            }
        }

        private void BookTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ProgramBusy)
            {
                ProgramBusy = true;
                WaitUntilBookingFinishLabel.Visibility = Visibility.Visible;
                WaitUntilBookingFinishAnimatedEllipse.Visibility = Visibility.Visible;
                Storyboard StoryBoardForhWaitAnimation = this.FindResource("WaitUntilBookingFinishAnimationStoryBoard") as Storyboard;
                StoryBoardForhWaitAnimation.Begin();
                BookTicketButton.Visibility = Visibility.Collapsed;
                CurrentTrain.AmountOfFreeTickets -= 1;
                ThreadPool.QueueUserWorkItem(o => BookingDBworker.BookTicket(CurrentTrain));
            }
        }

        public void BookingReady()
        {
            Storyboard StoryBoardForhWaitAnimation = this.FindResource("WaitUntilBookingFinishAnimationStoryBoard") as Storyboard;
            StoryBoardForhWaitAnimation.Stop();
            WaitUntilBookingFinishLabel.Visibility = Visibility.Collapsed;
            WaitUntilBookingFinishAnimatedEllipse.Visibility = Visibility.Collapsed;
            MessageBox.Show(Texts.NotificationBookingReady + " " + CurrentTrainInfo + "!", Texts.Notification, MessageBoxButton.OK, MessageBoxImage.Information);
            AmountOfTicketsLabel.Content = CurrentTrain.AmountOfFreeTickets.ToString();
            if (CurrentTrain.AmountOfFreeTickets > 0)
            {
                BookTicketButton.Visibility = Visibility.Visible;
            }
            ProgramBusy = false;
        }
    }
}
