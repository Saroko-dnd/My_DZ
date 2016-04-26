using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresForTetrisFactoryMethod
{
    public abstract class Figure
    {
        public int[,] FigureItself;
        public abstract string PrintArray();
    }
}
