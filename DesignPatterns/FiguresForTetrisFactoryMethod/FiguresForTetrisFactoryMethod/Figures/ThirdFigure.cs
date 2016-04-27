using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresForTetrisFactoryMethod.Figures
{
    public class ThirdFigure : Figure
    {
        public ThirdFigure()
        {
            FigureItself = new int[4, 4];
            for (int RowsCounter = 0; RowsCounter < 4; ++RowsCounter)
            {
                for (int ColumnsCounter = 0; ColumnsCounter < 4; ++ColumnsCounter)
                {
                    if (RowsCounter == 0 && ColumnsCounter == 0)
                    {
                        FigureItself[RowsCounter, ColumnsCounter] = 1;
                    }
                    else if (RowsCounter == 1 && ColumnsCounter < 3)
                    {
                        FigureItself[RowsCounter, ColumnsCounter] = 1;
                    }
                    else
                    {
                        FigureItself[RowsCounter, ColumnsCounter] = 0;
                    }
                }
            }
        }
    }
}
