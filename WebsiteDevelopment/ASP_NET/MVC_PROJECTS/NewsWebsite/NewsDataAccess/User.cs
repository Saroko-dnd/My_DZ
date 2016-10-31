using NewsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsDataAccess
{
    public class User
    {
        public uint UserID{ get; set; }
        public string UserName { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
