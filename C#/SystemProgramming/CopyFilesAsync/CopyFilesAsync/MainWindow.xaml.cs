using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
//for open dialog
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CopyFilesAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool WeOwnenrsOfMutex;

        public static Mutex CheckIfAnotherAppRun; 
        public MainWindow()
        {
            string MutexName = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();
            CheckIfAnotherAppRun = new Mutex(false, MutexName);
            //CheckIfAnotherAppRun = new Mutex(true, MutexName, out WeOwnenrsOfMutex);
            //if (WeOwnenrsOfMutex)
            if (CheckIfAnotherAppRun.WaitOne(0,false))
            {
                InitializeComponent();


                this.Closing += AsyncProcessesThreadsModules.MainFormClosing;

                AsyncCopyFiles.CopyProgressBar = this.CopyProgressBar;
                AsyncCopyFiles.CopyStateLabel = this.CopyStateLabel;

                StartButton.Click += AsyncCopyFiles.StartButton_Click;
                FromButton.Click += AsyncCopyFiles.FromButton_Click;
                WhereButton.Click += AsyncCopyFiles.WhereButton_Click;
                PauseButton.Click += AsyncCopyFiles.PauseButton_Click;
                UnPauseButton.Click += AsyncCopyFiles.UnPauseButton_Click;
                BreakButton.Click += AsyncCopyFiles.BreakButtonClick;

                ProcessesNamesDataGrid.SelectionChanged += AsyncProcessesThreadsModules.SelectionOfAnotherProcess;
                AsyncProcessesThreadsModules.DllsDataGrid = this.DllsDataGrid;
                AsyncProcessesThreadsModules.ThreadsDataGrid = this.ThreadsDataGrid;
                AsyncProcessesThreadsModules.ProcessesNamesDataGrid = this.ProcessesNamesDataGrid;
                AsyncProcessesThreadsModules.ActivateThreads();
                AsyncProcessesThreadsModules.SuperDataGrid = TestDataGrid;
                AsyncProcessesThreadsModules.BufForMainMutex = CheckIfAnotherAppRun;
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.ProgramAlreadyRunning,MyResourses.Texts.Error, 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }

        }

        public void GenerateColumnEvent(Object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == MyResourses.Texts.ProcessName)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

    }
}
