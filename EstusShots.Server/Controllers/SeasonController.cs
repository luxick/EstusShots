using System;
using System.Threading.Tasks;
using AutoMapper;
using EstusShots.Server.Models;
using EstusShots.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Dto = EstusShots.Shared.Models;

namespace EstusShots.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SeasonController : ControllerBase
    {
        private readonly EstusShotsContext _context;
        private readonly IMapper _mapper;

        public SeasonController(EstusShotsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dto.Season>> GetSeason(Guid id)
        {
            var season = await _context.Seasons.FindAsync(id);
            if (season == null) return NotFound();

            var seasonDto = _mapper.Map<Dto.Season>(season);
            return seasonDto;
        }
        
        [HttpPost]
        public async Task<ActionResult<Dto.Season>> CreateSeason(Dto.Season season)
        {
            var dbSeason = _mapper.Map<Season>(season);
            _context.Seasons.Add(dbSeason);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSeason), new {id = dbSeason.SeasonId}, dbSeason);
        }
    }
}