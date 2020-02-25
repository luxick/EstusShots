using System;
using System.Threading.Tasks;
using EstusShots.Server.Services;
using EstusShots.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstusShots.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SeasonsController : ControllerBase
    {
        private readonly EstusShotsContext _context;

        public SeasonsController(EstusShotsContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Season>> GetSeason(Guid id)
        {
            var season = await _context.Seasons.FindAsync(id);
            if (season == null) return NotFound();
            return season;
        }
        
        [HttpPost]
        public async Task<ActionResult<Season>> CreateSeason(Season season)
        {
            _context.Seasons.Add(season);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSeason), new {id = season.SeasonId}, season);
        }
    }
}