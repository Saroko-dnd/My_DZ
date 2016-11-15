using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceDataAccess
{
    class RaceWebsiteDBInitializer : CreateDatabaseIfNotExists<RaceWebsiteDbContext>
    {
        protected override void Seed(RaceWebsiteDbContext Context)
        {
            List<Car> TestListOfCars = new List<Car>();

            TestListOfCars.Add(new Car("FF0000", "First car", 100));
            TestListOfCars.Add(new Car("0011FF", "Second car", 150));
            TestListOfCars.Add(new Car("48A41A", "Third car", 50));

            List<Racer> TestListOfRacers = new List<Racer>();

            int CounterOfRacers = 1;
            foreach (Car CurrentTestCar in TestListOfCars)
            {
                TestListOfRacers.Add(new Racer("RacerFirstName_" + CounterOfRacers.ToString(), "RacerSecondName_" + CounterOfRacers.ToString(), "Racer biography " + CounterOfRacers.ToString()));
                TestListOfRacers.Last().SelectedCar = TestListOfCars[CounterOfRacers - 1];
                ++CounterOfRacers;
            }

            foreach (Racer CurrentRacer in TestListOfRacers)
            {
                Context.Racers.Add(CurrentRacer);
            }

            base.Seed(Context);
        }
    }
}
                       