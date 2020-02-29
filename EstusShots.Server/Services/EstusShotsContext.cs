using EstusShots.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EstusShots.Server.Services
{
    public class EstusShotsContext : DbContext
    {
        public EstusShotsContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Season> Seasons { get; set; } = default!;
        public DbSet<Episode> Episodes { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Season>().ToTable(nameof(Season));
            modelBuilder.Entity<Episode>().ToTable(nameof(Episode));
        }
    }
}