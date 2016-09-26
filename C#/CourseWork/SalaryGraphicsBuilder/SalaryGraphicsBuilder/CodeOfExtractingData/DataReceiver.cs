using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;
using SalaryGraphicsBuilder.Resources;
using System.Threading;
using SalaryGraphicsBuilder.DiagramCodeBehind;
using SalaryGraphicsBuilder.SerializationDeserializationXML;
using System.Collections.ObjectModel;
using SalaryGraphicsBuilder.EventsForMainWindowElements;

namespace SalaryGraphicsBuilder.CodeOfExtractingData
{
    static class DataReceiver
    {
        public static string PathToProfessionSalaryInfoFolder = string.Empty;
        private static bool DownloadMustBeStoped = false;
        private static int TimeoutForRequests = 1500;
        public static int PercentagesForGatheringInfo = 0;
        static string URLtoJobsTutBy = "https://jobs.tut.by/";
        static public ObservableCollection<string> ListOfProfessionNames = new ObservableCollection<string>();
        static public ObservableCollection<Profession> ListOfInfoAboutProfessions = new ObservableCollection<Profession>();

        private static string DownloadHtmlPage(string PageUrl)
        {
            Thread.Sleep(TimeoutForRequests);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(PageUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }
                string PageWithListOfCatalogsAsString = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                return PageWithListOfCatalogsAsString;
            }
            else
            {
                MessageBox.Show(Texts.UsualLoadHtmlError + PageUrl, Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().VisibilityForProgressBarForLoadingOfProfessions = Visibility.Collapsed;
                DownloadMustBeStoped = true;
                return string.Empty;
            }
        }

