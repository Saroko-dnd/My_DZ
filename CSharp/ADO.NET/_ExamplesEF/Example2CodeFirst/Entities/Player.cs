using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1CodeFirst.Entities
{
    // Класс сущности 
    public class Player
    {     // ключевое поле (можно сипользовать PlayerId)  
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        // навигационное свойство  
        public virtual Team Team { get; set; }
    }
}
