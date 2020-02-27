using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EstusShots.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dto = EstusShots.Shared.Dto;

namespace EstusShots.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SeasonsController : ControllerBase
    {
        private readonly EstusShotsContext _context;
        private readonly IMapper _mapper;

        
        public SeasonsController(EstusShotsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Dto.Season>>> GetSeasons()
        {
            var seasons = await _context.Seasons.ToListAsync();
            var dtos = _mapper.Map<List<Dto.Season>>(seasons);
            return dtos;
        }
    }
}