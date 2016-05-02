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
using СomposerPattern.ComposerClasses;

namespace ComposerPatternWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Text TestObjectOfTextClass;
        public static bool ProgramBusy = false;

        public MainWindow()
        {
            InitializeComponent();

            ThreadPool.SetMinThreads(4,4);

            LengthOfWordsTextBox.PreviewTextInput += CharsKiller.InputValidationOnlyInt;
        }

        public void DisplayTextAsync(string CurrentText)
        {
            TestObjectOfTextClass = new Text();
            TestObjectOfTextClass.Parse(CurrentText);
            string TextFromParser = TestObjectOfTextClass.TextToString();
            Application.Current.Dispatcher.Invoke(new Action(() => ResultTextBox.Text = TextFromParser));
            Application.Current.Dispatcher.Invoke(new Action(() => StatusLabel.Content = MyResourses.Texts.StatusFree));
            Application.Current.Dispatcher.Invoke(new Action(() => StatusLabel.Foreground = Brushes.Green));
            ProgramBusy = false;
        }

        public void ChangeWordsAsync(string CurrentText)
        {
            TestObjectOfTextClass = new Text();
            TestObjectOfTextClass.Parse(CurrentText);
            string TextFromParser = TestObjectOfTextClass.ChangeAllWords();
            Application.Current.Dispatcher.Invoke(new Action(() => ResultTextBox.Text = TextFromParser));
            Application.Current.Dispatcher.Invoke(new Action(() => StatusLabel.Content = MyResourses.Texts.StatusFree));
            Application.Current.Dispatcher.Invoke(new Action(() => StatusLabel.Foreground = Brushes.Green));
            ProgramBusy = false;
        }

        public void DeleteWordsAsync(string CurrentText, int LengthOfWords )
        {
            TestObjectOfTextClass = new Text();
            TestObjectOfTextClass.Parse(CurrentText);
            string TextFromParser = TestObjectOfTextClass.DeleteAllWords(LengthOfWords);
            Application.Current.Dispatcher.Invoke(new Action(() => ResultTextBox.Text = TextFromParser));
            Application.Current.Dispatcher.Invoke(new Action(() => StatusLabel.Content = MyResourses.Texts.StatusFree));
            Application.Current.Dispatcher.Invoke(new Action(() => StatusLabel.Foreground = Brushes.Green));
            ProgramBusy = false;
        }

        private void DisplayTextButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewTextBox.Text != string.Empty && !ProgramBusy)
            {
                string CurrentText = NewTextBox.Text;
                ProgramBusy = true;
                StatusLabel.Content = MyResourses.Texts.StatusBusy;
                StatusLabel.Foreground = Brushes.Red;
                ThreadPool.QueueUserWorkItem(o => DisplayTextAsync(CurrentText));
            }
        }

        private void PrintWithChangeWordsButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewTextBox.Text != string.Empty && !ProgramBusy)
            {
                string CurrentText = NewTextBox.Text;
                ProgramBusy = true;
                StatusLabel.Content = MyResourses.Texts.StatusBusy;
                StatusLabel.Foreground = Brushes.Red;
                ThreadPool.QueueUserWorkItem(o => ChangeWordsAsync(CurrentText));
            }
        }

        private void PrintWithDeleteWordsButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewTextBox.Text != string.Empty)
            {
                string CurrentText = NewTextBox.Text;
                try
                {
                    int LengthOfWords = Int32.Parse(LengthOfWordsTextBox.Text);
                    ProgramBusy = true;
                    StatusLabel.Content = MyResourses.Texts.StatusBusy;
                    StatusLabel.Foreground = Brushes.Red;
                    ThreadPool.QueueUserWorkItem(o => DeleteWordsAsync(CurrentText, LengthOfWords));
                }
                catch
                {
                    MessageBox.Show(MyResourses.Texts.LengthError, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
