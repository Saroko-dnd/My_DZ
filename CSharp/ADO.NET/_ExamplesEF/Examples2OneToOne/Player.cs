using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples2OneToOne
{
   public  class Player
    {
        // ключевое поле (можно иcпользовать PlayerId)  
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }

        // Создаем вторичный ключ и связываем таблицы один к одному
        // Обратите внимание на virtual -> Lazy Load !!!
        public virtual PlayerInfo PlayerInfo { get; set; }
    }
}
