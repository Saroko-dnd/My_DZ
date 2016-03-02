using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

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

        public static void PrintResult()
        {
            StringBuilder BufString = new StringBuilder();
            for (int RowNumber = 0; RowNumber < MatrixMultiplication.ResultMatrix.GetLength(0); ++RowNumber)
            {
                for (int ColumnNumber = 0; ColumnNumber < MatrixMultiplication.ResultMatrix.GetLength(1); ++ColumnNumber)
                {
                    BufString.Append(MatrixMultiplication.ResultMatrix[RowNumber, ColumnNumber]);
                    BufString.Append(" ");
                }
                BufString.AppendLine();
            }
            ResultMatrixLabel.Content = BufString.ToString();
        }

        public static void RunMultiplicationThread()
        {
            Thread MultiplicationThread = new Thread(StartMultiplication);
            MultiplicationThread.Start();
        }

        public static void StartMultiplication()
        {
            ResultMatrix = new int[NumberOfRowsInResultMatrix, NumberOfColumnsInResultMatrix];
            if (FirstMatrix.GetLength(1) == SecondMatrix.GetLength(0))
            {
                int Index = (FirstMatrix.GetLength(0) - 1);
                int AmountOfThreads = 0;
                while (Index >= 0)
                {
                    int BugFixInt = Index;
                    AllRelatedThreads.Add(new Thread(() => CalculateЬMatrixRow(BugFixInt)));
                    --Index;
                    ++AmountOfThreads;
                    //этот фрагмент кода лимитирует количество потоков
                    if (AmountOfThreads % 20 == 0 && AmountOfThreads > 0)
                    {
                        foreach (Thread CurrentThread in AllRelatedThreads)
                        {
                            CurrentThread.Start();
                        }
                        foreach (Thread CurrentThread in AllRelatedThreads)
                        {
                            CurrentThread.Join();
                            --AmountOfThreads;
                        }
                        AllRelatedThreads.Clear();
                    }
                }
                foreach (Thread CurrentThread in AllRelatedThreads)
                {
                    CurrentThread.Start();
                }
                foreach (Thread CurrentThread in AllRelatedThreads)
                {
                    CurrentThread.Join();
                    --AmountOfThreads;
                }
                Application.Current.Dispatcher.Invoke(
                    new System.Action(() => PrintResult())
                    );
            }
        }

        public static void CalculateЬMatrixRow(int RowNumber)
        {
            int SecondMatrixRow = 0;
            int SecondMatrixColumnAmount = 0;
            int FirstMatrixColumnAmount = 0;
            Application.Current.Dispatcher.Invoke(
                new System.Action(() => SecondMatrixColumnAmount = (SecondMatrix.GetLength(1) - 1))
                );
            Application.Current.Dispatcher.Invoke(
                new System.Action(() => FirstMatrixColumnAmount = (FirstMatrix.GetLength(1) - 1))
                );
            while (SecondMatrixColumnAmount == 0 || FirstMatrixColumnAmount == 0)
            {

            }
            for (int ColumnNumSecondMatrix = 0; ColumnNumSecondMatrix <= SecondMatrixColumnAmount; ++ColumnNumSecondMatrix)
            {
                SecondMatrixRow = 0;
                for (int ColumnNumFirstMatrix = 0; ColumnNumFirstMatrix <= FirstMatrixColumnAmount; ++ColumnNumFirstMatrix)
                {

                        Application.Current.Dispatcher.Invoke(
                            new System.Action(() => ResultMatrix[RowNumber, ColumnNumSecondMatrix] +=
                            (FirstMatrix[RowNumber, ColumnNumFirstMatrix] * SecondMatrix[SecondMatrixRow, ColumnNumSecondMatrix]))
                            );


                        ++SecondMatrixRow;
                }
            }
        }

    }
}
