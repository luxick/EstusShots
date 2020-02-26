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
    }
}