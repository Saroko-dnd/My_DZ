using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows;

namespace CopyFilesAsync
{
    public static class SemaphoreTest
    {
        public static Window MainAppWindow;
        public static bool FilesWereCreated = false;
        public static bool ThreadsRunning = false;
        public static Semaphore SemaphoreForFileWriter = new Semaphore(5, 5);
        public static TaskCounter CounterOfRemainingTasks;

        public static void CreateFiles(Object sender, EventArgs e)
        {
            if (!ThreadsRunning && !FilesWereCreated)
            {
                ThreadsRunning = true;
                CounterOfRemainingTasks = new TaskCounter(100);
                ThreadPool.SetMaxThreads(20, 20);
                for (int counter = 0; counter <= 100; ++counter)
                {
                    int BugFixInt = counter;
                    ThreadPool.QueueUserWorkItem(o => SaveFile(BugFixInt));
                }
            }
        }

        public static void SaveFile(int NumberOfFile)
        {
            try
            {
                SemaphoreForFileWriter.WaitOne();
                StreamWriter TextWriter = File.CreateText(NumberOfFile.ToString() + "_" + MyResourses.Texts.FileName);
                TextWriter.Write(MyResourses.Texts.FileText);
                TextWriter.Close();
                SemaphoreForFileWriter.Release();
            }
            catch (Exception CurrentException)
            {
                if (MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.No)
                {
                    Application.Current.Dispatcher.Invoke(new System.Action(() => MainAppWindow.Close()));
                }
            }

            if (Interlocked.Decrement(ref CounterOfRemainingTasks.AmountOfTasks) == 0)
            {
                MessageBox.Show(MyResourses.Texts.FilesSucess,MyResourses.Texts.Ready, MessageBoxButton.OK);
                FilesWereCreated = true;
                ThreadsRunning = false;
            }
        }
    }
}
