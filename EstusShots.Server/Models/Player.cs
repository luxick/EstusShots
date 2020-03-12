using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EstusShots.Server.Models
{
    public class Player
    {
        public Guid PlayerId { get; set; }

        [MaxLength(4)] public string? HexId { get; set; }

        [MaxLength(30)] public string Name { get; set; } = default!;

        [MaxLength(30)] public string Alias { get; set; } = default!;

        public bool Anonymous { get; set; }

        public string DisplayName => Anonymous ? Alias : Name;

        public ICollection<EpisodePlayer> EpisodePlayers { get; set; } = default!;
    }
}