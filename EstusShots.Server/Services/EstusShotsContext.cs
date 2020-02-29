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
        public DbSet<Player> Players { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Season>().ToTable(nameof(Season));
            modelBuilder.Entity<Episode>().ToTable(nameof(Episode));

            modelBuilder.Entity<EpisodePlayers>()
                .HasKey(t => new {t.EpisodeId, t.PlayerId});

            modelBuilder.Entity<EpisodePlayers>()
                .HasOne(pt => pt.Episode)
                .WithMany(p => p.EpisodePlayers)
                .HasForeignKey(pt => pt.EpisodeId);

            modelBuilder.Entity<EpisodePlayers>()
                .HasOne(pt => pt.Player)
                .WithMany(t => t.EpisodePlayers)
                .HasForeignKey(pt => pt.PlayerId);
        }
    }
}