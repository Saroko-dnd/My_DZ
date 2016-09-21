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

namespace SalaryGraphicsBuilder.CodeOfExtractingData
{
    static class DataReceiver
    {
        public static int PercentagesForGatheringInfo = 0;
        static string URLtoJobsTutBy = "https://jobs.tut.by/";
        static private List<Profession> ListOfInfoAboutProfessions = new List<Profession>();

        public static void GetPageWithListOfCatalogs()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URLtoJobsTutBy);
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

                GetAllCatalogsReferences(PageWithListOfCatalogsAsString);
            }
            else
            {
                MessageBox.Show(Texts.MainLoadHtmlError, Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static void GetAllCatalogsReferences(string JobsTutByPageAsString)
        {
            List<string> AllReferencesToCatalogs = new List<string>();
            Regex RegexForCatalogReference = new Regex("href=\"/catalog/[^\"]+\"");
            string BufferForMatchValue = string.Empty;

            foreach (Match CurrentReference in RegexForCatalogReference.Matches(JobsTutByPageAsString))
            {
                BufferForMatchValue = CurrentReference.Value;
                BufferForMatchValue = BufferForMatchValue.Replace("href=\"","");
                BufferForMatchValue = BufferForMatchValue.Replace("\"", "");

                AllReferencesToCatalogs.Add(BufferForMatchValue);
            }

            foreach (string CurrentReference in AllReferencesToCatalogs)
            {
                GetInfoAboutSalaries(URLtoJobsTutBy + CurrentReference.Remove(0,1));
            }
        }

        private static void GetInfoAboutSalaries(string CurrentCatalogReference)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URLtoJobsTutBy);
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

                GetAllCatalogsReferences(PageWithListOfCatalogsAsString);
            }
            else
            {
                MessageBox.Show(Texts.UsualLoadHtmlError + " " + CurrentCatalogReference, Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
