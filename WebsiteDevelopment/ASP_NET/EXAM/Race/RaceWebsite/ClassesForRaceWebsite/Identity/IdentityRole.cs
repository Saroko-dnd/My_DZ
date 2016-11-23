using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceWebsite.ClassesForRaceWebsite.Identity
{
    public class IdentityRole : IRole<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public IdentityRole()
        {
            this.Id = Guid.NewGuid();
        }

        public IdentityRole(string NewName) : this()
        {
            this.Name = NewName;
        }

        public IdentityRole(string NewName, Guid NewId)
        {
            this.Name = NewName;
            this.Id = NewId;
        }
    }
}