using NewsDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntities
{
    public class Comment
    {
        public string Message{ get; set; }
        public DateTime Date{ get; set; }
        public User Author{ get; set; }
    }
}
