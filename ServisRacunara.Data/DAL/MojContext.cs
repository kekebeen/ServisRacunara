using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ServisRacunara.Data.MODELS;

namespace ServisRacunara.Data.DAL
{
    public class MojContext : DbContext
    {
        public MojContext()
           : base("ConnectionString")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Uposlenik> Uposlenici { get; set; }
        public DbSet<Status> Statusi { get; set; }

        public DbSet<ServisniNalog> ServisniNalozi { get; set; }
        public DbSet<ServisPreuzeo> ServisPreuzimanja { get; set; }

        public DbSet<Kvar> Kvarovi { get; set; }

        public DbSet<Popravka> Popravke { get; set; }

        public DbSet<Rjesenje> Rjesenja { get; set; }

        public DbSet<ZahtjevZaServis> ZahtjeviZaServis { get; set; }

        public DbSet<Isplata> Isplate { get; set; }

        public DbSet<Plata> Plate { get; set; }

        public DbSet<Proizvod> Proizvodi { get; set; }

        public DbSet<Racun> Racuni { get; set; }

        public DbSet<Racunar> Racunari { get; set; }

        public DbSet<StavkaRacuna> StavkeRacuna { get; set; }
        
        public DbSet<KvarRacunarNalog> KvarRacunarNalog  { get; set; }

    }
}
