using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Windows;
using System.Windows.Controls;

namespace LinqForXml
{
    public static class XmlDataLoader
    {
        public static List<CD> CdList = new List<CD>();
        public static List<PRODUCER> ProducerList = new List<PRODUCER>();

        public static void ExecuteFirstQuery(DataGrid ResultDataGrid)
        {
            ResultDataGrid.ItemsSource = (from ourcdlist in XmlDataLoader.CdList
                                              where ourcdlist.YEAR.Year > 1991
                                              select new { PRODUCER = ourcdlist.PRODUCER }).ToList();
        }

        public static void ExecuteSecondQuery(DataGrid ResultDataGrid)
        {
            ResultDataGrid.ItemsSource = (from ourcdlist in XmlDataLoader.CdList
                                          select new { COUNTRY = ourcdlist.COUNTRY }).Distinct().ToList();
        }

        public static void ExecuteThirdQuery(DataGrid ResultDataGrid)
        {
            ResultDataGrid.ItemsSource = (from ourcdlist in XmlDataLoader.CdList.OrderBy(res => res.YEAR)
                                          where ourcdlist.COUNTRY == "USA"
                                          select new { TITLE = ourcdlist.TITLE }).
                                          ToList();
        }

        public static void ExecuteFourthQuery(DataGrid ResultDataGrid)
        {
            ResultDataGrid.ItemsSource = (from ourcdlist in XmlDataLoader.CdList
                                         group ourcdlist.PRICE by ourcdlist.COUNTRY into GroupByResult
                                         select new { COUNTRY = GroupByResult.Key, SumPrice = GroupByResult.
                                          Sum() }).
                                          ToList();
        }

        public static void ExecuteFifthQuery(DataGrid ResultDataGrid)
        {
            ResultDataGrid.ItemsSource = (from ourcdlist in XmlDataLoader.CdList.OrderBy(res => res.YEAR)
                                          group ourcdlist.TITLE by new { ourcdlist.YEAR.Year,
                                              ourcdlist.COMPANY} into GroupByResult
                                          select new
                                          {
                                              YEAR = GroupByResult.Key.Year,
                                              COMPANY = GroupByResult.Key.COMPANY,
                                              AmountOfAlbums = GroupByResult.Count()
                                          }).
                                          ToList();
        }

        public static void ExecuteSixthQuery(DataGrid ResultDataGrid)
        {
            var RowWithMaxFee = XmlDataLoader.ProducerList.OrderByDescending(x => x.FEE).First();
            ResultDataGrid.ItemsSource = (from ourcdlist in XmlDataLoader.CdList
                                          join OurProducerList in XmlDataLoader.ProducerList on 
                                          ourcdlist.PRODUCER equals OurProducerList.ID
                                          group ourcdlist by
                                             OurProducerList into GroupByResult
                                          select new
                                          {
                                              ProducerName = GroupByResult.Key.NAME,
                                              TITLE = GroupByResult.Key.COMPANY,
                                              AmountOfAlbums = GroupByResult.OrderByDescending(r => r.)
                                          }).
                                          ToList();
        }

        static XmlDataLoader()
        {
            try
            {
                XmlSerializer SerializerForCd = new XmlSerializer(typeof(List<CD>));
                FileStream FilestreamForCDXml = new FileStream(MyResourses.Texts.XmlCdFileName, FileMode.Open);
                XmlReader ReaderForCdXml = XmlReader.Create(FilestreamForCDXml);
                CdList = (List<CD>)SerializerForCd.Deserialize(ReaderForCdXml);
            }
            catch (Exception CurExc)
            {
                MessageBox.Show(CurExc.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
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
        }
    }

    public class CD
    {
        public string TITLE;
        public string ARTIST;
        public string COUNTRY;
        public string COMPANY;
        public float PRICE;
        public DateTime YEAR;
        public int PRODUCER;

        public string TITLE_
        {
            get
            {
                return TITLE;
            }
            set
            {
                TITLE = value;
            }
        }
        public string ARTIST_
        {
            get
            {
                return ARTIST;
            }
            set
            {
                ARTIST = value;
            }
        }

        public string COUNTRY_
        {
            get
            {
                return COUNTRY;
            }
            set
            {
                COUNTRY = value;
            }
        }
        public string COMPANY_
        {
            get
            {
                return COMPANY;
            }
            set
            {
                COMPANY = value;
            }
        }

        public float PRICE_
        {
            get
            {
                return PRICE;
            }
            set
            {
                PRICE = value;
            }
        }

        public DateTime YEAR_
        {
            get
            {
                return YEAR;
            }
            set
            {
                YEAR = value;
            }
        }
        public int PRODUCER_
        {
            get
            {
                return PRODUCER;
            }
            set
            {
                PRODUCER = value;
            }
        }
        public CD()
        {

        }
    }

    public class PRODUCER
    {
        public int ID;
        public string NAME;
        public DateTime DATE;
        public int FEE;

        public int ID_
        {
            get
            {
                return ID;
            }
            set
            {
                ID = value;
            }
        }

        public string NAME_
        {
            get
            {
                return NAME;
            }
            set
            {
                NAME = value;
            }
        }

        public DateTime DATE_
        {
            get
            {
                return DATE;
            }
            set
            {
                DATE = value;
            }
        }
        public int FEE_
        {
            get
            {
                return FEE;
            }
            set
            {
                FEE = value;
            }
        }

        public PRODUCER()
        {

        }
    }
}
