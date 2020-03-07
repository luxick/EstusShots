using EstusShots.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EstusShots.Server.Services
{
    public class EstusShotsContext : DbContext
    {
        public EstusShotsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Season> Seasons { get; set; } = default!;
        public DbSet<Episode> Episodes { get; set; } = default!;
        public DbSet<Player> Players { get; set; } = default!;
        public DbSet<Drink> Drinks { get; set; } = default!;
        public DbSet<Enemy> Enemies { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Season>().ToTable(nameof(Season));
            modelBuilder.Entity<Episode>().ToTable(nameof(Episode));
            modelBuilder.Entity<Enemy>().ToTable(nameof(Enemy));
            modelBuilder.Entity<Drink>().ToTable(nameof(Drink));
            modelBuilder.Entity<Player>().ToTable(nameof(Player));

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

            
            modelBuilder.Entity<SeasonEnemies>()
                .HasKey(t => new {t.SeasonId, t.EnemyId});
            
            modelBuilder.Entity<SeasonEnemies>()
                .HasOne(pt => pt.Season)
                .WithMany(p => p.SeasonEnemies)
                .HasForeignKey(pt => pt.SeasonId);

            modelBuilder.Entity<SeasonEnemies>()
                .HasOne(pt => pt.Enemy)
                .WithMany(t => t.SeasonEnemies)
                .HasForeignKey(pt => pt.EnemyId);
        }
    }
}