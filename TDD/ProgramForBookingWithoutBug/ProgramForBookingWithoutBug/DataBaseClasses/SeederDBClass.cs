using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramForBookingWithoutBug.DataBaseClasses
{
    public class BookingDBSeeder : CreateDatabaseIfNotExists<ContextForBookingDataBase>
    {
        protected override void Seed(ContextForBookingDataBase BookingDBcontext)
        {

            List<Station> AllStations = new List<Station>() { new Station("Минск"), new Station("Витебск"), new Station("Гродно"), new Station("Гомель"), new Station("Могилёв"), new Station("Брест"),
                new Station("Мозырь"), new Station("Солигорск"), new Station("Полоцк"), new Station("Орша")};

            List<Train> AllTrains = new List<Train>();

            Train BufForTrain;
            ICollection<Station> BufForStations;
            BufForTrain = new Train("Молниеносный_1901", 300);
            BufForTrain.Stations = new List<Station>();
            BufForTrain.Stations.Add(AllStations.Where((res) => res.StationName == "Брест").FirstOrDefault());
            BufForTrain.Stations.Add(AllStations.Where((res) => res.StationName == "Мозырь").FirstOrDefault());
            BufForTrain.Stations.Add(AllStations.Where((res) => res.StationName == "Витебск").FirstOrDefault());
            BufForTrain.Stations.Add(AllStations.Where((res) => res.StationName == "Могилёв").FirstOrDefault());

            AllTrains.Add(BufForTrain);

            BufForTrain = new Train("Быстрый_339", 250);
            BufForTrain.Stations = new List<Station>();
            BufForTrain.Stations.Add(AllStations.Where((res) => res.StationName == "Могилёв").FirstOrDefault());
            BufForTrain.Stations.Add(AllStations.Where((res) => res.StationName == "Солигорск").FirstOrDefault());
            BufForTrain.Stations.Add(AllStations.Where((res) => res.StationName == "Минск").FirstOrDefault());
            BufForTrain.Stations.Add(AllStations.Where((res) => res.StationName == "Гродно").FirstOrDefault());

            AllTrains.Add(BufForTrain);

            BufForTrain = new Train("Тихий_2281", 350);
            BufForTrain.Stations = new List<Station>();
            BufForTrain.Stations.Add(AllStations.Where((res) => res.StationName == "Гродно").FirstOrDefault());
            BufForTrain.Stations.Add(AllStations.Where((res) => res.StationName == "Полоцк").FirstOrDefault());
            BufForTrain.Stations.Add(AllStations.Where((res) => res.StationName == "Орша").FirstOrDefault());
            BufForTrain.Stations.Add(AllStations.Where((res) => res.StationName == "Гомель").FirstOrDefault());

            AllTrains.Add(BufForTrain);

            foreach (Station CurrentStation in AllStations)
            {
                BookingDBcontext.ListOfStations.Add(CurrentStation);
            }
            foreach (Train CurrentTrain in AllTrains)
            {
                BookingDBcontext.ListOfTrains.Add(CurrentTrain);
            }

            base.Seed(BookingDBcontext);
        }
    }
}
