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

    public abstract class FactoryOfFigures
    {
        protected static IFactoryOfFigures FactoryInstance;
        protected static object GateToFactory = new object();
    }

    public class FactoryOfFirstFigures : FactoryOfFigures, IFactoryOfFigures
    {
        private FactoryOfFirstFigures()
        {

        }

        public static IFactoryOfFigures GetFactory()
        {
            if (FactoryInstance == null)
            {
                lock (GateToFactory)
                {
                    FactoryInstance = new FactoryOfFirstFigures();
                    return FactoryInstance;
                }
            }
            else
            {
                return FactoryInstance;
            }
        }

        public Figure CreateFigure()
        {
            return new FirstFigure();
        }
    }

    public class FactoryOfSecondFigures : FactoryOfFigures, IFactoryOfFigures
    {
        private FactoryOfSecondFigures()
        {

        }

        public static IFactoryOfFigures GetFactory()
        {
            if (FactoryInstance == null)
            {
                lock (GateToFactory)
                {
                    FactoryInstance = new FactoryOfSecondFigures();
                    return FactoryInstance;
                }
            }
            else
            {
                return FactoryInstance;
            }
        }

        public Figure CreateFigure()
        {
            return new SecondFigure();
        }
    }

    public class FactoryOfThirdFigures : FactoryOfFigures, IFactoryOfFigures
    {
        private FactoryOfThirdFigures()
        {

        }

        public static IFactoryOfFigures GetFactory()
        {
            if (FactoryInstance == null)
            {
                lock (GateToFactory)
                {
                    FactoryInstance = new FactoryOfThirdFigures();
                    return FactoryInstance;
                }
            }
            else
            {
                return FactoryInstance;
            }
        }

        public Figure CreateFigure()
        {
            return new ThirdFigure();
        }
    }

    public class FactoryOfFouthFigures : FactoryOfFigures, IFactoryOfFigures
    {
        private FactoryOfFouthFigures()
        {

        }

        public static IFactoryOfFigures GetFactory()
        {
            if (FactoryInstance == null)
            {
                lock (GateToFactory)
                {
                    FactoryInstance = new FactoryOfFouthFigures();
                    return FactoryInstance;
                }
            }
            else
            {
                return FactoryInstance;
            }
        }

        public Figure CreateFigure()
        {
            return new FourthFigure();
        }
    }

    public class FactoryOfFifthFigures : FactoryOfFigures, IFactoryOfFigures
    {
        private FactoryOfFifthFigures()
        {

        }

        public static IFactoryOfFigures GetFactory()
        {
            if (FactoryInstance == null)
            {
                lock (GateToFactory)
                {
                    FactoryInstance = new FactoryOfFifthFigures();
                    return FactoryInstance;
                }
            }
            else
            {
                return FactoryInstance;
            }
        }

        public Figure CreateFigure()
        {
            return new FifthFigure();
        }
    }

    public class FactoryOfSixthFigures : FactoryOfFigures, IFactoryOfFigures
    {
        private FactoryOfSixthFigures()
        {

        }

        public static IFactoryOfFigures GetFactory()
        {
            if (FactoryInstance == null)
            {
                lock (GateToFactory)
                {
                    FactoryInstance = new FactoryOfSixthFigures();
                    return FactoryInstance;
                }
            }
            else
            {
                return FactoryInstance;
            }
        }

        public Figure CreateFigure()
        {
            return new SixthFigure();
        }
    }
}
