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

namespace ProgramForDownloadingWebSites
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool ProgramShutDown = false;
        public bool ProgramBusy = false;
        public StringBuilder MainStringBuilder = new StringBuilder();
        public MainWindow()
        {
            InitializeComponent();
            Downloading("https://habrahabr.ru/");
        }

        public void Downloading(string CurrentURL)
        {
            try
            {
                HttpWebRequest MainRequest = (HttpWebRequest)HttpWebRequest.Create(CurrentURL);
                HttpWebResponse MainResponse = (HttpWebResponse)MainRequest.GetResponse();
                StreamReader MainStreamReader = new StreamReader(MainResponse.GetResponseStream(), true);
                if (File.Exists(CurrentURL.Replace("https://","").Replace("/","") + MyResourses.Texts.html))
                {
                    throw new Exception(MyResourses.Texts.SiteAlreadyWasDownload);
                }
                else
                {
                    using (FileStream StreamForMainPage = File.Create(CurrentURL.Replace("https://", "").Replace("/", "") + MyResourses.Texts.html, 4000))
                    {
                        Byte[] MainPageInBytes = new UTF8Encoding(true).GetBytes(MainStreamReader.ReadToEnd());
                        StreamForMainPage.Write(MainPageInBytes, 0, MainPageInBytes.Length);
                    }
                }
                //File.Create(FilePath,);
                List<string> MainListOReferences = new List<string>();
                CQ ObjectCQ = CQ.Create(MainStreamReader);
                foreach (IDomObject CurObject in ObjectCQ.Find("a"))
                {
                    MainListOReferences.Add(CurObject.GetAttribute("href"));
                }
                foreach (IDomObject CurObject in ObjectCQ.Find("img"))
                {
                    MainListOReferences.Add(CurObject.GetAttribute("src"));
                }
                int fff = 0;
            }
            catch (Exception CurrentException)
            {
                if (!ProgramShutDown)
                {
                    MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            finally
            {
                ProgramBusy = false;
            }
        }
        private void StartSiteDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ProgramBusy)
            {
                ProgramBusy = true;
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.ErrorProgramBusy, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
