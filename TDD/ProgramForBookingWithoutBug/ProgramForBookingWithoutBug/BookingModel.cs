using ProgramForBookingWithoutBug.DataBaseClasses;
using ProgramForBookingWithoutBug.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramForBookingWithoutBug
{
    public static class BookingModel
    {
        /// <summary>
        /// Возвращает список имен станций из базы данных.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<string> GetNamesOfStations()
        {
            ObservableCollection<string> AllStations = new ObservableCollection<string>();
            using (ContextForBookingDataBase BookingDBcontext = new ContextForBookingDataBase(NamesOfVariables.ConnectionStringName))
            {
                foreach (Station CurrentStation in BookingDBcontext.ListOfStations)
                {
                    AllStations.Add(CurrentStation.StationName);
                }
            }

            return AllStations;
        }
    }
}
