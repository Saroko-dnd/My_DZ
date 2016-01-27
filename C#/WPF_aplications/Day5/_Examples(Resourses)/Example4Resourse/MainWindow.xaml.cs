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

namespace Example4Resourse
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            LinearGradientBrush brush = (LinearGradientBrush)cmd.FindResource("buttomGradient"); 
            // или 
            // LinearGradientBrush brush = (LinearGradientBrush)cmd.TryFindResource("buttomGradient");
            // TryFindResource - если не найдет ресурс, возвращает NULL
        }
    }
}
