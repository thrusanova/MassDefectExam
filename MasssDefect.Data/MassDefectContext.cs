namespace MasssDefect.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MassDefectContext : DbContext
    {

        public MassDefectContext()
            : base("name=MassDefectContext")
        {
        }

        public virtual DbSet<Star> Stars { get; set; }

        public virtual DbSet<SolarSystem> SolarSystems { get; set; }

        public virtual DbSet<Planet> PLanets { get; set; }

        public virtual DbSet<Anomaly> Anomalies { get; set; }

        public virtual DbSet<Person> Persons { get; set; }

    

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
            modelBuilder.Entity<Anomaly>()
                .HasMany(a => a.Victims)
                .WithMany(v => v.Anomalies)
                .Map(cs =>
                {
                    cs.MapLeftKey("AnomalyId");
                    cs.MapRightKey("PersonId");
                    cs.ToTable("AnomalyVictims");
                });
            modelBuilder.Entity<Anomaly>()
              .HasOptional(anom => anom.OriginPlanet)
             .WithMany(planet => planet.OriginOfAnomalies)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
               .HasOptional(anom => anom.TeleportPlanet)
             .WithMany(planet => planet.TeleportOfAnomalies)
            .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
    }
}


}