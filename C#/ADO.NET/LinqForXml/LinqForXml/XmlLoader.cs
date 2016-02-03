using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LinqForXml
{
    public class CD
    {
        public string TITLE;
        public string ARTIST;
        public string COUNTRY;
        public string COMPANY;
        public string PRICE;
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

        public string PRICE_
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
        public string ID;
        public string NAME;
        public string DATE;
        public string FEE;

        public PRODUCER()
        {

        }
    }
}
