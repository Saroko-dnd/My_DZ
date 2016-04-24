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
            FirstDayTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            FirstDayTextBox.PreviewTextInput += CharsKiller.InputValidation;
            FirstMonthTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            FirstMonthTextBox.PreviewTextInput += CharsKiller.InputValidation;
            FirstYearTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            FirstYearTextBox.PreviewTextInput += CharsKiller.InputValidation;
            SecondDayTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            SecondDayTextBox.PreviewTextInput += CharsKiller.InputValidation;
            SecondMonthTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            SecondMonthTextBox.PreviewTextInput += CharsKiller.InputValidation;
            SecondYearTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            SecondYearTextBox.PreviewTextInput += CharsKiller.InputValidation;
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
                case 3:
                    DBConnector.ExecuteFourthQuery(CityNameTextBox,AmountOfCustomersLabel, QueryProgressBar, 
                        ProgramStateLabel);
                    break;
                case 4:
                    DBConnector.ExecuteFifthQuery(FirstDayTextBox, FirstMonthTextBox, FirstYearTextBox,
                        SecondDayTextBox,SecondMonthTextBox, SecondYearTextBox, DataGridFifthQuery,
                        QueryProgressBar, ProgramStateLabel);
                    break;
                default:
                    break;
            }
        }
    }
}
