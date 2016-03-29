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
        public bool MessageWasMade = false;
        public bool Limit = true;
        public int CounterForDownload = 0;
        public List<string> SymbolsToErase = new List<string>();
        public List<string> ListOfLoadedURL = new List<string>();
        public int MainCounterForRecursion = 0;
        public int MaxAmountOfWebPages = 50;
        public string CopyOfCurrentURL = string.Empty;
        public string CurrentDirectoryFullName = Directory.GetCurrentDirectory();
        public List<WebElement> AllWebElements = new List<WebElement>();
        public List<string> AllProtocols = new List<string>();
        public Regex FileCheck = new Regex(".(jpg|png|bmp|gif|pcx|tga|jpeg|ico|js|css)");
        public Regex ImageCheck = new Regex(".(jpg|png|bmp|gif|pcx|tga|jpeg|ico)");
        public Regex WebPageCheck = new Regex(".html");
        public bool ProgramShutDown = false;
        public bool ProgramBusy = false;
        public StringBuilder MainStringBuilder = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();

            AmountOfPagesTextBox.PreviewTextInput += CharsKiller.InputValidationOnlyNumbers;

            AllWebElements.Add(new WebElement("a", "href"));
            AllWebElements.Add(new WebElement("img", "src"));
            AllWebElements.Add(new WebElement("script", "src"));
            AllWebElements.Add(new WebElement("link ", "href"));

            AllProtocols.Add("https:");
            AllProtocols.Add("http:");

            SymbolsToErase.Add("*");
            SymbolsToErase.Add("|");
            SymbolsToErase.Add("/");
            SymbolsToErase.Add(":");
            SymbolsToErase.Add("\"");
            SymbolsToErase.Add(">");
            SymbolsToErase.Add("<");
            SymbolsToErase.Add("?");
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
                        MainStringBuilder.AppendLine(MyResourses.Texts.FileSaved + " " + FileName);
                        Application.Current.Dispatcher.Invoke(new Action(() => ConsoleTextBox.Text = MainStringBuilder.ToString()));
                    }
                    catch (Exception CurrentException)
                    {
                        if (!ProgramShutDown)
                        {
                            //MessageBox.Show(CurrentException.Message + FileName + " " + URLtoFile, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                            MainStringBuilder.AppendLine(CurrentException.Message + MyResourses.Texts.FileName + FileName + " " + MyResourses.Texts.URLtoFile + " " + URLtoFile);
                            Application.Current.Dispatcher.Invoke(new Action(() => ConsoleTextBox.Text = MainStringBuilder.ToString()));
                        }
                    }
                }
            }
            catch (Exception CurrentException)
            {
                if (!ProgramShutDown)
                {
                    /*MessageBox.Show(CurrentException.Message + MyResourses.Texts.FileName + FileName + " " + MyResourses.Texts.URLtoFile + " " + URLtoFile, 
                        MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);*/
                    MainStringBuilder.AppendLine(CurrentException.Message + MyResourses.Texts.FileName + FileName + " " + MyResourses.Texts.URLtoFile + " " + URLtoFile);
                    Application.Current.Dispatcher.Invoke(new Action(() => ConsoleTextBox.Text = MainStringBuilder.ToString()));
                }
            }
        }

        public void SavePagesConnectedToCurrentPage(List<string> MainListOReferences, string DirectoryName, string ResDirectoryName, string CurrentProtocol, 
            string CurrentURL, string AddonForNamelessURL)
        {
            foreach (string CurrentLink in MainListOReferences)
            {
                if (Limit)
                {
                    return;
                }
                bool ThisLinkMustBeDownload = true;
                if (CurrentLink != null && CurrentLink != string.Empty)
                {
                    if (!FileCheck.IsMatch(CurrentLink) && CurrentLink.Contains(CopyOfCurrentURL))
                    {
                        string CurLinkProtocol = CurrentLink.Split('/')[0];
                        if (ListOfLoadedURL.Where(res => res == CurrentLink).Count() == 0)
                        {
                            Downloading(CurrentLink, CurLinkProtocol, DirectoryName, ResDirectoryName);
                        }
                    }
                    else if (!FileCheck.IsMatch(CurrentLink))
                    {
                        foreach (string Protokol in AllProtocols)
                        {
                            if (CurrentLink.Contains(Protokol))
                            {
                                ThisLinkMustBeDownload = false;
                                break;
                            }
                        }
                        if (ThisLinkMustBeDownload)
                        {
                            string CurLinkProtocol = CurrentProtocol;
                            string ProperCurrentLink = string.Empty;
                            if (CurrentLink.Contains("//"))
                            {
                                ProperCurrentLink = CurrentProtocol + CurrentLink;
                            }
                            else if (CurrentLink.ToCharArray()[0] == '/')
                            {
                                ProperCurrentLink = CurrentProtocol + "//" + AddonForNamelessURL.Replace(CurrentProtocol + "//", "").Replace("/", "") + CurrentLink;
                            }
                            else if (WebPageCheck.IsMatch(CurrentLink))
                            {
                                char[] CurrentURLcharArray = CurrentURL.ToCharArray();
                                int LastIndexOfCharArray = CurrentURLcharArray.Length - 1;
                                int LocalCounterOfDividers = 0;
                                while (LocalCounterOfDividers != 1)
                                {
                                    if (CurrentURLcharArray[LastIndexOfCharArray] == '/')
                                    {
                                        ++LocalCounterOfDividers;
                                    }
                                    if (LocalCounterOfDividers != 1)
                                    {
                                        --LastIndexOfCharArray;
                                    }
                                }
                                StringBuilder ThirdLocalStringBuilder = new StringBuilder();
                                int CounterAndIndex = 0;
                                foreach (char CurrentSymbol in CurrentURLcharArray)
                                {
                                    ThirdLocalStringBuilder.Append(CurrentSymbol);
                                    ++CounterAndIndex;
                                    if (CounterAndIndex > LastIndexOfCharArray)
                                    {
                                        break;
                                    }
                                }
                                ProperCurrentLink = ThirdLocalStringBuilder.ToString() + CurrentLink;
                            }
                            if (ProperCurrentLink != string.Empty && ListOfLoadedURL.Where(res => res == ProperCurrentLink).Count() == 0)
                            {
                                Downloading(ProperCurrentLink, CurLinkProtocol, DirectoryName, ResDirectoryName);
                            }
                        }
                    }
                }
            }
        }


        public string SaveFilesConnectedToCurrentPage(List<string> MainListOReferences, string ResDirectoryName, string CurrentProtocol, string CurrentURL,
            string AddonForNamelessURL, string MainPage)
        {
            string NewRefValue = string.Empty;
            foreach (string CurrentReference in MainListOReferences)
            {
                if (CurrentReference.Contains("js"))
                {
                    int rrr = 0;
                }
                if (CurrentReference != null && CurrentReference != string.Empty)
                {
                    if (FileCheck.IsMatch(CurrentReference))
                    {
                        bool ItIsDone = false;
                        string OldFileName = CurrentReference.Split('/')[CurrentReference.Split('/').Length - 1];
                        if (OldFileName != null && OldFileName != string.Empty)
                        {
                            string NewFileName = string.Empty;
                            NewFileName = OldFileName.Split('?')[0]; //избавляемся от неопределенности в формате данных

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
                                    int CounterForDeleteParts = 0;
                                    string[] AllPartsOfLink = BufForReference.Split('/');
                                    foreach (string CurrentPartOfLink in AllPartsOfLink)
                                    {
                                        if (CurrentPartOfLink == "..")
                                        {
                                            ++CounterForDeleteParts;
                                        }
                                        else if (CurrentPartOfLink != string.Empty)
                                        {
                                            break;
                                        }
                                    }
                                    //Код до звездочек выполняется если скачиваемый файл находиться на одну или более папок выше текущей ссылки
                                    if (CounterForDeleteParts > 0)
                                    {
                                        char[] ArrayOfCharsForLink = CurrentURL.ToCharArray();
                                        int LastIndexOfCharArray = ArrayOfCharsForLink.Length - 1;
                                        int CounterOfDividers = 0;
                                        while (CounterOfDividers != (CounterForDeleteParts + 1))
                                        {
                                            if (ArrayOfCharsForLink[LastIndexOfCharArray] == '/')
                                            {
                                                ++CounterOfDividers;
                                            }
                                            if (CounterOfDividers != (CounterForDeleteParts + 1))
                                            {
                                                --LastIndexOfCharArray;
                                            }
                                        }
                                        StringBuilder StringBuilderForNewLink = new StringBuilder();
                                        int CharIndexSecond = 0;
                                        while (CharIndexSecond <= LastIndexOfCharArray)
                                        {
                                            StringBuilderForNewLink.Append(ArrayOfCharsForLink[CharIndexSecond]);
                                            ++CharIndexSecond;
                                        }
                                        string ProperBufForReference = StringBuilderForNewLink.ToString() + BufForReference.Replace("../", "");
                                        SaveFile(NewFileName, ProperBufForReference, ResDirectoryName);
                                    }
                                    //**********************************************************************************************************
                                    else if (CurrentReference.Contains("//"))
                                    {
                                        SaveFile(NewFileName, CurrentProtocol + BufForReference, ResDirectoryName);
                                    }
                                    else
                                    {
                                        if (CurrentReference.ToCharArray()[0] != '/')
                                        {
                                            char[] CurrentURLcharArray = CurrentURL.ToCharArray();
                                            int LastIndexOfCharArray = CurrentURLcharArray.Length - 1;
                                            int LocalCounterOfDividers = 0;
                                            while (LocalCounterOfDividers != 1)
                                            {
                                                if (CurrentURLcharArray[LastIndexOfCharArray] == '/')
                                                {
                                                    ++LocalCounterOfDividers;
                                                }
                                                if (LocalCounterOfDividers != 1)
                                                {
                                                    --LastIndexOfCharArray;
                                                }
                                            }
                                            StringBuilder SecondLocalStringBuilder = new StringBuilder();
                                            int CounterAndIndex = 0;
                                            foreach (char CurrentSymbol in CurrentURLcharArray)
                                            {
                                                SecondLocalStringBuilder.Append(CurrentSymbol);
                                                ++CounterAndIndex;
                                                if (CounterAndIndex > LastIndexOfCharArray)
                                                {
                                                    break;
                                                }
                                            }
                                            SaveFile(NewFileName, SecondLocalStringBuilder.ToString() + BufForReference, ResDirectoryName);
                                        }
                                        else
                                        {
                                            SaveFile(NewFileName, CurrentProtocol + "//" + AddonForNamelessURL.Replace(CurrentProtocol + "//", "").Replace("/", "") + BufForReference, ResDirectoryName);
                                        }
                                    }
                                }
                            }
                            MainPage = MainPage.Replace(CurrentReference, NewRefValue);
                        }
                    }
                    else
                    {
                        string NewValueForLink = string.Empty;
                        bool ThisLinkMustBeDownload = true;
                        if (!FileCheck.IsMatch(CurrentReference) && CurrentReference.Contains(CopyOfCurrentURL))
                        {
                            NewValueForLink = CurrentReference;
                            //*******************************************************
                        }
                        else if (!FileCheck.IsMatch(CurrentReference))
                        {
                            foreach (string Protokol in AllProtocols)
                            {
                                if (CurrentReference.Contains(Protokol))
                                {
                                    ThisLinkMustBeDownload = false;
                                    break;
                                }
                            }
                            if (ThisLinkMustBeDownload)
                            {
                                string CurLinkProtocol = CurrentProtocol;
                                string ProperCurrentLink = string.Empty;
                                if (CurrentReference.Contains("//"))
                                {
                                    ProperCurrentLink = CurrentProtocol + CurrentReference;
                                }
                                else if (CurrentReference.ToCharArray()[0] == '/')
                                {
                                    ProperCurrentLink = CurrentProtocol + "//" + AddonForNamelessURL.Replace(CurrentProtocol + "//", "").Replace("/", "") + CurrentReference;
                                }
                                else if (WebPageCheck.IsMatch(CurrentReference))
                                {
                                    char[] CurrentURLcharArray = CurrentURL.ToCharArray();
                                    int LastIndexOfCharArray = CurrentURLcharArray.Length - 1;
                                    int LocalCounterOfDividers = 0;
                                    while (LocalCounterOfDividers != 1)
                                    {
                                        if (CurrentURLcharArray[LastIndexOfCharArray] == '/')
                                        {
                                            ++LocalCounterOfDividers;
                                        }
                                        if (LocalCounterOfDividers != 1)
                                        {
                                            --LastIndexOfCharArray;
                                        }
                                    }
                                    StringBuilder ThirdLocalStringBuilder = new StringBuilder();
                                    int CounterAndIndex = 0;
                                    foreach (char CurrentSymbol in CurrentURLcharArray)
                                    {
                                        ThirdLocalStringBuilder.Append(CurrentSymbol);
                                        ++CounterAndIndex;
                                        if (CounterAndIndex > LastIndexOfCharArray)
                                        {
                                            break;
                                        }
                                    }
                                    ProperCurrentLink = ThirdLocalStringBuilder.ToString() + CurrentReference;
                                }
                                //*****************************************************************************
                                NewValueForLink = ProperCurrentLink;
                            }
                        }
                        string FinalNewValueForLink = NewValueForLink.Replace(CurrentProtocol, "");
                        foreach (string CurProt in AllProtocols)
                        {
                            FinalNewValueForLink = FinalNewValueForLink.Replace(CurProt, "");
                        }

                        foreach (string Symbol in SymbolsToErase)
                        {
                            FinalNewValueForLink = FinalNewValueForLink.Replace(Symbol, "_");
                        }
                        if (FinalNewValueForLink != string.Empty)
                        {
                            if (WebPageCheck.IsMatch(NewValueForLink))
                            {
                                MainPage = MainPage.Replace("\"" + CurrentReference + "\"", "\"" + FinalNewValueForLink + "\"");
                            }
                            else
                            {
                                MainPage = MainPage.Replace("\"" + CurrentReference + "\"", "\"" + FinalNewValueForLink + ".html\"");
                            }
                        }
                    }
                }
            }
            return MainPage;
        }

        public void Downloading(string CurrentURL, string CurrentProtocol, string DirectoryName, string ResDirectoryName)
        {
            if (MainCounterForRecursion == MaxAmountOfWebPages || Limit)
            {
                Limit = true;
                return;
            }
            ++MainCounterForRecursion;
            try
            {
                ListOfLoadedURL.Add(CurrentURL);
                MainStringBuilder.Clear();
                string AddonForNamelessURL = string.Empty;
                Application.Current.Dispatcher.Invoke(new Action (() => AddonForNamelessURL = URLtextBox.Text.Split('/')[0] + "//" + URLtextBox.Text.Split('/')[2]));
                string MainPage = string.Empty;
                using (WebClient MainWebClient = new WebClient())
                {
                    MainWebClient.Encoding = Encoding.UTF8;
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
                //Вызываем функцию, которая сохранит все файлы текущей страницы
                MainPage = SaveFilesConnectedToCurrentPage(MainListOReferences, ResDirectoryName, CurrentProtocol, CurrentURL, AddonForNamelessURL, MainPage);

                string NewNameForPage = CurrentURL.Replace(CurrentProtocol,"");
                foreach (string CurProt in AllProtocols)
                {
                    NewNameForPage = NewNameForPage.Replace(CurProt, "");
                }
                
                foreach (string Symbol in SymbolsToErase)
                {
                    NewNameForPage = NewNameForPage.Replace(Symbol, "_");
                }

                if (WebPageCheck.IsMatch(NewNameForPage))
                {
                    File.WriteAllText(DirectoryName + "\\" + NewNameForPage, MainPage, Encoding.UTF8);
                }
                else
                {
                    File.WriteAllText(DirectoryName + "\\" + NewNameForPage + ".html", MainPage, Encoding.UTF8);
                }
                ++CounterForDownload;
                Application.Current.Dispatcher.Invoke(new Action(() => PagesCountLabel.Content = CounterForDownload.ToString()));

                //Вызываем функцию, которая сохранит все страницы , на которые имелись ссылки на текущей странице (рекурсия начинается здесь)
                SavePagesConnectedToCurrentPage(MainListOReferences, DirectoryName, ResDirectoryName, CurrentProtocol, CurrentURL, AddonForNamelessURL);

                /*foreach (string CurrentLink in MainListOReferences)
                {
                    if (Limit)
                    {
                        return;
                    }
                    bool ThisLinkMustBeDownload = true;
                    if (CurrentLink != null && CurrentLink != string.Empty)
                    {
                        if (!FileCheck.IsMatch(CurrentLink) && CurrentLink.Contains(CopyOfCurrentURL))
                        {
                            string CurLinkProtocol = CurrentLink.Split('/')[0];
                            if (ListOfLoadedURL.Where(res => res == CurrentLink).Count() == 0)
                            {
                                Downloading(CurrentLink, CurLinkProtocol, DirectoryName, ResDirectoryName);
                            }
                        }
                        else if (!FileCheck.IsMatch(CurrentLink))
                        {
                            foreach (string Protokol in AllProtocols)
                            {
                                if (CurrentLink.Contains(Protokol))
                                {
                                    ThisLinkMustBeDownload = false;
                                    break;
                                }
                            }
                            if (ThisLinkMustBeDownload)
                            {
                                string CurLinkProtocol = CurrentProtocol;
                                string ProperCurrentLink = string.Empty;
                                if (CurrentLink.Contains("//"))
                                {
                                    ProperCurrentLink = CurrentProtocol + CurrentLink;
                                }
                                else if (CurrentLink.ToCharArray()[0] == '/')
                                {
                                    ProperCurrentLink = CurrentProtocol + "//" + AddonForNamelessURL.Replace(CurrentProtocol + "//", "").Replace("/", "") + CurrentLink;
                                }
                                else if (WebPageCheck.IsMatch(CurrentLink))
                                {
                                    char[] CurrentURLcharArray = CurrentURL.ToCharArray();
                                    int LastIndexOfCharArray = CurrentURLcharArray.Length - 1;
                                    int LocalCounterOfDividers = 0;
                                    while (LocalCounterOfDividers != 1)
                                    {
                                        if (CurrentURLcharArray[LastIndexOfCharArray] == '/')
                                        {
                                            ++LocalCounterOfDividers;
                                        }
                                        if (LocalCounterOfDividers != 1)
                                        {
                                            --LastIndexOfCharArray;
                                        }
                                    }
                                    StringBuilder ThirdLocalStringBuilder = new StringBuilder();
                                    int CounterAndIndex = 0;
                                    foreach (char CurrentSymbol in CurrentURLcharArray)
                                    {
                                        ThirdLocalStringBuilder.Append(CurrentSymbol);
                                        ++CounterAndIndex;
                                        if (CounterAndIndex > LastIndexOfCharArray)
                                        {
                                            break;
                                        }
                                    }
                                    ProperCurrentLink = ThirdLocalStringBuilder.ToString() + CurrentLink;
                                }
                                if (ProperCurrentLink != string.Empty && ListOfLoadedURL.Where(res => res == ProperCurrentLink).Count() == 0)
                                {
                                    Downloading(ProperCurrentLink, CurLinkProtocol, DirectoryName, ResDirectoryName);
                                }
                            }
                        }
                    }
                }*/

                //
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
                if (Limit && !MessageWasMade)
                {
                    MessageWasMade = true;
                    ProgramBusy = false;
                    MessageBox.Show(MyResourses.Texts.Done, MyResourses.Texts.Message, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Application.Current.Dispatcher.Invoke(new Action(() => StatusLabel.Content = MyResourses.Texts.StatusFree));
                }
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
                        try
                        {
                            MaxAmountOfWebPages = Int32.Parse(AmountOfPagesTextBox.Text);
                        }
                        catch
                        {
                            MessageBox.Show(MyResourses.Texts.CantParseInt, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                            MaxAmountOfWebPages = 100;
                            AmountOfPagesTextBox.Text = "100";
                        }

                        Limit = false;
                        MessageWasMade = false;
                        MainStringBuilder.Clear();
                        ProgramBusy = true;
                        StatusLabel.Content = MyResourses.Texts.StatusBusy;
                        string NewUrl = URLtextBox.Text;
                        CopyOfCurrentURL = NewUrl;
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
