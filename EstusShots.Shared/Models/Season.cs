using System;
using System.ComponentModel.DataAnnotations;

namespace EstusShots.Shared.Models
{
    public class Season
    {
        public Guid SeasonId { get; set; }

        [MaxLength(50)] public string Game { get; set; } = default!;

        public string? Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }
    }
}