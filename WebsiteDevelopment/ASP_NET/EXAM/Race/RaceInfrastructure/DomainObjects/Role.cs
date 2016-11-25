using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure.DomainObjects
{
    public class Role
    {
        public Guid RoleID { get; set; }
        public string Name { get; set; }

        private ICollection<User> users;

        public virtual ICollection<User> Users
        {
            get { return users ?? (users = new List<User>()); }
            set { users = value; }
        }
    }
}
