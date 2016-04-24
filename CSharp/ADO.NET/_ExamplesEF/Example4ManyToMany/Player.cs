using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4ManyToMany
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
