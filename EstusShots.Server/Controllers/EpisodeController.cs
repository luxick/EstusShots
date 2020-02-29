using System;
using System.Threading.Tasks;
using AutoMapper;
using EstusShots.Server.Models;
using EstusShots.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EstusShots.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EpisodeController : ControllerBase
    {
        private readonly EstusShotsContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EpisodeController(EstusShotsContext context, IMapper mapper, ILogger<EpisodeController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<ActionResult<Shared.Dto.Episode>> CreateSeason(Shared.Dto.Episode episodeDto)
        {
            var episode = _mapper.Map<Episode>(episodeDto);
            _context.Episodes.Add(episode);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while saving object");
            }
            return CreatedAtAction("", new {id = episode.EpisodeId}, episode);
        }
    }
}