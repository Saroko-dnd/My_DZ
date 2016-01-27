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

namespace Example3RoutedEvents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExtraButton_MyButtonClick(object sender, RoutedEventArgs e)
        {
            ExtraButton button = (ExtraButton)sender;
            String info = String.Format("{0} {1} {2}", button.Name, e.RoutedEvent.Name, e.Source);
            MessageBox.Show(info);
        }

    }
}
