using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntities
{
    public class News
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint NewsID { get; set; }
        public string Header{ get; set; }
        public string Body{ get; set; }
        public DateTime Date{ get; set; }
        public bool HotNews { get; set; }

        public News()
        {

        }

        public News(DateTime NewsDate, string NewsHeader, string NewsBody, bool IsThisNewsHot)
        {
            Header = NewsHeader;
            Body = NewsBody;
            Date = NewsDate;
            HotNews = IsThisNewsHot;
        }
    }
}
