using System;
using System.Net;
using System.IO;
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
using System.Threading;
using SalaryGraphicsBuilder.CodeOfExtractingData;
using SalaryGraphicsBuilder.DiagramCodeBehind;
using SalaryGraphicsBuilder.Resources;
using SalaryGraphicsBuilder.SerializationDeserializationXML;
using System.Collections.ObjectModel;
using SalaryGraphicsBuilder.EventsForMainWindowElements;

namespace SalaryGraphicsBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TextBoxForRangeValueOnChart.PreviewTextInput += MainWindowEventStorage.TextBoxTextInputOnlyNumbers;
            TextBoxForRangeValueOnChart.PreviewKeyDown += MainWindowEventStorage.TextBoxKeyPressDisableSpace;
            //Adding of handler for pasting in TextBox
            DataObject.AddPastingHandler(TextBoxForRangeValueOnChart, MainWindowEventStorage.TextBoxPasteOnlyNumbers);
            TextBoxForRangeValueOnChart.TextChanged += MainWindowEventStorage.TextBoxWithSalaryRangeTextChanged;
            TextBoxForRangeValueOnChart.Text = DiagramManipulator.DefaultRangeForSalaries.ToString();

            ColumnDiagramForSalary.DataContext = DiagramManipulator.ValueListForWpfChart;
            ComboBoxForProfessions.DataContext = DataReceiver.ListOfProfessionNames;
        }

        private void GetSalaryInfoButton_Click(object sender, RoutedEventArgs e)
        {
            string CurrentDirectoryPath = System.IO.Directory.GetCurrentDirectory();
            DataReceiver.PathToProfessionSalaryInfoFolder = CurrentDirectoryPath + "\\" + Texts.NameOfFolderForXMLfilesWithProfessionSalaryInfo;
            System.IO.Directory.CreateDirectory(DataReceiver.PathToProfessionSalaryInfoFolder);

            (sender as Button).Visibility = System.Windows.Visibility.Collapsed;
            ThreadPool.QueueUserWorkItem(a => DataReceiver.GetDataForSalaryGraphics());
        }

        private void ComboBoxForProfessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string SelectedProfessionName = (sender as ComboBox).SelectedItem.ToString();
            ThreadPool.QueueUserWorkItem(a => DiagramManipulator.CreateDataForDiagram(SelectedProfessionName));
        }
    }
}
