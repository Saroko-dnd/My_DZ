using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace CopyFilesAsync
{
    public static class SemaphoreTest
    {
        public static Semaphore SemaphoreForFileWriter = new Semaphore(5, 5);
        public static TaskCounter CounterOfRemainingTasks;
        public static ManualResetEvent CurrentMRE;

        public static void CreateFiles()
        {
            CurrentMRE = new ManualResetEvent(false);
            CounterOfRemainingTasks = new TaskCounter(100);
            ThreadPool.SetMaxThreads(20,20);
            for (int counter = 0; counter <= 100; ++counter)
            {
                int BugFixInt = counter;
                ThreadPool.QueueUserWorkItem(o => SaveFile(BugFixInt));
            }
        }

        public static void SaveFile(int NumberOfFile)
        {
            SemaphoreForFileWriter.WaitOne();
            StreamWriter TextWriter = File.CreateText(NumberOfFile.ToString() + "_" + MyResourses.Texts.FileName);
            TextWriter.Write(MyResourses.Texts.FileText);
            TextWriter.Close();
            SemaphoreForFileWriter.Release();
            if (Interlocked.Decrement(ref CounterOfRemainingTasks.AmountOfTasks) == 0)
            {
                CurrentMRE.Set();
            }
        }
    }
}
