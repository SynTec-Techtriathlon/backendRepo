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
    }
}