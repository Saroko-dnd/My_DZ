using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace CopyFilesAsync
{
    static class MatrixMultiplication
    {
        public static int NumberOfRowsInResultMatrix = 0;
        public static int NumberOfColumnsInResultMatrix = 0;
        //1 - Rows 2- Columns
        public static int[,] FirstMatrix = new int[3,3];
        public static int[,] SecondMatrix = new int[3,3];
        public static List<List<int>> ArrayForDataGrid = new List<List<int>>();
        public static int[,] ResultMatrix;
        public static List<Thread> AllRelatedThreads = new List<Thread>();
        public static Label ResultMatrixLabel;

        public static void StartMultiplication()
        {
            ResultMatrix = new int[NumberOfRowsInResultMatrix, NumberOfColumnsInResultMatrix];
            if (FirstMatrix.GetLength(1) == SecondMatrix.GetLength(0))
            {
                for (int counter = (FirstMatrix.GetLength(0) - 1); counter >= 0 ;--counter)
                {
                    Thread CalculateЬMatrixRowThread = new Thread(() => CalculateЬMatrixRow(counter));
                    CalculateЬMatrixRowThread.Start();
                }
                foreach (Thread CurrentThread in AllRelatedThreads)
                {
                    CurrentThread.Join();
                }
            }
        }
        public static void CalculateЬMatrixRow(int RowNumber)
        {
            List<Thread> AllRelatedThreads = new List<Thread>();
            int SecondMatrixRow = 0;
            for (int counter = 0; counter <= (SecondMatrix.GetLength(1) - 1); ++counter)
            {
                SecondMatrixRow = 0;
                for (int counter_2 = 0; counter_2 <= (FirstMatrix.GetLength(1) - 1); ++counter_2)
                {

                        Application.Current.Dispatcher.Invoke(
                            new System.Action(() => ResultMatrix[RowNumber, counter] += (FirstMatrix[RowNumber, counter_2] * SecondMatrix[SecondMatrixRow, counter]))
                            );
                        ++SecondMatrixRow;
                }                
            }
        }

    }
}
