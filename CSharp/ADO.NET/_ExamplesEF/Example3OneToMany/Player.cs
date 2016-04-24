using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example3OneToMany
{
    public class Player
    {
        // ключевое поле (можно иcпользовать PlayerId)  
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public virtual Team Team { get; set; }
    }
}
