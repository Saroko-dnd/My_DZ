using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceWebsite.ClassesForRaceWebsite.Identity
{
    public class IdentityUser : IUser<Guid>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        public IdentityUser()
        {
            this.Id = Guid.NewGuid();
        }

        public IdentityUser(string NewUserName) : this()
        {
            this.UserName = NewUserName;
        }
    }
}