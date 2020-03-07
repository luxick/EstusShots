using System;
using System.ComponentModel.DataAnnotations;

namespace EstusShots.Server.Models
{
    public class Drink
    {
        public Guid DrinkId { get; set; }

        [MaxLength(50)] public string Name { get; set; } = default!;

        public double Vol { get; set; }
    }
}