using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Components
{
    public abstract class AbstractPerson
    {
        public abstract int GetMaxSpeed();
        public abstract int GetMaxDamage();
        public abstract int GetMaxHealth();
        public abstract int GetMaxArmor();
    }
}
