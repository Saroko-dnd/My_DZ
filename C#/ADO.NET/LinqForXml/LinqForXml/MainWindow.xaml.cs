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
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Collections.ObjectModel;

namespace LinqForXml
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<CD> CdList = new List<CD>();
        public static List<PRODUCER> ProducerList = new List<PRODUCER>();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                XmlSerializer SerializerForCd = new XmlSerializer(typeof(List<CD>));
                FileStream FilestreamForCDXml = new FileStream(MyResourses.Texts.XmlCdFileName, FileMode.Open);
                XmlReader ReaderForCdXml = XmlReader.Create(FilestreamForCDXml);
                CdList = (List<CD>)SerializerForCd.Deserialize(ReaderForCdXml);
            }
            catch (Exception CurExc)
            {
                MessageBox.Show(CurExc.Message,MyResourses.Texts.Error,MessageBoxButton.OK,MessageBoxImage.Error);
            }

            try
            {
                XmlSerializer SerializerForProducers = new XmlSerializer(typeof(List<PRODUCER>));
                FileStream FilestreamForProducersXml = new FileStream(MyResourses.Texts.XmlProducersFileName, FileMode.Open);
                XmlReader ReaderForProducersXml = XmlReader.Create(FilestreamForProducersXml);
                ProducerList = (List<PRODUCER>)SerializerForProducers.Deserialize(ReaderForProducersXml);
            }
            catch (Exception CurExc)
            {
                MessageBox.Show(CurExc.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //MessageBox.Show(CdList[0].YEAR.ToString(), MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            List<CD> BufForFirstQuery = (from ourcdlist in CdList where ourcdlist.YEAR.Year > 1991 select ourcdlist.PRODUCER).ToList();
            FirstQueryDataGrid.DataContext = ProducersAfterCCCP;
        }
    }
}
