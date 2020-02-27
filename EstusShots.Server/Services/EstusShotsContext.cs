using EstusShots.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EstusShots.Server.Services
{
    public class EstusShotsContext : DbContext
    {
        public EstusShotsContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Season> Seasons { get; set; } = default!;
        public DbSet<Episode> Episodes { get; set; } = default!;
    }
}