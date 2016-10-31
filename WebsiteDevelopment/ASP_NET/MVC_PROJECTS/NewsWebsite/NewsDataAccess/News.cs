using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntities
{
    public class News
    {
        public uint NewsID { get; set; }
        public string Header{ get; set; }
        public string Body{ get; set; }
        public DateTime Date{ get; set; }
        public bool HotNews { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
