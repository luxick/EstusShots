using System;

namespace EstusShots.Shared.Dto
{
    public class Player
    {
        public Guid PlayerId { get; set; }

        public string HexId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public bool Anonymous { get; set; }

        public string DisplayName => Anonymous ? Alias : Name;
    }
}