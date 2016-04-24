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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

namespace WebParsing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<WordAndCount> AllWordsCounters = new List<WordAndCount>();
        public bool ProgramBusy = false;

        public MainWindow()
        {
            InitializeComponent();

            ResultDataGrid.AutoGeneratingColumn += WordAndCount.AutoGeneratingColumnForDataGrid;
            ResultDataGrid.ItemsSource = AllWordsCounters;
        }

        public void WorkWithWebRequest(string RequestString)
        {
            try
            {
                bool ThisIsContent = false;
                bool ThisIsHeader = true;
                bool ScriptStarted = false;

                Regex StartTag = new Regex("[<]");
                Regex EndTag = new Regex("[>]");
                Regex CheckForNumbers = new Regex(@"^\d+$");
                Regex ScriptCheck = new Regex("script");
                Regex ScriptBugCheck_1 = new Regex("[a-zA-Z]script");
                Regex ScriptBugCheck_2 = new Regex("script[a-zA-Z]");
                Regex EmptySpacesCheck = new Regex("nbsp");

                string ResultString = string.Empty;
                HttpWebRequest NewRequset = (HttpWebRequest)HttpWebRequest.Create(RequestString);
                HttpWebResponse NewResponse = (HttpWebResponse)NewRequset.GetResponse();
                StreamReader ResponseReader = new StreamReader(NewResponse.GetResponseStream(), true);
                string TestDString = ResponseReader.ReadToEnd();
                string[] Words = Regex.Split(TestDString, @"(?<=[<>])");
                List<string> BufForWords = new List<string>();
                foreach (string CurString in Words)
                {
                    if (StartTag.IsMatch(CurString) && !ScriptStarted)
                    {
                        if (!ThisIsHeader)
                        {
                            ThisIsContent = true;
                        }
                        ThisIsHeader = true;

                    }
                    else if (EndTag.IsMatch(CurString) && !ScriptCheck.IsMatch(CurString) && !ScriptBugCheck_1.IsMatch(CurString) && !ScriptBugCheck_2.IsMatch(CurString))
                    {
                        ThisIsHeader = false;
                    }
                    else if (EndTag.IsMatch(CurString) && ScriptCheck.IsMatch(CurString) && !ScriptBugCheck_1.IsMatch(CurString) && !ScriptBugCheck_2.IsMatch(CurString) && !ScriptStarted)
                    {
                        ScriptStarted = true;
                    }
                    else if (EndTag.IsMatch(CurString) && ScriptCheck.IsMatch(CurString) && !ScriptBugCheck_1.IsMatch(CurString) && !ScriptBugCheck_2.IsMatch(CurString) && ScriptStarted)
                    {
                        ScriptStarted = false;
                        ThisIsHeader = true;
                    }
                    if (ThisIsContent)
                    {
                        ThisIsContent = false;
                        if (!EmptySpacesCheck.IsMatch(CurString))
                        {
                            string ContentString = CurString.Replace("<", "").Replace("\n","").Replace("\r", "").Replace("\t", "");
                            string[] CurrentWords = ContentString.Split(' ','|','\\', '.',',',';','&','!',':','(',')','{','}','[',']','<','>','"','#','@','$','%','*','_','+','=','/');
                            foreach (string SingleWord in CurrentWords)
                            {
                                if (SingleWord != string.Empty)
                                {
                                    BufForWords.Add(SingleWord);
                                }
                            }
                        }
                    }
                }
                foreach (string SingleWord in BufForWords)
                {
                    if (AllWordsCounters.Where(res => res.Word == SingleWord).Count() == 1)
                    {
                        AllWordsCounters.Where(res => res.Word == SingleWord).First().Count = AllWordsCounters.Where(res => res.Word == SingleWord).First().Count + 1;
                    }
                    else
                    {
                        AllWordsCounters.Add(new WordAndCount(SingleWord, 1));
                    }
                }
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ProgramBusy = false;
                Application.Current.Dispatcher.Invoke(new Action(() => StatusLabel.Content = MyResourses.Texts.StatusFree));
                Application.Current.Dispatcher.Invoke(new Action(() => ResultDataGrid.Items.Refresh()));
            }
        }

        private void SendWebRequestButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ProgramBusy)
            {
                ProgramBusy = true;
                StatusLabel.Content = MyResourses.Texts.StatusBusy;
                string RequestText = RequestTextBox.Text;
                ThreadPool.QueueUserWorkItem(o => WorkWithWebRequest(RequestText));
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.CantSendRequestNow, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
