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

namespace CopyFilesAsync
{
    static class AsyncProcessesThreadsModules
    {
        //CancellationTokenSource позволяет синхронно закрывать потоки связанные с его token
        public static CancellationTokenSource cts = new CancellationTokenSource();

        public static bool MainProgramClosing = false;
        public static bool MainSelectedAnotherProcess = false;
        public static bool MainProcessesPause = false;
        public static Thread ProcessesThread;
        public static Thread ShowAllModulesOfSelectedProcess;
        public static Process SelectedItem = new Process();
        public static DataGrid ProcessesNamesDataGrid = new DataGrid();
        public static DataGrid ThreadsDataGrid = new DataGrid();
        public static DataGrid DllsDataGrid = new DataGrid();

        public static void ActivateThreads()
        {
            ProcessesThread = new Thread(() => WorkWithProcesses(cts.Token));
            //ProcessesThread.IsBackground = true;
            ShowAllModulesOfSelectedProcess = new Thread(() => ShowModulesForSelectedProcess(cts.Token));
            //ShowAllModulesOfSelectedProcess.IsBackground = true;
            ProcessesThread.Start();
            ShowAllModulesOfSelectedProcess.Start();
        }

        public static void MainFormClosing (Object sender,EventArgs e)
        {
            //говорим источнику закрыть все связанные потоки (tokens)
            cts.Cancel();

            while (ProcessesThread.ThreadState != System.Threading.ThreadState.Stopped || ShowAllModulesOfSelectedProcess.ThreadState != System.Threading.ThreadState.Stopped)
            {

            }
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
            bool Pause = false;
            int SelectedIndex = -1;
            try
            {
                while (true)
                {
                    if (CurrentToken.IsCancellationRequested)
                        break;
                    Application.Current.Dispatcher.Invoke(
                        new System.Action(() => SelectedIndex = ProcessesNamesDataGrid.SelectedIndex)
                        );
                    if (SelectedIndex >= 0)
                    {
                        Pause = true;
                        Application.Current.Dispatcher.Invoke(
                            new System.Action(() => MainProcessesPause = true)
                            );
                    }
                    while (Pause)
                    {
                        Application.Current.Dispatcher.Invoke(
                            new System.Action(() => Pause = MainProcessesPause)
                            );
                        if (CurrentToken.IsCancellationRequested)
                            break;
                    }
                    Application.Current.Dispatcher.Invoke(
                        new System.Action(() => ProcessesNamesDataGrid.ItemsSource = Process.GetProcesses())
                        );
                    Thread.Sleep(3000);
                }
            }
            catch (TaskCanceledException TaskFinishedException)
            {
                //просто завершаем поток, так как завершен процесс
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
            Process SelectedProcess = new Process();
            try
            {
                while (true)
                {
                    if (CurrentToken.IsCancellationRequested)
                        break;
                    Application.Current.Dispatcher.Invoke(new System.Action(() => SelectionChanged = MainSelectedAnotherProcess));
                    if (SelectionChanged)
                    {
                        try
                        {
                            Application.Current.Dispatcher.Invoke(new System.Action(() => SelectedProcess =
                                Process.GetProcessById(SelectedItem.Id)));
                            Application.Current.Dispatcher.Invoke(new System.Action(() => ThreadsDataGrid.ItemsSource =
                                SelectedProcess.Threads));
                            Application.Current.Dispatcher.Invoke(new System.Action(() => DllsDataGrid.ItemsSource =
                                SelectedProcess.Modules));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            SelectionChanged = false;
                            Application.Current.Dispatcher.Invoke(new System.Action(() => MainProcessesPause = false));
                        }
                    }
                    Thread.Sleep(3000);
                }
            }
            catch (TaskCanceledException TaskFinishedException)
            {
                //просто завершаем поток, так как завершен процесс
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message + " " + MyResourses.Texts.ThreadProcessesDied,
                    MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show(CurrentException.Message + " " + MyResourses.Texts.ThreadProcessesDied,
                    MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
