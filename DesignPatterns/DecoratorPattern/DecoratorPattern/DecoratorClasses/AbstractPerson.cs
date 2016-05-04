using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.DecoratorClasses
{
    public abstract class AbstractPerson
    {
        protected int Speed;
        protected int Damage;
        protected int Health;
        protected int Armor;
        public abstract int GetSpeed();
        public abstract int GetDamage();
        public abstract int GetHealth();
        public abstract int GetArmor();
    }
}
