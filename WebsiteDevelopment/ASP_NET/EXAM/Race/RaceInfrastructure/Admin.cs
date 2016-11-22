using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public class Admin
    {
        public long AdminID { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
    }
}
