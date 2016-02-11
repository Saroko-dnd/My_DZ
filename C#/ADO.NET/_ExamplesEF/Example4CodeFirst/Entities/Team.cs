using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1CodeFirst.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // навигационное свойство  
        public virtual ICollection<Player> Players { get; set; }
    }

}
