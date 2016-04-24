using Example1CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1CodeFirst.Configuration
{
    public class TeamEntityTypeConfig : EntityTypeConfiguration<Team>
    {
        public TeamEntityTypeConfig()
        {
            ToTable("Team");
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired().HasMaxLength(20);
            Property(p => p.Name).IsRequired();

        }
    }
}
