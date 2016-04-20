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
        public static bool ProgramBusy = false;
        public static List<TestDataClass> AllTestDataList = TestDataClass.GenerateRandomListOfData(30);
        public static List<DataFilter> AllFilters = new List<DataFilter>();
        public static List<string> ListOfIntTextBoxNames = new List<string>();
        public static List<string> ListOfDoubleTextBoxNames = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            TestsDataGrid.AutoGeneratingColumn += TestDataClass.DataGridAutoGeneratingColumn;
            TestsDataGrid.ItemsSource = AllTestDataList;

            DayTextBox.PreviewTextInput += CharsKiller.OnlyNumbersPreviewTextInput;
            MonthTextBox.PreviewTextInput += CharsKiller.OnlyNumbersPreviewTextInput;
            YearTextBox.PreviewTextInput += CharsKiller.OnlyNumbersPreviewTextInput;
            TemperatureTextBox.PreviewTextInput += CharsKiller.OnlyNumbersSignedPreviewTextInput;
            StressTextBox.PreviewTextInput += CharsKiller.OnlyDoublePreviewTextInput;
            DeflactionTextBox.PreviewTextInput += CharsKiller.OnlyDoublePreviewTextInput;

            ListOfIntTextBoxNames.Add(DayTextBox.Name);
            ListOfIntTextBoxNames.Add(MonthTextBox.Name);
            ListOfIntTextBoxNames.Add(YearTextBox.Name);
            ListOfIntTextBoxNames.Add(TemperatureTextBox.Name);

            ListOfDoubleTextBoxNames.Add(StressTextBox.Name);
            ListOfDoubleTextBoxNames.Add(DeflactionTextBox.Name);

            RegistrationOfFilters();
        }

        public void RegistrationOfFilters()
        {

        }

        private void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            int TestInt = Int32.Parse(TemperatureTextBox.Text);
            TestsDataGrid.ItemsSource = AllTestDataList.Where(DynamicLINQbuilder.WhereMethod<TestDataClass>("Temperature", TestInt, BinaryOperators.GreaterThan));
        }

        private void FilterTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            string CurrentTextBoxName = (sender as TextBox).Name;
            if (ListOfIntTextBoxNames.Where(res => res == CurrentTextBoxName).FirstOrDefault() != null)
            {
                AllFilters.Where(res => res.NameOfFilterTextBox == CurrentTextBoxName).First().Constant = Int32.Parse((sender as TextBox).Text);
            }
        }
    }
}
