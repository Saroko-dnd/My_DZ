using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Components
{
    public class Elf : AbstractPerson
    {
        public override int GetMaxSpeed()
        {
            return 30;
        }

        public override int GetMaxDamage()
        {
            return 15;
        }

        public override int GetMaxHealth()
        {
            return 100;
        }

        public override int GetMaxArmor()
        {
            return 0;
        }
    }
}
