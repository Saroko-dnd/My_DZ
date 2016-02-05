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
using System.Linq;

namespace TASK_2_ado_net
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (MainTabControl.SelectedIndex)
            {
                case 0:
                    DBConnector.ExecuteFirstQuery(DataGridFirstQuery,QueryProgressBar, ProgramStateLabel);
                    break;
                case 1:
                    DBConnector.ExecuteSecondQuery(DataGridSecondQuery, QueryProgressBar, ProgramStateLabel);
                    break;
                case 2:
                    DBConnector.ExecuteThirdQuery(DataGridThirdQuery, QueryProgressBar, ProgramStateLabel);
                    break;
                default:
                    break;
            }
        }
    }
}
