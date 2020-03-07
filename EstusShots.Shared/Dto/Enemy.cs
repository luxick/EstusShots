using System;
using System.Collections.Generic;

namespace EstusShots.Shared.Dto
{
    public class Enemy
    {
        public Guid EnemyId { get; set; }

        public string Name { get; set; }

        public bool Boss { get; set; }

        public bool Defeated { get; set; }

        public Guid? SeasonId { get; set; }

        public List<Season> Seasons { get; set; }
    }
}