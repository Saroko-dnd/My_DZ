using DecoratorPattern.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Decorators
{
    public abstract class ProfessionDecorator : AbstractPerson
    {
        public abstract AbstractPerson GetCurrentPerson();

        public override int GetMaxArmor()
        {
            throw new NotImplementedException();
        }

        public override int GetMaxDamage()
        {
            throw new NotImplementedException();
        }

        public override int GetMaxHealth()
        {
            throw new NotImplementedException();
        }

        public override int GetMaxSpeed()
        {
            throw new NotImplementedException();
        }
    }
}
