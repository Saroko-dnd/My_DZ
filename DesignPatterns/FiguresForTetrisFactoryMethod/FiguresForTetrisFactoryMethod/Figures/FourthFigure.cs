﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresForTetrisFactoryMethod.Figures
{
    public class FourthFigure : Figure
    {
        public FourthFigure()
        {
            FigureItself = new int[4, 4];
            for (int RowsCounter = 0; RowsCounter < 4; ++RowsCounter)
            {
                for (int ColumnsCounter = 0; ColumnsCounter < 4; ++ColumnsCounter)
                {
                    if (ColumnsCounter == 0)
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
