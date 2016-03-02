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
        public static Random MainRandom = new Random();
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
                
                FirstMatrixRowsTextBox.PreviewTextInput += CharsKiller.InputValidation;
                FirstMatrixRowsTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
                FirstMatrixColumnsTextBox.PreviewTextInput += CharsKiller.InputValidation;
                FirstMatrixColumnsTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
                SecondMatrixRowsTextBox.PreviewTextInput += CharsKiller.InputValidation;
                SecondMatrixRowsTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
                SecondMatrixColumnsTextBox.PreviewTextInput += CharsKiller.InputValidation;
                SecondMatrixColumnsTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;

                MatrixMultiplication.ResultMatrixLabel = ResultMultiplicationMatrixLabel;
                StartProcessesThreadsButton.Click += AsyncProcessesThreadsModules.ActivateThreads;
                StartMatrixMultiplication.Click += CreateTwoMatrixAndStartMultiplication;

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

        public void CreateTwoMatrixAndStartMultiplication(Object sender,EventArgs e)
        {
            if (!MatrixMultiplication.AlreadyRunning )
            {
                MatrixMultiplication.AlreadyRunning = true;
                try
                {
                    if (Int32.Parse(SecondMatrixColumnsTextBox.Text) == 0 || Int32.Parse(FirstMatrixRowsTextBox.Text) == 0
                        || Int32.Parse(FirstMatrixColumnsTextBox.Text) == 0 || Int32.Parse(SecondMatrixRowsTextBox.Text) == 0)
                    {
                        MessageBox.Show(MyResourses.Texts.ZeroError, MyResourses.Texts.Error,
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MatrixMultiplication.NumberOfColumnsInResultMatrix = Int32.Parse(SecondMatrixColumnsTextBox.Text);
                        MatrixMultiplication.NumberOfRowsInResultMatrix = Int32.Parse(FirstMatrixRowsTextBox.Text);

                        MatrixMultiplication.FirstMatrix = new int[Int32.Parse(FirstMatrixRowsTextBox.Text), Int32.Parse(FirstMatrixColumnsTextBox.Text)];
                        MatrixMultiplication.SecondMatrix = new int[Int32.Parse(SecondMatrixRowsTextBox.Text), Int32.Parse(SecondMatrixColumnsTextBox.Text)];
                        StringBuilder BufString = new StringBuilder();

                        for (int RowNumber = 0; RowNumber < Int32.Parse(FirstMatrixRowsTextBox.Text); ++RowNumber)
                        {
                            for (int ColumnNumber = 0; ColumnNumber < Int32.Parse(FirstMatrixColumnsTextBox.Text); ++ColumnNumber)
                            {
                                MatrixMultiplication.FirstMatrix[RowNumber, ColumnNumber] = MainRandom.Next(10);
                                BufString.Append(" ");
                                BufString.Append(MatrixMultiplication.FirstMatrix[RowNumber, ColumnNumber]);
                            }
                            BufString.AppendLine();
                        }
                        FirstGeneratedMatrixLabel.Text = BufString.ToString();
                        BufString.Clear();
                        for (int RowNumber = 0; RowNumber < Int32.Parse(SecondMatrixRowsTextBox.Text); ++RowNumber)
                        {
                            for (int ColumnNumber = 0; ColumnNumber < Int32.Parse(SecondMatrixColumnsTextBox.Text); ++ColumnNumber)
                            {
                                MatrixMultiplication.SecondMatrix[RowNumber, ColumnNumber] = MainRandom.Next(10);
                                BufString.Append(" ");
                                BufString.Append(MatrixMultiplication.SecondMatrix[RowNumber, ColumnNumber]);
                            }
                            BufString.AppendLine();
                        }
                        SecondGeneratedMatrixLabel.Text = BufString.ToString();
                        BufString.Clear();

                        MatrixMultiplication.RunMultiplicationThread();
                    }
                }
                catch (FormatException CurrentException)
                {
                    MessageBox.Show(MyResourses.Texts.WrongFormatRowsColumnsMatrix, MyResourses.Texts.Error,
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (OverflowException CurrentException)
                {
                    MessageBox.Show(MyResourses.Texts.TooBigNumber, MyResourses.Texts.Error,
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
