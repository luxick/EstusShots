using System;

namespace EstusShots.Server.Models
{
    public class Drink
    {
        public Guid DrinkId { get; set; }

        public string Name { get; set; } = default!;

        public double Vol { get; set; }
    }
}