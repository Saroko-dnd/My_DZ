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
        public List<string> AllProtocols = new List<string>();
        public Regex FileCheck = new Regex(".(jpg|png|bmp|gif|pcx|tga|jpeg|ico|js|css)");
        public Regex ImageCheck = new Regex(".(jpg|png|bmp|gif|pcx|tga|jpeg|ico)");
        public bool ProgramShutDown = false;
        public bool ProgramBusy = false;
        public StringBuilder MainStringBuilder = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();

            AllWebElements.Add(new WebElement("img", "src"));
            AllWebElements.Add(new WebElement("script", "src"));
            AllWebElements.Add(new WebElement("link ", "href"));

            AllProtocols.Add("https:");
            AllProtocols.Add("http:");
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

        public void Downloading(string CurrentURL, string CurrentProtocol, string DirectoryName, string ResDirectoryName)
        {
            try
            {
                string AddonForNamelessURL = string.Empty;
                Application.Current.Dispatcher.Invoke(new Action (() => AddonForNamelessURL = URLtextBox.Text.Split('/')[0] + "//" + URLtextBox.Text.Split('/')[2]));
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
                string NewRefValue = string.Empty;
                foreach (string CurrentReference in MainListOReferences)
                {
                    if (CurrentReference != null)
                    {
                        if (FileCheck.IsMatch(CurrentReference))
                        {
                            bool ItIsDone = false;
                            string OldFileName = CurrentReference.Split('/')[CurrentReference.Split('/').Length - 1];
                            string NewFileName = string.Empty;
                            if (ImageCheck.IsMatch(CurrentReference))
                            {
                                NewFileName = OldFileName.Split('?')[0]; //избавляемся от неопределенности в формате данных
                            }
                            else
                            {
                                NewFileName = OldFileName;
                            }
                            string BufForReference = CurrentReference.Replace(OldFileName, NewFileName);

                            NewRefValue = "./" + MyResourses.Texts.ResFolder + "/" + NewFileName;
                            foreach (string CurProtocol in AllProtocols)
                            {
                                if (BufForReference.Contains(CurProtocol))
                                {
                                    SaveFile(NewFileName, BufForReference, ResDirectoryName);
                                    ItIsDone = true;
                                    break;
                                }
                            }
                            if (!ItIsDone)
                            {
                                if (BufForReference.Contains(CurrentProtocol))
                                {
                                    SaveFile(NewFileName, BufForReference, ResDirectoryName);
                                }
                                else
                                {
                                    if (CurrentReference.Contains("//"))
                                    {
                                        SaveFile(NewFileName, CurrentProtocol + BufForReference, ResDirectoryName);
                                    }
                                    else
                                    {
                                        SaveFile(NewFileName, CurrentProtocol + "//" + AddonForNamelessURL.Replace(CurrentProtocol + "//", "").Replace("/", "") + BufForReference, ResDirectoryName);
                                    }
                                }
                            }
                            MainPage = MainPage.Replace(CurrentReference, NewRefValue);
                        }
                    }
                }
                string NewNameForPage = CurrentURL.Replace(CurrentProtocol,"");
                foreach (string CurProt in AllProtocols)
                {
                    NewNameForPage = NewNameForPage.Replace(CurProt, "");
                }
                
                NewNameForPage = NewNameForPage.Replace("/","_");
                File.WriteAllText(DirectoryName + "\\" + NewNameForPage + ".html", MainPage, Encoding.UTF8);
                MessageBox.Show(MyResourses.Texts.Done, MyResourses.Texts.Message, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception CurrentException)
            {
                if (!ProgramShutDown)
                {
                    MessageBox.Show(CurrentException.Message + " !downnloading thread!", MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
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
                        string CurrentProtocol = NewUrl.Split('/')[0];
                        string ResDirectoryPath = Directory.GetCurrentDirectory() + "\\" + NewUrl.Replace(CurrentProtocol, "").Replace("/", "_") + "\\" + MyResourses.Texts.ResFolder;
                        string MainDirectoryPath = Directory.GetCurrentDirectory() + "\\" + NewUrl.Replace(CurrentProtocol, "").Replace("/", "_");
                        Directory.CreateDirectory(ResDirectoryPath);
                        ThreadPool.QueueUserWorkItem(o => Downloading(NewUrl, CurrentProtocol, MainDirectoryPath, ResDirectoryPath));
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
