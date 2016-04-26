using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresForTetrisFactoryMethod.Figures
{
    class FirstFigure : Figure
    {
        public override string PrintArray()
        {
            StringBuilder BuilderForFigure = new StringBuilder();
            for (int ColumnsCounter = 0; ColumnsCounter < 4; ++ColumnsCounter)
            {
                for (int RowsCounter = 0; RowsCounter < 4; ++RowsCounter)
                {
                    BuilderForFigure.Append(FigureItself[ColumnsCounter, RowsCounter].ToString());
                }
                BuilderForFigure.Append("\r\n");
            }
            return BuilderForFigure.ToString();
        }
        public FirstFigure()
        {
            FigureItself = new int[4, 4];
            for (int RowsCounter = 0; RowsCounter < 4; ++RowsCounter)
            {
                for (int ColumnsCounter = 0; ColumnsCounter < 4; ++ColumnsCounter)
                {
                    if (RowsCounter == 0 && ColumnsCounter == 1)
                    {
                        FigureItself[ColumnsCounter, RowsCounter] = 1;
                    }
                    else if (RowsCounter == 1 && ColumnsCounter < 3)
                    {
                        FigureItself[ColumnsCounter, RowsCounter] = 1;
                    }
                    else 
                    {
                        FigureItself[ColumnsCounter, RowsCounter] = 0;
                    }
                }
            }
        }
    }
}
