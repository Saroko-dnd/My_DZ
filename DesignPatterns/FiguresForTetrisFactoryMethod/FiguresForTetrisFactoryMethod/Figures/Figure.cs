using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresForTetrisFactoryMethod
{
    public abstract class Figure : IFigure
    {
        public int[,] FigureItself;

        public override string ToString()
        {
            StringBuilder BuilderForFigure = new StringBuilder();
            for (int RowsCounter = 0; RowsCounter < 4; ++RowsCounter)
            {
                for (int ColumnsCounter = 0; ColumnsCounter < 4; ++ColumnsCounter)
                {
                    BuilderForFigure.Append(FigureItself[RowsCounter, ColumnsCounter].ToString());
                }
                BuilderForFigure.Append("\r\n");
            }
            return BuilderForFigure.ToString();
        }
    }

    public interface IFigure
    {

    }
}
