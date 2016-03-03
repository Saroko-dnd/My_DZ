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
        public static Object NumbersOfRowsGate = new object();
        public static Object PrintResultGate = new object();

        public static bool ThreadsWereCreated = false;
        public static bool ProgramClosing = false;
        public static bool PrintedAlready = false;
        public static bool TaskFinished = false;

        public static List<int> NumbersOfRows = new List<int>();
        public static bool AlreadyRunning = false;
        public static int NumberOfRowsInResultMatrix = 0;
        public static int NumberOfColumnsInResultMatrix = 0;
        //1 - Rows 2- Columns
        public static int[,] FirstMatrix = new int[3,3];
        public static int[,] SecondMatrix = new int[3,3];
        public static List<List<int>> ArrayForDataGrid = new List<List<int>>();
        public static int[,] ResultMatrix;
        public static List<Thread> AllRelatedThreads = new List<Thread>();
        public static TextBox ResultMatrixLabel;

        public static ManualResetEvent[] doneEvents = new ManualResetEvent[20];

        public static void UIClosing(Object sender,EventArgs e)
        {
            lock (PrintResultGate)
            {
                ProgramClosing = true;
            }
            //В WPF нельзя пользоваться WaitHandle.WaitAll() поскольку UI поток является STA
            if (ThreadsWereCreated)
            {
                foreach (ManualResetEvent CurrentMRE in doneEvents)
                {
                    CurrentMRE.WaitOne();
                }
            }
        }

        //WaitHandle класс для ожидания срабатывания событий.
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
            ResultMatrixLabel.Text = BufString.ToString();
            AlreadyRunning = false;
        }

        public static void RunMultiplicationThread()
        {
            TaskFinished = false;
            PrintedAlready = false;
            Thread MultiplicationThread = new Thread(StartMultiplication);
            MultiplicationThread.Start();
        }

        public static void StartMultiplication()
        {
            ResultMatrix = new int[NumberOfRowsInResultMatrix, NumberOfColumnsInResultMatrix];
            if (FirstMatrix.GetLength(1) == SecondMatrix.GetLength(0))
            {
                int Index = (FirstMatrix.GetLength(0) - 1);
                //создание потоков
                for (int counter = 0; counter < 20; ++counter)
                {
                    doneEvents[counter] = new ManualResetEvent(false);
                }
                ThreadsWereCreated = true;
                ThreadPool.SetMaxThreads(20, 20);
                for (int Amount = 0; Amount < 20; ++Amount)
                {
                    int BugFixInt = Amount;
                    ThreadPool.QueueUserWorkItem(o => CalculateЬMatrixRow(doneEvents[BugFixInt]));
                }
                foreach (Thread CurrentThread in AllRelatedThreads)
                {
                    CurrentThread.Start();
                }
                //заполнение очереди задач
                while (Index >= 0)
                {
                    NumbersOfRows.Add(Index);
                    --Index;
                }
                //говорим, что задачи кончились
                TaskFinished = true;
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.MultiplicationImpossible,MyResourses.Texts.Error,
                    MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        public static void CalculateЬMatrixRow(ManualResetEvent CurrentMRE)
        {
            int CurrentRowNumber = -1;
            int SecondMatrixRow = 0;
            int SecondMatrixColumnAmount = 0;
            int FirstMatrixColumnAmount = 0;
            SecondMatrixColumnAmount = (SecondMatrix.GetLength(1) - 1);
            FirstMatrixColumnAmount = (FirstMatrix.GetLength(1) - 1);
            while (true)
            {
                while (CurrentRowNumber < 0)
                {
                    lock (NumbersOfRowsGate)
                    {
                        if (NumbersOfRows.Count() == 0 && TaskFinished)
                        {
                            lock (PrintResultGate)
                            {
                                if (!PrintedAlready && !ProgramClosing)
                                {
                                    Application.Current.Dispatcher.Invoke(
                                        new System.Action(() => PrintResult())
                                        );
                                    PrintedAlready = true;
                                }
                            }
                            CurrentMRE.Set();
                            return;
                        }
                        else if (NumbersOfRows.Count() > 0)
                        {
                            CurrentRowNumber = NumbersOfRows.First();
                            NumbersOfRows.Remove(CurrentRowNumber);
                        }
                    }
                }
                for (int ColumnNumSecondMatrix = 0; ColumnNumSecondMatrix <= SecondMatrixColumnAmount; ++ColumnNumSecondMatrix)
                {
                    SecondMatrixRow = 0;
                    for (int ColumnNumFirstMatrix = 0; ColumnNumFirstMatrix <= FirstMatrixColumnAmount; ++ColumnNumFirstMatrix)
                    {
                        ResultMatrix[CurrentRowNumber, ColumnNumSecondMatrix] +=
                            (FirstMatrix[CurrentRowNumber, ColumnNumFirstMatrix] * SecondMatrix[SecondMatrixRow, ColumnNumSecondMatrix]);
                        ++SecondMatrixRow;
                    }
                 }
                 CurrentRowNumber = -1;
            }
        }

    }
}
