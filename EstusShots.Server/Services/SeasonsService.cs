using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EstusShots.Server.Models;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Dto = EstusShots.Shared.Dto;

namespace EstusShots.Server.Services
{
    public class SeasonsService : ISeasonsController
    {
        private readonly EstusShotsContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SeasonsService(EstusShotsContext context, IMapper mapper, ILogger<SeasonsService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<GetSeasonsResponse>> GetSeasons(GetSeasonsParameter parameter)
        {
            var seasons = await _context.Seasons.ToListAsync();
            var dtos = _mapper.Map<List<Dto.Season>>(seasons);
            return new ApiResponse<GetSeasonsResponse>(new GetSeasonsResponse(dtos));
        }

        public async Task<ApiResponse<GetSeasonResponse>> GetSeason(GetSeasonParameter parameter)
        {
            var season = await _context.Seasons.FindAsync(parameter.SeasonId);
            if (season == null)
            {
                _logger.LogWarning($"Season '{parameter.SeasonId}' not found in database");
                return new ApiResponse<GetSeasonResponse>(new OperationResult(false, $"Season '{parameter.SeasonId}' not found in database"));
            }
            var seasonDto = _mapper.Map<Dto.Season>(season);
            return new ApiResponse<GetSeasonResponse>(new GetSeasonResponse(seasonDto));
        }

        public async Task<ApiResponse<SaveSeasonResponse>> SaveSeason(SaveSeasonParameter parameter)
        {
            var season = _mapper.Map<Season>(parameter.Season);
            var existing = await _context.Seasons.FindAsync(season.SeasonId);
            if (existing == null)
            {
                _context.Seasons.Add(season);await _context.SaveChangesAsync();
                _logger.LogInformation($"New season created: '{season.SeasonId}'");
            }
            else
            {
                throw new NotImplementedException();
            }
            return new ApiResponse<SaveSeasonResponse>(new SaveSeasonResponse(season.SeasonId));
        }
    }
}