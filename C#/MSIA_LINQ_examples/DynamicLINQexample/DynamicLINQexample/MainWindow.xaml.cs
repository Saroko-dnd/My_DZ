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

namespace DynamicLINQexample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<TestDataClass> AllTestDataList = new List<TestDataClass>() { new TestDataClass(DateTime.Now, 40, 34.9, 432.98) };

        public MainWindow()
        {
            InitializeComponent();

            TestsDataGrid.AutoGeneratingColumn += TestDataClass.DataGridAutoGeneratingColumn;
            TestsDataGrid.ItemsSource = AllTestDataList;

            DayTextBox.PreviewTextInput += CharsKiller.OnlyNumbersPreviewTextInput;
            MonthTextBox.PreviewTextInput += CharsKiller.OnlyNumbersPreviewTextInput;
            YearTextBox.PreviewTextInput += CharsKiller.OnlyNumbersPreviewTextInput;
            TemperatureTextBox.PreviewTextInput += CharsKiller.OnlyNumbersPreviewTextInput;
            StressTextBox.PreviewTextInput += CharsKiller.OnlyDoublePreviewTextInput;
        }
    }
}