        public static void GetDataForSalaryGraphics()
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { ListOfProfessionNames.Clear(); }));

            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().VisibilityForProgressBarInfoLabel = Visibility.Visible;
            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().ContentForProgressBarInfoLabel = Texts.FirstContentToShowForLabelInfoForProgressBar;
            ListOfInfoAboutProfessions.Clear();

            DownloadMustBeStoped = false;
            string PageWithListOfCatalogsAsString = DownloadHtmlPage(URLtoJobsTutBy);
            if (DownloadMustBeStoped)
            {
                MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().VisibilityForProgressBarForLoadingOfProfessions = Visibility.Collapsed;
                MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().VisibilityForProgressBarInfoLabel = Visibility.Collapsed;
                return;
            }
            List<string> AllReferencesToCatalogs = GetAllCatalogsReferences(PageWithListOfCatalogsAsString);
            double IncrementForProgressBarValue = MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().MaximumForProgressBarForLoadingOfProfessions / (double)AllReferencesToCatalogs.Count;
            double CurrentValueForProgressBar = MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().MinimumForProgressBarForLoadingOfProfessions;
            foreach (string CurrentReferenceToCatalog in AllReferencesToCatalogs)
            {
                GetInfoAboutSalary(DownloadHtmlPage(CurrentReferenceToCatalog), CurrentReferenceToCatalog);
                //MessageBox.Show(CurrentReferenceToCatalog + " загружен!");

                if (DownloadMustBeStoped)
                {
                    MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().VisibilityForProgressBarForLoadingOfProfessions = Visibility.Collapsed;
                    MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().VisibilityForProgressBarInfoLabel = Visibility.Collapsed;
                    return;
                }
                CurrentValueForProgressBar += IncrementForProgressBarValue;
                if (CurrentValueForProgressBar > MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().MaximumForProgressBarForLoadingOfProfessions)
                {
                    CurrentValueForProgressBar = MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().MaximumForProgressBarForLoadingOfProfessions;
                }
                MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().ValueForProgressBarForLoadingOfProfessions = CurrentValueForProgressBar;
            }

            Application.Current.Dispatcher.Invoke(new Action(() => { MessageBox.Show(Texts.MessageBoxLoadingIsComplete); }));
                   
            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().VisibilityForProgressBarForLoadingOfProfessions = Visibility.Collapsed;
            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().VisibilityForProgressBarInfoLabel = Visibility.Collapsed;
            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().UIControlsAreEnabled = true;
            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().GatherInfoAboutSalariesButtonIsEnabled = true;
        }

        private static List<string> GetAllCatalogsReferences(string JobsTutByPageAsString)
        {
            List<string> AllReferencesToCatalogs = new List<string>();
            Regex RegexForCatalogReference = new Regex("href=\"/catalog/[^\"]+\"");
            string BufferForMatchValue = string.Empty;

            foreach (Match CurrentReference in RegexForCatalogReference.Matches(JobsTutByPageAsString))
            {
                BufferForMatchValue = CurrentReference.Value;
                BufferForMatchValue = BufferForMatchValue.Replace("href=\"","");
                BufferForMatchValue = BufferForMatchValue.Replace("\"", "");

                AllReferencesToCatalogs.Add(URLtoJobsTutBy + BufferForMatchValue.Remove(0, 1));
            }

            return AllReferencesToCatalogs;
        }

        private static void GetInfoAboutSalary(string FirstHtmlPageOfCatalog, string PureReferenceToCatalog)
        {
            string Profession = PureReferenceToCatalog.Split('/').Last();
            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().ContentForProgressBarInfoLabel = Texts.ContentStartForLabelInfoForProgressBar + " " + Profession + "...";
            if (File.Exists(PathToProfessionSalaryInfoFolder + "\\" + Profession + ".xml"))
            {
                ListOfInfoAboutProfessions.Add(XMLSerializerAndDeserializer.DeserializeProfession(PathToProfessionSalaryInfoFolder + "\\" + Profession + ".xml"));
                Application.Current.Dispatcher.Invoke(new Action(() => { ListOfProfessionNames.Add(ListOfInfoAboutProfessions.Last().ProfessionName); }));
            }
            else
            {
                List<string> ReferencesToPagesOfCatalog = new List<string>();
                string BufferForCurrentHtmlPageOfCatalog = FirstHtmlPageOfCatalog;
                string BufferForLastPageReference = string.Empty;
                //Собираем ссылки на все страницы каталога в ReferencesToPagesOfCatalog
                while (true)
                {
                    GatherReferencesToPagesOfCatalog(BufferForCurrentHtmlPageOfCatalog, Profession, ReferencesToPagesOfCatalog, PureReferenceToCatalog);
                    if (ReferencesToPagesOfCatalog.Count == 0)
                    {
                        break;
                    }
                    if (BufferForLastPageReference != ReferencesToPagesOfCatalog.Last())
                    {
                        BufferForLastPageReference = ReferencesToPagesOfCatalog.Last();
                        BufferForCurrentHtmlPageOfCatalog = DownloadHtmlPage(BufferForLastPageReference);
                    }
                    else
                    {
                        break;
                    }
                    if (DownloadMustBeStoped)
                    {
                        return;
                    }
                }

                ListOfInfoAboutProfessions.Add(new Profession(Profession));
                Application.Current.Dispatcher.Invoke(new Action(() => { ListOfProfessionNames.Add(ListOfInfoAboutProfessions.Last().ProfessionName); }));

                GetSalaryValues(PureReferenceToCatalog);
                if (ReferencesToPagesOfCatalog.Count > 0)
                {
                    foreach (string CurrentReferenceToPageOfCatalog in ReferencesToPagesOfCatalog)
                    {
                        GetSalaryValues(CurrentReferenceToPageOfCatalog);
                        if (DownloadMustBeStoped)
                        {
                            return;
                        }
                    }
                }
                //Сохраняем текущую профессию  в виде XML файл
                string CurrentProfessionXML = string.Empty;
                CurrentProfessionXML = XMLSerializerAndDeserializer.SerializeProfession(ListOfInfoAboutProfessions.Last());
                
                using (FileStream FileStreamToXmlFileForCurrentProfession = File.Create(PathToProfessionSalaryInfoFolder + "\\" + Profession + ".xml"))
                {
                    Byte[] ProfessionSalaryInfo = new UTF8Encoding(true).GetBytes(CurrentProfessionXML);
                    // Add some information to the file.
                    FileStreamToXmlFileForCurrentProfession.Write(ProfessionSalaryInfo, 0, ProfessionSalaryInfo.Length);
                }
            } 
        }

        public static void GetSalaryValues(string ReferenceToPageOfCatalog)
        {
            string CurrentPageOfCatalog = DownloadHtmlPage(ReferenceToPageOfCatalog);
            if (DownloadMustBeStoped)
            {
                return;
            }
            Regex RegexForCurrencyAndSalaryMetaInfo = new Regex("<meta itemprop=\"salaryCurrency\" content=\"[^.]+<meta itemprop=\"baseSalary\" content=\"[0-9]+\">");
            Regex RegexForCurrency = new Regex("[A-Z]{3}");
            Regex RegexForSalary = new Regex("[0-9]+");
            string BufferForMatchValue = string.Empty;
            string BufferForCurrency = string.Empty;
            string BufferForSalary = string.Empty;

            foreach (Match CurrentReference in RegexForCurrencyAndSalaryMetaInfo.Matches(CurrentPageOfCatalog))
            {
                BufferForMatchValue = CurrentReference.Value;
                BufferForCurrency = RegexForCurrency.Match(BufferForMatchValue).Value;
                BufferForSalary = RegexForSalary.Match(BufferForMatchValue).Value;
                ListOfInfoAboutProfessions.Last().ListOfInfoAboutOffers.Add(new SalaryInfo(BufferForCurrency, Double.Parse(BufferForSalary)));                        
            }
        }

        private static void GatherReferencesToPagesOfCatalog(string PageOfCatalog, string Profession, List<string> ListOfReferencesToPagesOfCatalog, string PureReferenceToCatalog)
        {
            Regex RegexForReferenceToCatalogPage = new Regex("href=\"/catalog/[^\"]+page-[0-9]+\"");
            string BufferForMatchValue = string.Empty;
            string BuferForConstructedReference = string.Empty;

            foreach (Match CurrentReferenceToPageOfCatalog in RegexForReferenceToCatalogPage.Matches(PageOfCatalog))
            {
                BufferForMatchValue = CurrentReferenceToPageOfCatalog.Value;
                BufferForMatchValue = BufferForMatchValue.Replace("href=\"", "");
                BufferForMatchValue = BufferForMatchValue.Replace("\"", "");
                BufferForMatchValue = BufferForMatchValue.Replace("/catalog/" + Profession, "");
                BuferForConstructedReference = PureReferenceToCatalog + BufferForMatchValue;
                //Проверяем на двойники
                if (ListOfReferencesToPagesOfCatalog.Where(Reference => Reference == BuferForConstructedReference).FirstOrDefault() == null)
                {
                    ListOfReferencesToPagesOfCatalog.Add(BuferForConstructedReference);
                }
            }
        }
    }
}
