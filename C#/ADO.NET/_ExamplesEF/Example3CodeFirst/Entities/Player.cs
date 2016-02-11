using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1CodeFirst.Entities
{
    // Класс сущности 
    public class Player
    {    
        public int Id { get; set; }
     
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
      
        // public DateTime BirthDay { get; set; }
      
    }
}
