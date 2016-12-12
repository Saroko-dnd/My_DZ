namespace AzureFirstTry
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SarokoDBModel : DbContext
    {
        public SarokoDBModel()
            : base("name=ConnectionToSaliamisan")
        {
        }

        public virtual DbSet<Fish> Fishes { get; set; }
        public virtual DbSet<Sea> Seas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sea>()
                 .HasMany(u => u.Fishes)
                 .WithMany(l => l.Seas)
                 .Map(ul =>
                 {
                     ul.MapLeftKey("FishId");
                     ul.MapRightKey("SeaId");
                     ul.ToTable("FishesAndSeas");
                 });
        }
    }
}
