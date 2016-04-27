using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresForTetrisFactoryMethod.Figures
{
    public interface IFactoryOfFigures
    {
        Figure CreateFigure();
    }

    public class FactoryOfFirstFigures : IFactoryOfFigures
    {
        public Figure CreateFigure()
        {
            return new FirstFigure();
        }
    }

    public class FactoryOfSecondFigures : IFactoryOfFigures
    {
        public Figure CreateFigure()
        {
            return new SecondFigure();
        }
    }

    public class FactoryOfThirdFigures : IFactoryOfFigures
    {
        public Figure CreateFigure()
        {
            return new ThirdFigure();
        }
    }

    public class FactoryOfFouthFigures : IFactoryOfFigures
    {
        public Figure CreateFigure()
        {
            return new FourthFigure();
        }
    }

    public class FactoryOfFifthFigures : IFactoryOfFigures
    {
        public Figure CreateFigure()
        {
            return new FifthFigure();
        }
    }

    public class FactoryOfSixthFigures : IFactoryOfFigures
    {
        public Figure CreateFigure()
        {
            return new SixthFigure();
        }
    }
}
