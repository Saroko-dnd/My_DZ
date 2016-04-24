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

namespace Example3Resource
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStatic_Click(object sender, RoutedEventArgs e)
        {
            MainWin.Resources["buttomGradient"] = new SolidColorBrush(Colors.LightSkyBlue);
        }

        private void btnDynamic_Click(object sender, RoutedEventArgs e)
        {
            MainWin.Resources["buttomGradient"] = new SolidColorBrush(Colors.LightSkyBlue);
        }
    }
}
