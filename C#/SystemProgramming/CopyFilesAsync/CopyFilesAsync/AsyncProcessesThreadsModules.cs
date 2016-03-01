using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Linq;
using System.Collections.ObjectModel;

namespace CopyFilesAsync
{
    static class AsyncProcessesThreadsModules
    {
        public static Mutex BufForMainMutex = new Mutex();
        //CancellationTokenSource позволяет синхронно закрывать потоки связанные с его token
        public static CancellationTokenSource cts = new CancellationTokenSource();
        //мои объекты критических секций
        public static object MainLock_1 = new object();
        public static object MainLock_2 = new object();

        public static ObservableCollection<ProcessThread> AllThreads = new ObservableCollection<ProcessThread>();
        public static ObservableCollection<ProcessModule> AllModules = new ObservableCollection<ProcessModule>();

        public static bool MainProgramClosing = false;
        public static bool MainSelectedAnotherProcess = false;
        public static Thread ProcessesThread;
        public static Thread ShowAllModulesOfSelectedProcess;
        public static Process SelectedItem = new Process();
        public static DataGrid ProcessesNamesDataGrid = new DataGrid();
        public static DataGrid ThreadsDataGrid = new DataGrid();
        public static DataGrid DllsDataGrid = new DataGrid();

        public static Xceed.Wpf.DataGrid.DataGridControl SuperDataGrid = new Xceed.Wpf.DataGrid.DataGridControl();

        public static void ActivateThreads(Object sender,EventArgs e)
        {
            if (ProcessesThread == null && ShowAllModulesOfSelectedProcess == null)
            {
                ProcessesThread = new Thread(() => WorkWithProcesses(cts.Token));
                //ProcessesThread.IsBackground = true;
                ShowAllModulesOfSelectedProcess = new Thread(() => ShowModulesForSelectedProcess(cts.Token));
                //ShowAllModulesOfSelectedProcess.IsBackground = true;
                ProcessesThread.Start();
                ShowAllModulesOfSelectedProcess.Start();
            }
        }

        public static void MainFormClosing (Object sender,EventArgs e)
        {
            //говорим источнику закрыть все связанные потоки (tokens)
            lock (MainLock_1)
            {
                lock (MainLock_2)
                {
                    cts.Cancel();
                }
            }
            if (ProcessesThread != null && ShowAllModulesOfSelectedProcess != null)
            {
                while (ProcessesThread.ThreadState != System.Threading.ThreadState.Stopped || ShowAllModulesOfSelectedProcess.ThreadState != System.Threading.ThreadState.Stopped)
                {
                    int i = 0;
                }
            }
            BufForMainMutex.ReleaseMutex();
        }

        public static void SelectionOfAnotherProcess(Object sender, EventArgs e)
        {
            if ((sender as DataGrid).SelectedIndex >= 0)
            {
                SelectedItem = ProcessesNamesDataGrid.SelectedItem as Process;
                if (!MainSelectedAnotherProcess)
                    MainSelectedAnotherProcess = true;
            }
        }

        public static void WorkWithProcesses(CancellationToken CurrentToken)
        {
            try
            {
                while (true)
                {
                    lock (MainLock_1)
                    {
                        if (CurrentToken.IsCancellationRequested)
                            break;
                        Application.Current.Dispatcher.Invoke(
                            new System.Action(() => ProcessesNamesDataGrid.ItemsSource = Process.GetProcesses())
                            );
                    }
                    int CounterOfMilliseconds = 0;
                    while (!CurrentToken.IsCancellationRequested && CounterOfMilliseconds < 3000)
                    {
                        CounterOfMilliseconds += 100;
                        Thread.Sleep(100);
                    }
                    if (CurrentToken.IsCancellationRequested)
                        break;
                }
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message + " " + MyResourses.Texts.ThreadProcessesDied,
                    MyResourses.Texts.Error,MessageBoxButton.OK,MessageBoxImage.Error);
                MessageBox.Show(CurrentException.Message + " " + MyResourses.Texts.ThreadProcessesDied,
                    MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void ShowModulesForSelectedProcess(CancellationToken CurrentToken)
        {
            bool SelectionChanged = false;
            bool ProcessAlreadyLoaded = false;
            Process SelectedProcess = new Process();
            int pos = 0;
            Application.Current.Dispatcher.Invoke(new System.Action(() => ThreadsDataGrid.DataContext =
                AllThreads));
            Application.Current.Dispatcher.Invoke(new System.Action(() => DllsDataGrid.DataContext =
                AllModules));
            try
            {
                while (true)
                {
                    lock (MainLock_2)
                    {
                        if (CurrentToken.IsCancellationRequested)
                            break;
                        Application.Current.Dispatcher.Invoke(new System.Action(() => SelectionChanged = MainSelectedAnotherProcess));
                        //Application.Current.Dispatcher.Invoke(new System.Action(() => SelectedProcess = SelectedItem));
                        if (SelectionChanged || ProcessAlreadyLoaded)
                        {
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new System.Action(() => SelectedProcess =
                                    Process.GetProcessById(SelectedItem.Id)));
                                ProcessAlreadyLoaded = true;
                                //Application.Current.Dispatcher.Invoke(new System.Action(() => SuperDataGrid.ItemsSource =
                                    //SelectedProcess.Threads));
                                Application.Current.Dispatcher.Invoke(new System.Action(() => AllThreads.Clear()));
                                Application.Current.Dispatcher.Invoke(new System.Action(() => AllModules.Clear()));
                                foreach (ProcessThread ProcThr in SelectedProcess.Threads)
                                {
                                    Application.Current.Dispatcher.Invoke(new System.Action(() => AllThreads.Add(ProcThr)));
                                }
                                foreach (ProcessModule ProcMod in SelectedProcess.Modules)
                                {
                                    Application.Current.Dispatcher.Invoke(new System.Action(() => AllModules.Add(ProcMod)));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                SelectionChanged = false;
                                Application.Current.Dispatcher.Invoke(new System.Action(() => MainSelectedAnotherProcess = false));
                            }
                        }
                    }
                    int CounterOfMilliseconds = 0;
                    while (!CurrentToken.IsCancellationRequested && CounterOfMilliseconds < 3000)
                    {
                        CounterOfMilliseconds += 100;
                        Thread.Sleep(100);
                    }
                    if (CurrentToken.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message + " " + MyResourses.Texts.ThreadProcessesDied,
                    MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
