using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Для использования DataAnnotations
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Example1CodeFirst.Entities
{
    // В базе данных таблица будет называться 
    [Table("TeamPlayers")]
    public class Player
    {
        // ключевое поле в базе данных
        [Key]
        public int Id { get; set; }

        // обязательное поле, длина min/max 
        [Required, MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        // обязательное поле, имя столбца в базе данных 
        [Required, Column("Post")]
        public string Position { get; set; }
        
        // поле не будет создано в базе данных
        [NotMapped]
        public int Age { get; set; }
        // навигационное свойство  
        public virtual Team Team { get; set; }
    }
}
