using EXAMprogramObserverPattern.Observables;
using EXAMprogramObserverPattern.Observers;
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

namespace EXAMprogramObserverPattern
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Publisher MainPublisher = new Publisher();
        public MainWindow()
        {
            InitializeComponent();

            NewsPaperTextBox.TextChanged += TextChangedNewspaperTextBox;
            JournalTextBox.TextChanged += TextChangedJournalTextBox;

            PostOffice UniqueOffice = new PostOffice(PostOfficeJournalTextBox, PostOfficeNewspaperTextBox);

            List<string> ListForFirstObserver = new List<string>() { "Times", "Spring", "Winter" };
            List<string> ListForSecondObserver = new List<string>() { "Summer", "Children", "Games" };
            List<string> ListForThirdObserver = new List<string>() { "Tanks", "Trees", "Cars" };

            FirstReaderListBox.ItemsSource = ListForFirstObserver;
            SecondReaderListBox.ItemsSource = ListForSecondObserver;
            ThirdReaderListBox.ItemsSource = ListForThirdObserver;

            UniqueOffice.AddObserver(new Reader(ListForFirstObserver, FirstReaderTextBox));
            UniqueOffice.AddObserver(new Reader(ListForSecondObserver, SecondReaderTextBox));
            UniqueOffice.AddObserver(new Reader(ListForThirdObserver, ThirdReaderTextBox));

            MainPublisher.AddObserver(UniqueOffice);
        }

        private void TextChangedNewspaperTextBox(object sender, EventArgs e)
        {
            MainPublisher.CreateNewNewspaper((sender as TextBox).Text);
        }

        private void TextChangedJournalTextBox(object sender, EventArgs e)
        {
            MainPublisher.CreateNewJournal((sender as TextBox).Text);
        }
    }
}
