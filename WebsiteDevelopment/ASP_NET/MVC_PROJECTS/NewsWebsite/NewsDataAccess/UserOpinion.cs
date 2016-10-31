using NewsDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntities
{
    class UserOpinion
    {
        public bool Like { get; set; }
        public User Author { get; set; }
    }
}
