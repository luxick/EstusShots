using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EstusShots.Server.Models
{
    public class Enemy
    {
        public Guid EnemyId { get; set; }

        [MaxLength(50)] public string Name { get; set; } = default!;

        public bool Boss { get; set; }

        public bool Defeated { get; set; }

        public ICollection<SeasonEnemy> SeasonEnemies { get; set; } = default!;
    }
}