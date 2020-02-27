using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EstusShots.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Dto = EstusShots.Shared.Dto;

namespace EstusShots.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EpisodesController : ControllerBase
    {        
        private readonly EstusShotsContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EpisodesController(ILogger<EpisodesController> logger, IMapper mapper, EstusShotsContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }
        
        [HttpGet("seasonId")]
        public async Task<ActionResult<List<Dto.Episode>>> GetEpisodes(Guid seasonId)
        {   
            _logger.LogDebug($"All");
            var episodes = await _context.Episodes.Where(x => x.SeasonId == seasonId).ToListAsync();
            var dtos = _mapper.Map<List<Dto.Episode>>(episodes);
            return dtos;
        }
    }
}