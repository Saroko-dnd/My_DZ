using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.DecoratorClasses
{
    public class Human : AbstractPerson
    {
        public override int GetSpeed()
        {
            return Speed;
        }

        public override int GetDamage()
        {
            return Damage;
        }

        public override int GetHealth()
        {
            return Health;
        }

        public override int GetArmor()
        {
            return Armor;
        }

        public Human()
        {
            Speed = 20;
            Damage = 20;
            Health = 150;
            Armor = 0;
        }
    }
}
