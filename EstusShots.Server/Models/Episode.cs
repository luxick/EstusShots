using System;
using System.ComponentModel.DataAnnotations;

namespace EstusShots.Server.Models
{
    public class Episode
    {
        public Guid EpisodeId { get; set; }

        public int Number { get; set; }

        [MaxLength(50)] public string Title { get; set; } = default!;

        public DateTime DateTime { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public Guid SeasonId { get; set; }

        public Season Season { get; set; } = default!;
    }
}