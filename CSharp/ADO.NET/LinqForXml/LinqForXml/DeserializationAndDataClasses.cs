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
using System.Reflection;
using System.ComponentModel;

namespace LinqForXml
{

    public class ColumnNameAttribute : System.Attribute
    {
        public ColumnNameAttribute(string Name) { this.Name = Name; }
        public string Name { get; set; }

        public static void dgPrimaryGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var desc = e.PropertyDescriptor as PropertyDescriptor;
            var att = desc.Attributes[typeof(ColumnNameAttribute)] as ColumnNameAttribute;
            if (att != null)
            {
                e.Column.Header = att.Name;
            }
        }
    }
    public static class XmlDataLoader
    {
        public static List<CD> CdList = new List<CD>();
        public static List<PRODUCER> ProducerList = new List<PRODUCER>();

        public static void ExecuteFirstQuery(DataGrid ResultDataGrid)
        {
            ResultDataGrid.ItemsSource = (from ourcdlist in XmlDataLoader.CdList
                                              where ourcdlist.YEAR.Year > 1991
                                              select new { ourcdlist.ARTIST }).ToList();
            ResultDataGrid.ItemsSource = XmlDataLoader.CdList.Where(ResYear => ResYear.YEAR.Year > 1991).
                Select(res => new { ARTIST = res.ARTIST }).ToList();
        }
        

        public static void ExecuteSecondQuery(DataGrid ResultDataGrid)
        {
            ResultDataGrid.ItemsSource = (from ourcdlist in XmlDataLoader.CdList
                                          select new { COUNTRY = ourcdlist.COUNTRY }).Distinct().ToList();
            ResultDataGrid.ItemsSource = XmlDataLoader.CdList.Select(res => new { COUNTRY = res.COUNTRY }).Distinct().ToList();
        }

        public static void ExecuteThirdQuery(DataGrid ResultDataGrid)
        {
            ResultDataGrid.ItemsSource = (from ourcdlist in XmlDataLoader.CdList.OrderBy(res => res.YEAR)
                                          where ourcdlist.COUNTRY == "USA"
                                          select new { TITLE = ourcdlist.TITLE, YEAR = ourcdlist.YEAR.Year}).
                                          ToList();
            ResultDataGrid.ItemsSource = XmlDataLoader.CdList.OrderBy(ResYear => ResYear.YEAR.Year).
                Where(CountryName => CountryName.COUNTRY == "USA").Select(Result => new { TITLE = Result.TITLE,
                                                                                          YEAR = Result.YEAR.Year
                                                                                        }).ToList();
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
            ResultDataGrid.ItemsSource = (from OurProducerList in XmlDataLoader.ProducerList.OrderByDescending(r => r.FEE).Take(1)    
                                          join ourcdlist in XmlDataLoader.CdList  on
                                          OurProducerList.ID equals ourcdlist.PRODUCER
                                          select new
                                          {
                                              ProducerName = OurProducerList.NAME,
                                              TITLE = ourcdlist.TITLE
                                          }).ToList();
        }

        public static void ExecuteSeventhQuery(DataGrid ResultDataGrid)
        {
            ResultDataGrid.ItemsSource = (from OurProducerList in XmlDataLoader.ProducerList
                                          join ourcdlist in XmlDataLoader.CdList on
                                          OurProducerList.ID equals ourcdlist.PRODUCER
                                          where (ourcdlist.YEAR.Year >= 1988 && ourcdlist.YEAR.Year <= 1990)
                                          group ourcdlist.TITLE by OurProducerList.NAME into ResultGroupBy
                                          select new
                                          {
                                              ProducerName = ResultGroupBy.Key,
                                              AmountOfAlbums = ResultGroupBy.Count()
                                          }).ToList();
        }

        public static void ExecuteEighthQuery(DataGrid ResultDataGrid)
        {
            ResultDataGrid.ItemsSource = (from OurProducerList in XmlDataLoader.ProducerList.OrderByDescending(x => x.DATE.Year).Take(1)
                                          select new
                                          {
                                              ProducerName = OurProducerList.NAME,
                                          }).ToList();
        }

        public static void ExecuteNinthQuery(DataGrid ResultDataGrid)
        {
            ResultDataGrid.ItemsSource = (from ourcdlist in XmlDataLoader.CdList.OrderBy(x => x.PRICE).Take(1)
                                          join OurProducerList in XmlDataLoader.ProducerList
                                          on ourcdlist.PRODUCER equals OurProducerList.ID
                                          select new
                                          {
                                              ProducerName = OurProducerList.NAME,
                                              ARTIST = ourcdlist.ARTIST,
                                              TITLE = ourcdlist.TITLE
                                          }).Take(1);
        }

        public static void ExecuteTenthQuery(DataGrid ResultDataGrid)
        {
            ResultDataGrid.ItemsSource = (from ourcdlist in XmlDataLoader.CdList.OrderBy(x => x.YEAR.Year).
                                              ThenBy(y => y.PRICE).ThenBy(z => z.TITLE)
                                          select ourcdlist).ToList();
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
        [ColumnNameAttribute("Название")]
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
        [ColumnName("Исполнитель")]
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
        [ColumnName("Страна")]
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
        [ColumnName("Компания")]
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
        [ColumnName("Цена")]
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
        [ColumnName("Год")]
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
        [ColumnName("ID продъюсера")]
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

        [ColumnName("ID продъюсера")]
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
        [ColumnName("Имя")]
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
        [ColumnName("Дата награждения")]
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
        [ColumnName("Сумма награды")]
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
