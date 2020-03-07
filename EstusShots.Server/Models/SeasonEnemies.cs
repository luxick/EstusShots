using System;

namespace EstusShots.Server.Models
{
    public class SeasonEnemies
    {
        public Guid SeasonId { get; set; } = default!;
        public Season Season { get; set; } = default!;

        public Guid EnemyId { get; set; } = default!;
        public Enemy Enemy { get; set; } = default!;
    }
}