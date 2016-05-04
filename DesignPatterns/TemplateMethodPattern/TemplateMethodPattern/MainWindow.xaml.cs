using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using TemplateMethodPattern.TestClasses;

namespace TemplateMethodPattern
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool ProgramBusy = false;

        public MainWindow()
        {
            InitializeComponent();

            ThreadPool.SetMinThreads(4,4);
        }

        private void GetTestResultsAsync()
        {
            StringBuilder BuilderForConsoleTextBox = new StringBuilder();
            List<AbstractTest> ListOfTests = new List<AbstractTest>();
            ListOfTests.Add(new SmallTest());
            ListOfTests.Add(new Test());
            ListOfTests.Add(new BigTest());
            foreach (AbstractTest CurrentTest in ListOfTests)
            {
                BuilderForConsoleTextBox.Append(CurrentTest.PerformTest().ToString());
            }
            Application.Current.Dispatcher.Invoke(new Action(() => ConsoleTextBox.Text = BuilderForConsoleTextBox.ToString()));
            Application.Current.Dispatcher.Invoke(new Action(() => StatusLabel.Content = MyResourses.Texts.StatusFree));
            ProgramBusy = false;
        }

        private void PrintResultsOfTestsButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProgramBusy)
            {
                MessageBox.Show(MyResourses.Texts.ProgramBusyError, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ProgramBusy = true;
                StatusLabel.Content = MyResourses.Texts.StatusBusy;
                ThreadPool.QueueUserWorkItem(o => GetTestResultsAsync());
            }
        }
    }
}
