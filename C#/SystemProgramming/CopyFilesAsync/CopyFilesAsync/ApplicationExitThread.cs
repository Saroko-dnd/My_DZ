using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace CopyFilesAsync
{

    public static class ApplicationExitThread
    {
        public static bool ProgramCanBeCanceled = false;
        public static bool AppTryingToClose = false;
        public static MainWindow MainAppWindow;

        public static void WaitForThreads ()
        {
            while (!AppTryingToClose)
            {

            }
            AsyncProcessesThreadsModules.cts.Cancel();
            if (AsyncProcessesThreadsModules.ProcessesThread != null && AsyncProcessesThreadsModules.ShowAllModulesOfSelectedProcess != null)
            {
                while (AsyncProcessesThreadsModules.ProcessesThread.ThreadState != System.Threading.ThreadState.Stopped || 
                    AsyncProcessesThreadsModules.ShowAllModulesOfSelectedProcess.ThreadState != System.Threading.ThreadState.Stopped)
                {

                }
            }

            if (MatrixMultiplication.ThreadsWereCreated)
            {
                MatrixMultiplication.CheckFinishMRE.WaitOne();
            }
            while (SemaphoreTest.ThreadsRunning)
            {

            }
            ProgramCanBeCanceled = true;
            Application.Current.Dispatcher.Invoke(new System.Action(() => MainAppWindow.Close()));
        }
    }
}
