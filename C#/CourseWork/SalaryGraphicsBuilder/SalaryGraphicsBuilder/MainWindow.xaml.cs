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

            ColumnDiagramForSalary.DataContext = DiagramManipulator.ValueListForWpfChart;
        }

        private void GetSalaryInfoButton_Click(object sender, RoutedEventArgs e)
        {
            //Test of creating directory*******
            string CurrentDirectoryPath = System.IO.Directory.GetCurrentDirectory();
            System.IO.Directory.CreateDirectory(CurrentDirectoryPath + "\\Professions");
            //*********
            (sender as Button).Visibility = System.Windows.Visibility.Collapsed;
            ThreadPool.QueueUserWorkItem(a => DataReceiver.GetDataForSalaryGraphics());
        }
    }
}
