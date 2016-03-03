using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;

namespace CopyFilesAsync
{
    public class Triangle
    {
        public static bool ProgramAlreadyBusy = false;
        public static ManualResetEvent CurrentMRE = new ManualResetEvent(false);
        public static TaskCounter CounterOfRemainingTasks;
        public static StringBuilder ResultsForPrint = new StringBuilder();
        public static void calculateAreasInManyThreads()
        {
            ThreadPool.SetMaxThreads(20, 20);
            CurrentMRE = new ManualResetEvent(false);
            CounterOfRemainingTasks = new TaskCounter(40);
            for (int counter = 0; counter < 40; ++counter)
            {
                Triangle BufForTriangle = new Triangle(MainWindow.MainRandom.Next(50), MainWindow.MainRandom.Next(50), counter);
                ThreadPool.QueueUserWorkItem(o => BufForTriangle.CalculateTriangleArea());
            }
            CurrentMRE.WaitOne();
        }

        public int TriangleID;
        public int Height;
        public int Width;

        public void CalculateTriangleArea()
        {
            int Area = (Height * Width) / 2;
            lock (ResultsForPrint)
            {
                ResultsForPrint.Append(TriangleID.ToString() + " " + Area.ToString());
                ResultsForPrint.AppendLine();
            }            
            if (Interlocked.Decrement(ref CounterOfRemainingTasks.AmountOfTasks) == 0)
            {
                CurrentMRE.Set();
            }
        }

        public Triangle(int NewHeight,int NewWidth,int NewID)
        {
            Height = NewHeight;
            Width = NewWidth;
            TriangleID = NewID;
        }
    }
}
