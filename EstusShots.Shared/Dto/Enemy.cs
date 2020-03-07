using System;
using System.ComponentModel.DataAnnotations;

namespace EstusShots.Shared.Dto
{
    public class Enemy
    {
        public Guid EnemyId { get; set; }

        public string Name { get; set; }

        public bool Boss { get; set; }

        public bool Defeated { get; set; }

        public Guid? SeasonId { get; set; }

        public Season Season { get; set; }
    }
}