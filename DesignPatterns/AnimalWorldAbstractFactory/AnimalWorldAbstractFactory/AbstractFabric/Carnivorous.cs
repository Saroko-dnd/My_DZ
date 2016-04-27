using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalWorldAbstractFactory.AbstractFabric
{
    public class Carnivorous
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        int strength;

        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }
        public void EatHerbivorous(Herbivorous CurrentHerbivorous)
        {
            if (CurrentHerbivorous.Weight > strength)
            {
                strength -= 10;
            }
            else if (CurrentHerbivorous.Weight < strength)
            {
                strength += 10;
            }
        }

        public Carnivorous(string NewName)
        {
            Name = NewName;
            Strength = Program.MainRandom.Next(1, 101);
        }
    }
}
