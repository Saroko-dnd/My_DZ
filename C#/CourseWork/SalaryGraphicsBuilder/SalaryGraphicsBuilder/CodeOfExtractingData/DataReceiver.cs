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

namespace SalaryGraphicsBuilder.CodeOfExtractingData
{
    static class DataReceiver
    {
        private static int TimeoutForRequests = 1500;
        public static int PercentagesForGatheringInfo = 0;
        static string URLtoJobsTutBy = "https://jobs.tut.by/";
        static private List<Profession> ListOfInfoAboutProfessions = new List<Profession>();

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
                return string.Empty;
            }
        }

        public static void GetDataForSalaryGraphics()
        {
            string PageWithListOfCatalogsAsString = DownloadHtmlPage(URLtoJobsTutBy);
            List<string> AllReferencesToCatalogs = GetAllCatalogsReferences(PageWithListOfCatalogsAsString);
            foreach (string CurrentReferenceToCatalog in AllReferencesToCatalogs)
            {
                GetInfoAboutSalary(DownloadHtmlPage(CurrentReferenceToCatalog), CurrentReferenceToCatalog);
            }
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
            List<string> ReferencesToPagesOfCatalog = new List<string>();
            string Profession = PureReferenceToCatalog.Split('/').Last();
            string BufferForCurrentHtmlPageOfCatalog = FirstHtmlPageOfCatalog;
            string BufferForLastPageReference = string.Empty;
            //Собираем ссылки на все страницы каталога в ReferencesToPagesOfCatalog
            while (true)
            {
                GatherReferencesToPagesOfCatalog(BufferForCurrentHtmlPageOfCatalog, Profession, ReferencesToPagesOfCatalog, PureReferenceToCatalog);
                if (BufferForLastPageReference != ReferencesToPagesOfCatalog.Last())
                {
                    BufferForLastPageReference = ReferencesToPagesOfCatalog.Last();
                    BufferForCurrentHtmlPageOfCatalog = DownloadHtmlPage(BufferForLastPageReference);
                }
                else
                {
                    break;
                }
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
