using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace school
{
    public class date
    {
        string current_date;
        [XmlIgnore]
        public int day;
        [XmlIgnore]
        public int month;
        [XmlIgnore]
        public int year;

        public date ()
        {
            day = DateTime.Today.Day;
            month = DateTime.Today.Month;
            year = DateTime.Today.Year;
            current_date = year.ToString() + "-" + month.ToString() + "-" + day.ToString();
        }

        public date(int new_day, int new_month, int new_year)
        {
            day = new_day;
            month = new_month;
            year = new_year;
            current_date = year.ToString() + "-" + month.ToString() + "-" + day.ToString();
        }
    }
}
