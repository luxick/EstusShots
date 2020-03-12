using System;

namespace EstusShots.Server.Models
{
    public class EpisodePlayer
    {
        public Guid EpisodeId { get; set; } = default!;
        public Episode Episode { get; set; } = default!;

        public Guid PlayerId { get; set; } = default!;
        public Player Player { get; set; } = default!;
    }
}