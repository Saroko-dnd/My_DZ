using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Examples2OneToOne
{
   public class PlayerInfo
    {
        [ForeignKey("Player")]
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }    
        public virtual Player Player { get; set; }
    }
}
