using Microsoft.EntityFrameworkCore;
using projet_cabinet.Models;

namespace projet_cabinet.Data
{
    public class UsersDBContext : DbContext
    {
        public UsersDBContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Patient>("Patient")
                .HasValue<Infirmier>("Infirmier")
                .HasValue<Medecin>("Medecin");

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Dossier)
                .WithOne(d => d.Patient)
                .HasForeignKey<Dossier>(d => d.PatientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RDV>()
                .HasOne(r => r.Medecin)
                .WithMany(m => m.RDVs)
                .HasForeignKey(r => r.MedecinID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RDV>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.RDVs)
                .HasForeignKey(r => r.PatientID)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Dossier)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.DossierID);

            modelBuilder.Entity<Dossier>()
                .HasOne(d => d.Medecin)
                .WithMany(m => m.Dossiers)
                .HasForeignKey(d => d.MedecinID)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Infirmier> Infirmiers { get; set; }
        public DbSet<Medecin> Medecins { get; set; }
        public DbSet<RDV> RDVs { get; set; }
        public DbSet<Dossier> Dossiers { get; set; }
        public DbSet<Examen> Examens { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
    }
}
