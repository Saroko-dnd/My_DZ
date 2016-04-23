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
using System.Threading;
using System.Reflection;

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

            ThreadPool.SetMinThreads(4, 4);

            TestsDataGrid.AutoGeneratingColumn += TestDataClass.DataGridAutoGeneratingColumn;
            TestsDataGrid.ItemsSource = AllTestDataList;

            DayTextBox.PreviewTextInput += CharsKiller.OnlyNumbersPreviewTextInput;
            MonthTextBox.PreviewTextInput += CharsKiller.OnlyNumbersPreviewTextInput;
            YearTextBox.PreviewTextInput += CharsKiller.OnlyNumbersPreviewTextInput;
            TemperatureTextBox.PreviewTextInput += CharsKiller.OnlyNumbersSignedPreviewTextInput;
            StressTextBox.PreviewTextInput += CharsKiller.OnlyDoublePreviewTextInput;
            DeflactionTextBox.PreviewTextInput += CharsKiller.OnlyDoublePreviewTextInput;
            DayTextBox.TextChanged += FilterTextBoxTextChanged;
            MonthTextBox.TextChanged += FilterTextBoxTextChanged;
            YearTextBox.TextChanged += FilterTextBoxTextChanged;
            TemperatureTextBox.TextChanged += FilterTextBoxTextChanged;
            StressTextBox.TextChanged += FilterTextBoxTextChanged;
            DeflactionTextBox.TextChanged += FilterTextBoxTextChanged;

            BinaryOperatorsForTestDayComboBox.SelectionChanged += FilterComboBoxSelectionChanged;
            BinaryOperatorsForTestMonthComboBox.SelectionChanged += FilterComboBoxSelectionChanged;
            BinaryOperatorsForTestYearComboBox.SelectionChanged += FilterComboBoxSelectionChanged;
            BinaryOperatorsForTestTemperatureComboBox.SelectionChanged += FilterComboBoxSelectionChanged;
            BinaryOperatorsForTestStressComboBox.SelectionChanged += FilterComboBoxSelectionChanged;
            BinaryOperatorsForTestDeflactionComboBox.SelectionChanged += FilterComboBoxSelectionChanged;

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
            AllFilters.Add(new DataFilter("DayOfTest", null, null, "DayTextBox", "BinaryOperatorsForTestDayComboBox"));
            AllFilters.Add(new DataFilter("MonthOfTest", null, null, "MonthTextBox", "BinaryOperatorsForTestMonthComboBox"));
            AllFilters.Add(new DataFilter("YearOfTest", null, null, "YearTextBox", "BinaryOperatorsForTestYearComboBox"));
            AllFilters.Add(new DataFilter("Temperature", null, null, "TemperatureTextBox", "BinaryOperatorsForTestTemperatureComboBox"));
            AllFilters.Add(new DataFilter("Stress", null, null, "StressTextBox", "BinaryOperatorsForTestStressComboBox"));
            AllFilters.Add(new DataFilter("Deflection", null, null, "DeflactionTextBox", "BinaryOperatorsForTestDeflactionComboBox"));
        }

        private void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            StatusLabel.Content = MyResourses.Texts.StatusBusy;
            ThreadPool.QueueUserWorkItem(o => ApplyAllFilters());
        }

        private void ApplyAllFilters()
        {
            bool FirstTime = true;
            List<TestDataClass> BufferForItemsSource = null;
            foreach (DataFilter CurrentFilter in AllFilters)
            {
                if (CurrentFilter.CurrentBinaryOperator != null && CurrentFilter.Constant != null)
                {
                    if (FirstTime)
                    {
                        BufferForItemsSource = AllTestDataList.Where(DynamicLINQbuilder.WhereMethod<TestDataClass>(CurrentFilter.PropertyName, CurrentFilter.Constant, 
                            CurrentFilter.CurrentBinaryOperator)).ToList();
                        FirstTime = false;
                    }
                    else
                    {
                        BufferForItemsSource = BufferForItemsSource.Where(DynamicLINQbuilder.WhereMethod<TestDataClass>(CurrentFilter.PropertyName, CurrentFilter.Constant,
                            CurrentFilter.CurrentBinaryOperator)).ToList();
                    }
                }
            }
            Application.Current.Dispatcher.Invoke(new Action(() => TestsDataGrid.ItemsSource = BufferForItemsSource));
            Application.Current.Dispatcher.Invoke(new Action(() => StatusLabel.Content = MyResourses.Texts.StatusFree));
        }

        private void FilterTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            string CurrentTextBoxName = (sender as TextBox).Name;
            string NumberInTextBox = (sender as TextBox).Text;
            if (ListOfIntTextBoxNames.Where(res => res == CurrentTextBoxName).FirstOrDefault() != null)
            {
                try
                {
                    AllFilters.Where(res => res.NameOfFilterTextBox == CurrentTextBoxName).First().Constant = Int32.Parse(NumberInTextBox);
                    (sender as TextBox).BorderBrush = Brushes.Black;
                }
                catch(FormatException CurrentException)
                {
                    AllFilters.Where(res => res.NameOfFilterTextBox == CurrentTextBoxName).First().Constant = null;
                    (sender as TextBox).BorderBrush = Brushes.Red;
                }
                catch (OverflowException CurrentException)
                {
                    AllFilters.Where(res => res.NameOfFilterTextBox == CurrentTextBoxName).First().Constant = null;
                    (sender as TextBox).BorderBrush = Brushes.Red;
                }
            }
            else if (ListOfDoubleTextBoxNames.Where(res => res == CurrentTextBoxName).FirstOrDefault() != null)
            {
                try
                {
                    AllFilters.Where(res => res.NameOfFilterTextBox == CurrentTextBoxName).First().Constant = Double.Parse(NumberInTextBox);
                    (sender as TextBox).BorderBrush = Brushes.Black;
                }
                catch (FormatException CurrentException)
                {
                    AllFilters.Where(res => res.NameOfFilterTextBox == CurrentTextBoxName).First().Constant = null;
                    (sender as TextBox).BorderBrush = Brushes.Red;
                }
                catch (OverflowException CurrentException)
                {
                    AllFilters.Where(res => res.NameOfFilterTextBox == CurrentTextBoxName).First().Constant = null;
                    (sender as TextBox).BorderBrush = Brushes.Red;
                }
            }
        }

        private void FilterComboBoxSelectionChanged(object sender, EventArgs e)
        {
            string CurrentComboBoxName = (sender as ComboBox).Name;
            if ((sender as ComboBox).SelectedItem.ToString() != MyResourses.Texts.NoFilter)
            {
                BinaryOperator SelectedOperator = new BinaryOperator((sender as ComboBox).SelectedItem.ToString());
                AllFilters.Where(res => res.NameOfFilterComboBox == CurrentComboBoxName).First().CurrentBinaryOperator = SelectedOperator;
            }
            else
            {
                AllFilters.Where(res => res.NameOfFilterComboBox == CurrentComboBoxName).First().CurrentBinaryOperator = null;
            }
        }
    }
}
