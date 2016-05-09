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
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;
using FromPythonAndRubyToExcel.PythonClasses;
using FromPythonAndRubyToExcel.ExcelClasses;

namespace FromPythonAndRubyToExcel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PythonWorker TestWorker = PythonWorker.GetPythonWorker();

            ExcelGraphCreator TestObject = ExcelGraphCreator.GetExcelGraphCreator();
            TestObject.SaveDataInExcel(TestWorker, "");
        }

    }
}
