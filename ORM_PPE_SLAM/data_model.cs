using Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ORM_PPE_SLAM
{
    public partial class data_model : DbContext
    {
        public data_model()
            : base("name=data_model")
        {
        }

        public virtual DbSet<departement> departements { get; set; }
        public virtual DbSet<medecin> medecins { get; set; }
        public virtual DbSet<specialite> specialites { get; set; }
        public virtual DbSet<user> user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<departement>()
               .Property(e => e.NomRegion)
               .IsUnicode(false);

            modelBuilder.Entity<departement>()
                .Property(e => e.NomDepartement)
                .IsUnicode(false);

            modelBuilder.Entity<departement>()
                .HasMany(e => e.Medecins)
                .WithRequired(e => e.Departement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<medecin>()
                .Property(e => e.NomMedecin)
                .IsUnicode(false);

            modelBuilder.Entity<medecin>()
                .Property(e => e.PrenomMedecin)
                .IsUnicode(false);

            modelBuilder.Entity<medecin>()
                .Property(e => e.AdresseMedecin)
                .IsUnicode(false);

            modelBuilder.Entity<medecin>()
                .Property(e => e.TelephoneMedecin)
                .IsUnicode(false);

            modelBuilder.Entity<medecin>()
                .Property(e => e.SpecialiteMedecin)
                .IsUnicode(false);

            modelBuilder.Entity<specialite>()
                .Property(e => e.LbSpecialite)
                .IsUnicode(false);
        }
    }
}
