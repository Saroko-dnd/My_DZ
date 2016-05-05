using DecoratorPattern.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Decorators
{
    public class HumanRider : ProfessionDecorator
    {
        private HumanSwordmaster CurrentPerson = new HumanSwordmaster();

        public override AbstractPerson GetCurrentPerson()
        {
            return CurrentPerson;
        }

        public override int GetMaxArmor()
        {
            if (CurrentPerson == null)
            {
                return -1;
            }
            return CurrentPerson.GetMaxArmor() + 100;
        }

        public override int GetMaxDamage()
        {
            if (CurrentPerson == null)
            {
                return -1;
            }
            return CurrentPerson.GetMaxDamage() - 10;
        }

        public override int GetMaxHealth()
        {
            if (CurrentPerson == null)
            {
                return -1;
            }
            return CurrentPerson.GetMaxHealth() + 200;
        }

        public override int GetMaxSpeed()
        {
            if (CurrentPerson == null)
            {
                return -1;
            }
            return CurrentPerson.GetMaxSpeed() + 40;
        }
    }
}
