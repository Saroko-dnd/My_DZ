using DecoratorPattern.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Decorators
{
    public class ElfGoodMage : ProfessionDecorator
    {
        private ElfMage CurrentPerson = new ElfMage();

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
            return CurrentPerson.GetMaxArmor() + 30;
        }

        public override int GetMaxDamage()
        {
            if (CurrentPerson == null)
            {
                return -1;
            }
            return CurrentPerson.GetMaxDamage() + 50;
        }

        public override int GetMaxHealth()
        {
            if (CurrentPerson == null)
            {
                return -1;
            }
            return CurrentPerson.GetMaxHealth() + 100;
        }

        public override int GetMaxSpeed()
        {
            if (CurrentPerson == null)
            {
                return -1;
            }
            return CurrentPerson.GetMaxSpeed() + 30;
        }
    }
}
