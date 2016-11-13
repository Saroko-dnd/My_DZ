using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceLogic
{
    public class RaceManager : IRaceManager
    {

        IRaceRepository CurrentRaceRepository;
        IAccessorToRaceInfo CurrentAccessorToRaceInfo;

        public Car Winner
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool NewRaceCanBeCreated
        {
            get
            { 

                
                throw new NotImplementedException();
            }
        }

        public IEnumerable<Car> GetAllCars()
        {
            return CurrentRaceRepository.AllCars;
        }

        public RaceManager(IRaceRepository NewRaceRepository, IAccessorToRaceInfo NewRaceState)
        {
            CurrentRaceRepository = NewRaceRepository;
            CurrentAccessorToRaceInfo = NewRaceState;
        }
    }
}
