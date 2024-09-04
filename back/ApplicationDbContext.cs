using Back.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Back
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ReportData> ReportData { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Spouse> Spouses { get; set; }
        public DbSet<History> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring composite keys
            modelBuilder.Entity<Applicant>()
                .HasKey(a => new { a.NIC, a.Nationality });

            modelBuilder.Entity<Spouse>()
                .HasKey(s => new { s.ApplicantNIC, s.ApplicantNationality, s.SpouseNIC });

            modelBuilder.Entity<Passport>()
                .HasKey(s => new { s.Country , s.Id});

            modelBuilder.Entity<Spouse>()
               .HasKey(s => new { s.ApplicantNIC, s.ApplicantNationality, s.SpouseNIC });

            // Configuring one-to-one relationships
            modelBuilder.Entity<Applicant>()
                .HasOne(a => a.Passport)
                .WithOne(p => p.Applicant)
                .HasForeignKey<Passport>(p => new { p.ApplicantNIC, p.ApplicantNationality });

            modelBuilder.Entity<Applicant>()
                .HasOne(a => a.Spouse)
                .WithOne(s => s.Applicant)
                .HasForeignKey<Spouse>(s => new { s.ApplicantNIC, s.ApplicantNationality });

            // Configuring one-to-many relationships
            modelBuilder.Entity<Applicant>()
                .HasMany(a => a.Applications)
                .WithOne(ap => ap.Applicant)
                .HasForeignKey(ap => new { ap.ApplicantNIC, ap.ApplicantNationality });

            modelBuilder.Entity<Applicant>()
                .HasMany(a => a.Histories)
                .WithOne(h => h.Applicant)
                .HasForeignKey(h => new { h.ApplicantNIC, h.ApplicantNationality });

            base.OnModelCreating(modelBuilder);
        }
    }
}