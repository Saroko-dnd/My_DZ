using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }

        private ICollection<Role> roles;

        public virtual ICollection<Role> Roles
        {
            get { return roles ?? (roles = new List<Role>()); }
            set { roles = value; }
        }
    }
}
