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

namespace WPF_examples_linq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<DataClass> DataStorage = new List<DataClass>();
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

            var Result = DataStorage.GroupBy(res => { if (res.Year < 40) return "Young"; else return "Old"; });
            StringBuilder MainBuilder = new StringBuilder();

            foreach (var GroupedData in Result)
            {
                MainBuilder.Append(GroupedData.Key);
                MainBuilder.Append(" ");
                MainBuilder.Append(GroupedData.Count().ToString());
                MainBuilder.AppendLine();
            }

            ConsoleTextBox.Text = MainBuilder.ToString();

        }
    }
}
