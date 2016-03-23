using System;
using System.Collections;
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

namespace WPF_examples_linq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public StringBuilder StringBuikderForTextBox = new StringBuilder();
        public List<DataClass> DataStorage = new List<DataClass>();
        public List<DataClass_2> DataStorage_2 = new List<DataClass_2>();

        public MainWindow()
        {
            InitializeComponent();
            DataStorage.Add(new DataClass("Kevin",30, "Immortals", 110));
            DataStorage.Add(new DataClass("Max", 30, "Immortals", 100));
            DataStorage.Add(new DataClass("Kelly", 40, "Immortals", 100));
            DataStorage.Add(new DataClass("Henry", 40, "Immortals", 110));
            DataStorage.Add(new DataClass("Andrew", 32, "Geeks", 115));
            DataStorage.Add(new DataClass("Saladin", 32, "Geeks", 95));
            DataStorage.Add(new DataClass("Larry", 32, "Geeks", 95));
            DataStorage.Add(new DataClass("Ray", 35, "Geeks", 95));
            DataStorage.Add(new DataClass("Edward", 44, "Fools", 99));
            DataStorage.Add(new DataClass("Justin", 50, "Fools", 101));
            DataStorage.Add(new DataClass("John", 44, "Fools", 99));
            DataStorage.Add(new DataClass("Ryan", 40, "Fools", 99));

            DataStorage_2.Add(new DataClass_2("Fools", 100,400));
            DataStorage_2.Add(new DataClass_2("Immortals", 500, 600));
            DataStorage_2.Add(new DataClass_2("Geeks", 1100, 150));

            //пример GroupBy
            var Result = DataStorage.GroupBy(res => { if (res.Year < 40) return "Young"; else return "Old"; });

            StringBuikderForTextBox.Append("Результат группировки:");
            StringBuikderForTextBox.AppendLine();
            foreach (var GroupedData in Result)
            {
                StringBuikderForTextBox.Append(GroupedData.Key);
                StringBuikderForTextBox.Append(" ");
                StringBuikderForTextBox.Append(GroupedData.Count().ToString());
                StringBuikderForTextBox.AppendLine();
            }
            ConsoleTextBox.Text = StringBuikderForTextBox.ToString();

            //пример join
            QueryResultDataGrid.ItemsSource = DataStorage.Join(DataStorage_2, Original => Original.RelatedCommand,Addon => Addon.CommandName,
                (Original, Addon) => new {SuperName = Original.Name, Addon.Popularity}).ToList();

        }
    }
}
