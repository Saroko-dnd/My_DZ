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

namespace Diagramm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<KeyValuePair<string, int>> valueList = new List<KeyValuePair<string, int>>();
            valueList.Add(new KeyValuePair<string, int>("Developer", 60));
            valueList.Add(new KeyValuePair<string, int>("Disigner", 20));
            valueList.Add(new KeyValuePair<string, int>("Tester", 50));
            valueList.Add(new KeyValuePair<string, int>("QA", 30));
            valueList.Add(new KeyValuePair<string, int>("Project Manager", 40));


            ColumnSeries.DataContext = valueList;
            pieChart.DataContext = valueList;
            AreaSeries.DataContext = valueList;
            BarSeries.DataContext = valueList;
            BubbleSeries.DataContext = valueList;


            Dictionary<string, int> Values = new Dictionary<string, int>();
            Values.Add("Developer", 60);
            Values.Add("Disigner", 20);
            Values.Add("Tester", 50);
            Values.Add("QA", 30);
            Values.Add("Project Manager", 40);

            ScatterSeries.DataContext = Values;
        }
    }
}
