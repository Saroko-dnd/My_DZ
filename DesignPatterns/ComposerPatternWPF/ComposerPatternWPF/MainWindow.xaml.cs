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
using СomposerPattern.ComposerClasses;

namespace ComposerPatternWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Text TestObjectOfTextClass;

        public MainWindow()
        {
            InitializeComponent();

            LengthOfWordsTextBox.PreviewTextInput += CharsKiller.InputValidationOnlyInt;
        }

        private void DisplayTextButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewTextBox.Text != string.Empty)
            {
                TestObjectOfTextClass = new Text();
                TestObjectOfTextClass.Parse(NewTextBox.Text);
                ResultTextBox.Text = TestObjectOfTextClass.TextToString();
            }
        }

        private void PrintWithChangeWordsButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewTextBox.Text != string.Empty)
            {
                TestObjectOfTextClass = new Text();
                TestObjectOfTextClass.Parse(NewTextBox.Text);
                ResultTextBox.Text = TestObjectOfTextClass.ChangeAllWords();
            }
        }

        private void PrintWithDeleteWordsButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewTextBox.Text != string.Empty)
            {
                TestObjectOfTextClass = new Text();
                TestObjectOfTextClass.Parse(NewTextBox.Text);
                ResultTextBox.Text = TestObjectOfTextClass.DeleteAllWords(Int32.Parse(LengthOfWordsTextBox.Text));
            }
        }
    }
}
