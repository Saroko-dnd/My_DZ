using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;
using FromPythonAndRubyToExcel.ExcelClasses;
using FromPythonAndRubyToExcel.ClassesForScripts.PythonClasses;
using FromPythonAndRubyToExcel.ClassesForScripts.RubyClasses;
using System.IO;
using FromPythonAndRubyToExcel.ClassesForScripts;
using System.Threading;
using Microsoft.Win32;
using Gat.Controls;

namespace FromPythonAndRubyToExcel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ExcelGraphCreator MainGraphCreator = ExcelGraphCreator.GetExcelGraphCreator();
        private static bool ProgramBusy = false;

        public MainWindow()
        {
            InitializeComponent();

            /*PythonWorker TestPythonWorker = PythonWorker.GetPythonWorker();
            RubyWorker TestRubyWorker = RubyWorker.GetRubyWorker();

            ExcelGraphCreator TestObject = ExcelGraphCreator.GetExcelGraphCreator();
            TestObject.SaveDataInExcel(TestPythonWorker, Directory.GetCurrentDirectory() + @"\PythonGraph.xls");
            TestObject.SaveDataInExcel(TestRubyWorker, Directory.GetCurrentDirectory() + @"\RubyGraph.xls");*/
        }

        private void CreateGraph(IScriptWorker CurrentWorker, string ScriptFullName, string CurrentSaveFullName, string PathToLibraries)
        {
            try
            {
                CurrentWorker.LoadScript(ScriptFullName, PathToLibraries);
                MainGraphCreator.SaveDataInExcel(CurrentWorker, CurrentSaveFullName);
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message, MyResources.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ProgramBusy = false;
            }
        }

        private void SavePythonDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ProgramBusy)
            {
                ProgramBusy = true;
                PythonWorker CurrentPythonWorker = PythonWorker.GetPythonWorker();
                string PathToPythonLibraries =  PathToPythonLibraryTextBox.Text;
                string ScriptFullName = PythonScriptFullNameTextBox.Text;
                string SaveFileName = SavePathForPythonTextBox.Text;
                ThreadPool.QueueUserWorkItem(o => CreateGraph(CurrentPythonWorker, ScriptFullName, SaveFileName, PathToPythonLibraries));
            }
        }

        private void SaveRubyDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ProgramBusy)
            {
                ProgramBusy = true;
                RubyWorker CurrentRubyWorker = RubyWorker.GetRubyWorker();
                string ScriptFullName = RubyScriptFullNameTextBox.Text;
                string SaveFileName = SavePathForRubyTextBox.Text;
                ThreadPool.QueueUserWorkItem(o => CreateGraph(CurrentRubyWorker, ScriptFullName, SaveFileName, null));
            }
        }

        private void SelectPythonScriptButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog CurrentOpenFileDialog = new OpenFileDialog();
            CurrentOpenFileDialog.ShowDialog();
            PythonScriptFullNameTextBox.Text = CurrentOpenFileDialog.FileName;
        }

        private void SelectFolderForPythonButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog CurrentSaveFileDialog = new SaveFileDialog();
            CurrentSaveFileDialog.ShowDialog();
            if (!CurrentSaveFileDialog.FileName.EndsWith(MyResources.Texts.ExtensionOfExcelFiles))
            {
                SavePathForPythonTextBox.Text = CurrentSaveFileDialog.FileName + MyResources.Texts.ExtensionOfExcelFiles;
            }
            else
            {
                SavePathForPythonTextBox.Text = CurrentSaveFileDialog.FileName;
            }
        }

        private void SelectRubyScriptButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog CurrentOpenFileDialog = new OpenFileDialog();
            CurrentOpenFileDialog.ShowDialog();
            RubyScriptFullNameTextBox.Text = CurrentOpenFileDialog.FileName;
        }

        private void SelectFolderForRubyButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog CurrentSaveFileDialog = new SaveFileDialog();
            CurrentSaveFileDialog.ShowDialog();
            if (!CurrentSaveFileDialog.FileName.EndsWith(MyResources.Texts.ExtensionOfExcelFiles))
            {
                SavePathForRubyTextBox.Text = CurrentSaveFileDialog.FileName + MyResources.Texts.ExtensionOfExcelFiles;
            }
            else
            {
                SavePathForRubyTextBox.Text = CurrentSaveFileDialog.FileName;
            }
        }

        private void FindPythonLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDialogView TestObject = new OpenDialogView();
            OpenDialogViewModel TestDialogModel = (OpenDialogViewModel)TestObject.DataContext;
            TestDialogModel.IsDirectoryChooser = true;
            TestDialogModel.FileNameText = MyResources.Texts.FolderName;
            TestDialogModel.Caption = MyResources.Texts.SpecifyPythonLibraryFolderCaption;
            TestDialogModel.Show();
            PathToPythonLibraryTextBox.Text = TestDialogModel.SelectedFilePath;
        }
    }
}
