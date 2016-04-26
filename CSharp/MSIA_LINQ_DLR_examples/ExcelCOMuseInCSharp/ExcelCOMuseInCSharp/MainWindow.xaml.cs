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
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelCOMuseInCSharp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Excel.Application excelApp = new Excel.Application();
            //Необходимо для отрисовки интерфейса
            excelApp.Visible = true;
            //excelApp.ActivateMicrosoftApp(Excel.XlMSApplication.xlMicrosoftWord);
            Excel.Workbook excelWB = excelApp.Workbooks.Add();
            Excel.Worksheet excelWS = excelWB.Worksheets.Add();
            Excel.Chart excelChart = excelWB.Charts.Add();
        }
    }
}
