using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ProgramForBookingWithoutBug
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> allStations;

        private Visibility departureStationNotFoundLabelVisibility;
        private Visibility destinationStationNotFoundLabelVisibility;

        private Visibility searchDataAnimatedEllipseVisibility;
        private bool searchDataAnimationBegin;

        private string programStateLabelContent;
        private Brush programStateLabelForeground;

        public ObservableCollection<string> AllStations
        {
            get
            {
                return allStations;
            }

            set
            {
                allStations = value;
            }
        }

        public Visibility DepartureStationNotFoundLabelVisibility
        {
            get
            {
                return departureStationNotFoundLabelVisibility;
            }

            set
            {
                departureStationNotFoundLabelVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public Visibility DestinationStationNotFoundLabelVisibility
        {
            get
            {
                return destinationStationNotFoundLabelVisibility;
            }

            set
            {
                destinationStationNotFoundLabelVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public string ProgramStateLabelContent
        {
            get
            {
                return programStateLabelContent;
            }

            set
            {
                programStateLabelContent = value;
                NotifyPropertyChanged();
            }
        }

        public Brush ProgramStateLabelForeground
        {
            get
            {
                return programStateLabelForeground;
            }

            set
            {
                programStateLabelForeground = value;
                NotifyPropertyChanged();
            }
        }

        public Visibility SearchDataAnimatedEllipseVisibility
        {
            get
            {
                return searchDataAnimatedEllipseVisibility;
            }

            set
            {
                searchDataAnimatedEllipseVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public bool SearchDataAnimationBegin
        {
            get
            {
                return searchDataAnimationBegin;
            }

            set
            {
                searchDataAnimationBegin = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Вызывается когда класс BookingViewModel должен сообщить классу MainWindow об изменении значения конкретного свойства,  чтобы изменить 'привязанное' к нему значения в классе MainWindow
        /// (В данном случае об изменении свойства вызвавшего этот метод)
        /// </summary>
        /// <param name="PropertyName"></param>
        public void NotifyPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        public BookingViewModel()
        {
            AllStations = BookingModel.GetNamesOfStations();

            DepartureStationNotFoundLabelVisibility = Visibility.Collapsed;
            DestinationStationNotFoundLabelVisibility = Visibility.Collapsed;

            ProgramStateLabelContent = string.Empty;
            ProgramStateLabelForeground = Brushes.Orange;

            SearchDataAnimatedEllipseVisibility = Visibility.Collapsed;
            SearchDataAnimationBegin = false;
        }
    }
}
