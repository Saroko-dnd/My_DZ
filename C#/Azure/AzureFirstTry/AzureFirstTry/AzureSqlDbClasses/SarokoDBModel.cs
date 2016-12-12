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
                 .HasMany(SeaEntity => SeaEntity.Fishes)
                 .WithMany(FishEntity => FishEntity.Seas)
                 .Map(ManyToManyConfig =>
                 {
                     ManyToManyConfig.MapLeftKey("FishId");
                     ManyToManyConfig.MapRightKey("SeaId");
                     ManyToManyConfig.ToTable("FishesAndSeas");
                 });
        }
    }
}
