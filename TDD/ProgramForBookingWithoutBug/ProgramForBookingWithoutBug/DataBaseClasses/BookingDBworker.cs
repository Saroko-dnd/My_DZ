using ProgramForBookingWithoutBug.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProgramForBookingWithoutBug.DataBaseClasses
{
    public static class BookingDBworker
    {
        public static MainWindow ApplicationMainWindow;

        /// <summary>
        /// Ищет в базе данных поед, который останавливается на двух указанных станциях
        /// и вызывает у класса MainWindow метод SearchOver с найденным поездом или null в качестве аргумента.
        /// </summary>
        /// <param name="DepartureStationName"></param>
        /// <param name="DestinationStationName"></param>
        /// <returns></returns>
        public static void SearchTrain(string DepartureStationName, string DestinationStationName)
        {
            Train ResultTrain = null;
            int AmountOfFoundStations;
            using (ContextForBookingDataBase BookingDBcontext = new ContextForBookingDataBase(NamesOfVariables.ConnectionStringName))
            {
                List<Train> AllTrains = new List<Train>();
                foreach (Train CurrentTrain in BookingDBcontext.ListOfTrains)
                {
                    AllTrains.Add(CurrentTrain);
                }

                bool TrainWasFound = false;

                foreach (Train CurrentTrain in AllTrains)
                {
                    AmountOfFoundStations = CurrentTrain.Stations.Where((res) => res.StationName == DepartureStationName || res.StationName == DestinationStationName).Count();
                    if (AmountOfFoundStations == 2)
                    {
                        ResultTrain = CurrentTrain;
                        Application.Current.Dispatcher.Invoke(new Action(() => ApplicationMainWindow.SearchOver(ResultTrain)));
                        TrainWasFound = true;
                        break;
                    }
                }
                if (!TrainWasFound)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => ApplicationMainWindow.SearchOver(null)));
                }
            }
        }

        public static void BookTicket(Train CurrentTrain)
        {
            using (ContextForBookingDataBase BookingDBcontext = new ContextForBookingDataBase(NamesOfVariables.ConnectionStringName))
            {
                BookingDBcontext.ListOfTrains.First((res) => res.TrainName == CurrentTrain.TrainName).AmountOfFreeTickets = CurrentTrain.AmountOfFreeTickets;
                BookingDBcontext.SaveChanges();
                Application.Current.Dispatcher.Invoke(new Action(() => ApplicationMainWindow.BookingReady()));
            }
        }
    }
}
