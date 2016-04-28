using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalWorldAbstractFactory.AbstractFabric;

namespace AnimalWorldAbstractFactory
{
    public class WorldOfAnimals
    {
        List<Carnivorous> AllCarnivorous = new List<Carnivorous>();
        List<Herbivorous> AllHerbivorous = new List<Herbivorous>();
        IContinent AnimalsFactory;

        public void FeedingHerbivores()
        {
            AllHerbivorous = AllHerbivorous.Where(res => res.Alive).ToList();
            foreach (Herbivorous CurrentHerbivorous in AllHerbivorous)
            {
                CurrentHerbivorous.EatGrass();
            }
        }

        public void FeedingCarnivorous()
        {
            foreach (Carnivorous CurrentCarnivorous in AllCarnivorous)
            {
                CurrentCarnivorous.EatHerbivorous(AllHerbivorous[Program.MainRandom.Next() % AllHerbivorous.Count]);
            }
            AllCarnivorous = AllCarnivorous.Where(res => res.Strength > 0).ToList();
        }

        public void Simulation(int AmountOfRounds)
        {
            for (int Counter = 0; Counter < AmountOfRounds; ++Counter)
            {
                FeedingHerbivores();
                FeedingCarnivorous();
            }

            foreach (Carnivorous CurrentCarnivorous in AllCarnivorous)
            {
                CurrentCarnivorous.PrintInfo();
            }
            foreach (Herbivorous CurrentHerbivorous in AllHerbivorous)
            {
                CurrentHerbivorous.PrintInfo();
            }

        }

        public WorldOfAnimals(IContinent CurrentContinent, int AmountOfCarnivorous, int AmountOfHerbivorous)
        {
            AnimalsFactory = CurrentContinent;
            int RandomValue;

            for (int Counter = 0; Counter < AmountOfCarnivorous; ++Counter)
            {
                RandomValue = Program.MainRandom.Next(1, 3);
                if (RandomValue == 1)
                {
                    AllCarnivorous.Add(AnimalsFactory.GetСarnivorous());
                }
                else
                {
                    AllCarnivorous.Add(AnimalsFactory.GetСarnivorous());
                }
            }

            for (int Counter = 0; Counter < AmountOfHerbivorous; ++Counter)
            {
                RandomValue = Program.MainRandom.Next(1, 3);
                if (RandomValue == 1)
                {
                    AllHerbivorous.Add(AnimalsFactory.GetHerbivorous());
                }
                else
                {
                    AllHerbivorous.Add(AnimalsFactory.GetHerbivorous());
                }
            }
        }
    }
}
