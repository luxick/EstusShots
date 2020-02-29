using System;
using System.Collections.Generic;
using System.Linq;
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
    public class EpisodesService : IEpisodesController
    {
        private readonly ILogger _logger;
        private readonly EstusShotsContext _context;
        private readonly IMapper _mapper;
        
        public EpisodesService(ILogger<EpisodesService> logger, EstusShotsContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetEpisodesResponse>> GetEpisodes(GetEpisodesParameter parameter)
        {
            var episodes = await _context.Episodes
                .Where(x => x.SeasonId == parameter.SeasonId)
                .Include(x => x.Season)
                .ToListAsync();
            var dtos = _mapper.Map<List<Dto.Episode>>(episodes);
            _logger.LogInformation($"{dtos.Count} episodes loaded for season '{parameter.SeasonId}'");
            return new ApiResponse<GetEpisodesResponse>(new GetEpisodesResponse(dtos));
        }

        public async Task<ApiResponse<GetEpisodeResponse>> GetEpisode(GetEpisodeParameter parameter)
        {
            var episode = await _context.Episodes
                .Include(x => x.Season)
                .FirstOrDefaultAsync(x => x.EpisodeId == parameter.EpisodeId);
            if (episode == null)
            {
                _logger.LogWarning($"Episode with ID {parameter.EpisodeId} was not found in database");
                return new ApiResponse<GetEpisodeResponse>(new OperationResult(false, "Episode not found"));
            }
            var dto = _mapper.Map<Dto.Episode>(episode);
            return new ApiResponse<GetEpisodeResponse>(new GetEpisodeResponse(dto));
        }

        public async Task<ApiResponse<SaveEpisodeResponse>> SaveEpisode(SaveEpisodeParameter parameter)
        {
            var episode = _mapper.Map<Episode>(parameter.Episode);
            if (episode.EpisodeId == Guid.Empty)
            {
                _context.Episodes.Add(episode);
                try
                {
                    var count = await _context.SaveChangesAsync();
                    _logger.LogInformation($"Updated {count} rows");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error while saving object");
                    return new ApiResponse<SaveEpisodeResponse>(new OperationResult(e));
                }
                return new ApiResponse<SaveEpisodeResponse>(new SaveEpisodeResponse(episode.EpisodeId));
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}