using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JournalV.Models;

namespace JournalV.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<Procedure> Procedures { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка связей
            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Pet)
                .WithMany()
                .HasForeignKey(v => v.PetId);

            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Veterinarian)
                .WithMany(v => v.Visits)
                .HasForeignKey(v => v.VeterinarianId);

            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Procedure)
                .WithMany(p => p.Visits)
                .HasForeignKey(v => v.ProcedureId);

            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Pets)
                .HasForeignKey(p => p.OwnerId);
        }
    }
}
