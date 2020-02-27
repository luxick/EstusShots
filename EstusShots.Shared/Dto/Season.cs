using System;

namespace EstusShots.Shared.Dto
{
    public class Season
    {
        public Guid SeasonId { get; set; }

        public int Number { get; set; }

        public string Game { get; set; } = default!;

        public string? Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public string DisplayName => $"S{Number:D2} {Game}";
    }
}