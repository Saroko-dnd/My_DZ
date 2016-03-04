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
        public static Object RefreshProcessDataGate = new object();

        public static List<int> IDOfAllProcessesInDataGrid = new List<int>();
        public static Label SelectedProcessLabel;
        public static bool AlreadyRunning = false;
        public static Mutex BufForMainMutex = new Mutex();
        //CancellationTokenSource позволяет синхронно закрывать потоки связанные с его token
        public static CancellationTokenSource cts = new CancellationTokenSource();

        public static List<ProcessThread> AllThreads = new List<ProcessThread>();
        public static List<ProcessModule> AllModules = new List<ProcessModule>();

        public static bool MainProgramClosing = false;
        public static bool MainSelectedAnotherProcess = false;
        public static Thread ProcessesThread;
        public static Thread ShowAllModulesOfSelectedProcess;
        public static Process SelectedItem = null;
        public static DataGrid ProcessesNamesDataGrid = new DataGrid();
        public static DataGrid ThreadsDataGrid = new DataGrid();
        public static DataGrid DllsDataGrid = new DataGrid();

        public static void KillSelectedProcess(Object sender, EventArgs e)
        {
            lock (RefreshProcessDataGate)
            {
                if (SelectedItem != null)
                {
                    SelectedItem.Kill();
                    SelectedItem = null;
                }
                else
                {
                    MessageBox.Show(MyResourses.Texts.ProcessNotSelected,MyResourses.Texts.Error,MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }

        public static void ActivateThreads(Object sender,EventArgs e)
        {
            if (!AlreadyRunning)
            {
                AlreadyRunning = true;
                ThreadsDataGrid.ItemsSource = AllThreads;
                DllsDataGrid.ItemsSource = AllModules;
                if (ProcessesThread == null && ShowAllModulesOfSelectedProcess == null)
                {
                    ProcessesThread = new Thread(() => WorkWithProcesses(cts.Token));
                    ShowAllModulesOfSelectedProcess = new Thread(() => ShowModulesForSelectedProcess(cts.Token));
                    ProcessesThread.Start();
                    ShowAllModulesOfSelectedProcess.Start();
                }
            }
        }

        public static void SelectionOfAnotherProcess(Object sender, EventArgs e)
        {
            if ((sender as DataGrid).SelectedIndex >= 0)
            {
                SelectedItem = ProcessesNamesDataGrid.SelectedItem as Process;
                SelectedProcessLabel.Content = SelectedItem.ProcessName;
                if (!MainSelectedAnotherProcess)
                    MainSelectedAnotherProcess = true;
            }
        }

        public static void WorkWithProcesses(CancellationToken CurrentToken)
        {
            bool RefreshNecessary = true;
            try
            {
                while (true)
                {
                        if (CurrentToken.IsCancellationRequested)
                            break;

                        foreach (Process CurrentProcess in Process.GetProcesses())
                        {
                            if (!IDOfAllProcessesInDataGrid.Contains(CurrentProcess.Id))
                            {
                                RefreshNecessary = true;
                                break;
                            }
                        }
                        foreach (int CurrentID in IDOfAllProcessesInDataGrid)
                        {
                            if (!Process.GetProcesses().Select(res => res.Id).Contains(CurrentID))
                            {
                                RefreshNecessary = true;
                                break;
                            }
                        }
                        if (RefreshNecessary)
                        {
                            IDOfAllProcessesInDataGrid.Clear();
                            foreach (Process CurrentProcess in Process.GetProcesses())
                            {
                                IDOfAllProcessesInDataGrid.Add(CurrentProcess.Id);
                            }
                            Application.Current.Dispatcher.Invoke(
                                new System.Action(() => ProcessesNamesDataGrid.ItemsSource = Process.GetProcesses())
                                );
                            RefreshNecessary = false;
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
            try
            {
                while (true)
                {
                        if (CurrentToken.IsCancellationRequested)
                            break;
                        Application.Current.Dispatcher.Invoke(new System.Action(() => SelectionChanged = MainSelectedAnotherProcess));
                        AllThreads.Clear();
                        AllModules.Clear();
                        lock (RefreshProcessDataGate)
                        {
                            if (SelectedItem != null && (SelectionChanged || ProcessAlreadyLoaded))
                            {
                                try
                                {

                                    Application.Current.Dispatcher.Invoke(new System.Action(() => SelectedProcess =
                                        Process.GetProcessById(SelectedItem.Id)));
                                    ProcessAlreadyLoaded = true;
                                    foreach (ProcessThread ProcThr in SelectedProcess.Threads)
                                    {
                                        AllThreads.Add(ProcThr);
                                    }
                                    foreach (ProcessModule ProcMod in SelectedProcess.Modules)
                                    {
                                        AllModules.Add(ProcMod);
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
                            else
                            {
                                Application.Current.Dispatcher.Invoke(new System.Action(() => SelectedProcessLabel.Content = string.Empty));
                            }
                            Application.Current.Dispatcher.Invoke(new System.Action(() => DllsDataGrid.Items.Refresh()));
                            Application.Current.Dispatcher.Invoke(new System.Action(() => ThreadsDataGrid.Items.Refresh()));
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
