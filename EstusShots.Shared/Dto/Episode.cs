using System;
using System.Text.Json.Serialization;

namespace EstusShots.Shared.Dto
{
    public class Episode
    {
        public Guid EpisodeId { get; set; }
        
        public int Number { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public DateTime Start { get; set; }
        
        public DateTime? End { get; set; }

        public Guid SeasonId { get; set; }

        public Season Season { get; set; }

        public string DisplayName => $"S{Season?.Number ?? 0:D2}E{Number:D2}";
    }
}