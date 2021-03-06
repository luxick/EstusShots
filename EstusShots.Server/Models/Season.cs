using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EstusShots.Server.Models
{
    public class Season
    {
        public Guid SeasonId { get; set; }

        public int Number { get; set; }

        [MaxLength(50)] public string Game { get; set; } = default!;

        public string? Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public List<Episode> Episodes { get; set; } = default!;

        public ICollection<SeasonEnemy> SeasonEnemies { get; set; } = default!;
    }
}