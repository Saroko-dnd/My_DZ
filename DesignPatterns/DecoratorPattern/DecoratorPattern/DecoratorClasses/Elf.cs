using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.DecoratorClasses
{
    public class Elf : AbstractPerson
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

        public Elf()
        {
            Speed = 30;
            Damage = 15;
            Health = 100;
            Armor = 0;
        }
    }
}
