using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalWorldAbstractFactory.AbstractFabric
{
    public class Herbivorous
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int weight;

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        private bool alive;

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }
        public void EatGrass()
        {
            Weight = weight + 10;
        }

        public Herbivorous(string NewName)
        {
            Name = NewName;
            Alive = true;
            weight = Program.MainRandom.Next(1,101);
        }
    }
}
