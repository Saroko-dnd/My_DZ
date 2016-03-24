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
using System.IO;
using CsQuery;
using System.Threading;
using System.Text.RegularExpressions;

namespace ProgramForDownloadingWebSites
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string CurrentDirectoryFullName = Directory.GetCurrentDirectory();
        public List<WebElement> AllWebElements = new List<WebElement>();
        public Regex FileCheck = new Regex(".(jpg|png|bmp|gif|pcx|tga|jpeg|ico|js|css)");
        public bool ProgramShutDown = false;
        public bool ProgramBusy = false;
        public StringBuilder MainStringBuilder = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();

            AllWebElements.Add(new WebElement("img", "src"));
            AllWebElements.Add(new WebElement("script", "src"));
            AllWebElements.Add(new WebElement("link ", "href"));
        }

        public void SaveFile(string FileName, string URLtoFile, string ResDirectoryPath)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    try
                    {
                        webClient.DownloadFile(URLtoFile, ResDirectoryPath + "\\" + FileName);
                    }
                    catch (Exception CurrentException)
                    {
                        if (!ProgramShutDown)
                        {
                            MessageBox.Show(CurrentException.Message + FileName + " " + URLtoFile, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception CurrentException)
            {
                if (!ProgramShutDown)
                {
                    MessageBox.Show(CurrentException.Message + FileName +  " " + URLtoFile, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Downloading(string CurrentURL, string DirectoryName, string ResDirectoryName)
        {
            try
            {
                string MainPage = string.Empty;
                using (WebClient MainWebClient = new WebClient())
                {
                    MainPage = MainWebClient.DownloadString(CurrentURL);
                }

                List<string> MainListOReferences = new List<string>();
                CQ ObjectCQ = CQ.Create(MainPage);
                foreach (WebElement CurrentElement in AllWebElements)
                {
                    foreach (IDomObject CurObject in ObjectCQ.Find(CurrentElement.ElementName))
                    {
                        MainListOReferences.Add(CurObject.GetAttribute(CurrentElement.ReferencePartName));
                    }
                }
                string FileName = string.Empty;
                string NewRefValue = string.Empty;
                foreach (string CurrentReference in MainListOReferences)
                {
                    if (CurrentReference != null)
                    {
                        if (FileCheck.IsMatch(CurrentReference))
                        {
                            FileName = CurrentReference.Split('/')[CurrentReference.Split('/').Length - 1];
                            FileName = FileName.Split('?')[0]; //избавляемся от неопределенности в формате данных
                            NewRefValue = "./" + MyResourses.Texts.ResFolder + "/" + FileName;
                            if (CurrentReference.Contains("http"))
                            {
                                SaveFile(FileName, CurrentReference, ResDirectoryName); 
                            }
                            else
                            {
                                if (CurrentReference.Contains("//"))
                                {
                                    SaveFile(FileName, "https:" + CurrentReference, ResDirectoryName);
                                }
                                else
                                {
                                    SaveFile(FileName, "https://" + CurrentURL.Replace("https://", "").Replace("/","") + CurrentReference, ResDirectoryName);
                                }
                            }
                            MainPage = MainPage.Replace(CurrentReference, NewRefValue);
                        }
                    }
                }
                MessageBox.Show(MyResourses.Texts.Done, MyResourses.Texts.Message, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception CurrentException)
            {
                if (!ProgramShutDown)
                {
                    MessageBox.Show(CurrentException.Message + "downnloading", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            finally
            {
                ProgramBusy = false;
            }
        }

        private void StartSiteDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ProgramBusy)
                {
                    if (URLtextBox.Text == string.Empty)
                    {
                        MessageBox.Show(MyResourses.Texts.URLempty, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        ProgramBusy = true;
                        string NewUrl = URLtextBox.Text;
                        string ResDirectoryPath = Directory.GetCurrentDirectory() + "\\" + NewUrl.Replace("https:", "").Replace("/", "_") + "\\" + MyResourses.Texts.ResFolder;
                        string MainDirectoryPath = Directory.GetCurrentDirectory() + "\\" + NewUrl.Replace("https:", "").Replace("/", "_");
                        Directory.CreateDirectory(ResDirectoryPath);
                        ThreadPool.QueueUserWorkItem(o => Downloading(NewUrl, MainDirectoryPath, ResDirectoryPath));
                    }
                }
                else
                {
                    MessageBox.Show(MyResourses.Texts.ErrorProgramBusy, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
