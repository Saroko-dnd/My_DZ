using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Для использования 
using System.Data.Entity.ModelConfiguration;
using Example1CodeFirst.Entities;

namespace Example1CodeFirst.Configuration
{
    public class PlayerEntityTypeConfig : EntityTypeConfiguration<Player>
    {
        public PlayerEntityTypeConfig()
        {
            ToTable("TeamPlayers");
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired().HasMaxLength(20);
            Property(p => p.Position).IsRequired().HasColumnName("Post");

        }
    }
}
