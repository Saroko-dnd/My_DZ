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
using System.ComponentModel;

namespace CopyFilesAsync
{
    static class MatrixMultiplication
    {
        public static TaskCounter CounterOfRemainingTasks;

        public static bool ThreadsWereCreated = false;
        public static bool PrintedAlready = false;

        public static bool AlreadyRunning = false;
        public static int NumberOfRowsInResultMatrix = 0;
        public static int NumberOfColumnsInResultMatrix = 0;
        //1 - Rows 2- Columns
        public static int[,] FirstMatrix = new int[3,3];
        public static int[,] SecondMatrix = new int[3,3];
        public static List<List<int>> ArrayForDataGrid = new List<List<int>>();
        public static int[,] ResultMatrix;
        public static TextBox ResultMatrixTextBox;

        public static ManualResetEvent CheckFinishMRE;

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
            ResultMatrixTextBox.Text = BufString.ToString();
            AlreadyRunning = false;
        }

        public static void RunMultiplicationThread(int AmountOfRows)
        {
            PrintedAlready = false;
            CheckFinishMRE = new ManualResetEvent(false);
            CounterOfRemainingTasks = new TaskCounter(AmountOfRows);
            Thread MultiplicationThread = new Thread(() => StartMultiplication(AmountOfRows));
            MultiplicationThread.Start();
        }

        public static void StartMultiplication(int AmountOfRows)
        {
            ResultMatrix = new int[NumberOfRowsInResultMatrix, NumberOfColumnsInResultMatrix];
            if (FirstMatrix.GetLength(1) == SecondMatrix.GetLength(0))
            {
                int Index = (FirstMatrix.GetLength(0) - 1);
                //создание потоков
                ThreadsWereCreated = true;
                ThreadPool.SetMaxThreads(20, 20);
                for (int Amount = 0; Amount < AmountOfRows; ++Amount)
                {
                    int BugFixInt = Amount;
                    ThreadPool.QueueUserWorkItem(o => CalculateЬMatrixRow(BugFixInt));
                }
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.MultiplicationImpossible,MyResourses.Texts.Error,
                    MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        public static void CalculateЬMatrixRow(int RowIndex)
        {
            int SecondMatrixRow = 0;
            int SecondMatrixColumnAmount = 0;
            int FirstMatrixColumnAmount = 0;
            SecondMatrixColumnAmount = (SecondMatrix.GetLength(1) - 1);
            FirstMatrixColumnAmount = (FirstMatrix.GetLength(1) - 1);
            for (int ColumnNumSecondMatrix = 0; ColumnNumSecondMatrix <= SecondMatrixColumnAmount; ++ColumnNumSecondMatrix)
            {
                SecondMatrixRow = 0;
                for (int ColumnNumFirstMatrix = 0; ColumnNumFirstMatrix <= FirstMatrixColumnAmount; ++ColumnNumFirstMatrix)
                {
                    ResultMatrix[RowIndex, ColumnNumSecondMatrix] +=
                        (FirstMatrix[RowIndex, ColumnNumFirstMatrix] * SecondMatrix[SecondMatrixRow, ColumnNumSecondMatrix]);
                    ++SecondMatrixRow;
                }
            }
            if (Interlocked.Decrement(ref CounterOfRemainingTasks.AmountOfTasks) == 0)
            {
                Application.Current.Dispatcher.Invoke(new System.Action(() => PrintResult()));
                CheckFinishMRE.Set();
            }
        }

    }
}
