using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Components
{
    public class Human : AbstractPerson
    {

        public override int GetMaxSpeed()
        {
            return 20;
        }

        public override int GetMaxDamage()
        {
            return 20;
        }

        public override int GetMaxHealth()
        {
            return 150;
        }

        public override int GetMaxArmor()
        {
            return 0;
        }
    }
}
