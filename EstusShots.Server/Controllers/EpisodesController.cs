using System;
using System.Threading.Tasks;
using EstusShots.Server.Services;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EstusShots.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class EpisodesController : ControllerBase, IEpisodesController
    {
        private readonly ILogger _logger;
        private readonly EpisodesService _episodesService;

        public EpisodesController(ILogger<EpisodesController> logger, EpisodesService episodesService)
        {
            _logger = logger;
            _episodesService = episodesService;
        }

        public async Task<ApiResponse<GetEpisodesResponse>> GetEpisodes(GetEpisodesParameter parameter)
        {
            try
            {
                _logger.LogInformation($"Request received from client '{Request.HttpContext.Connection.RemoteIpAddress}'");
                return await _episodesService.GetEpisodes(parameter);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception Occured");
                return new ApiResponse<GetEpisodesResponse>(new OperationResult(e));
            }
        }

        public async Task<ApiResponse<GetEpisodeResponse>> GetEpisode(GetEpisodeParameter parameter)
        {
            try
            {
                _logger.LogInformation($"Request received from client '{Request.HttpContext.Connection.RemoteIpAddress}'");
                return await _episodesService.GetEpisode(parameter);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception Occured");
                return new ApiResponse<GetEpisodeResponse>(new OperationResult(e));
            }
        }

        public async Task<ApiResponse<SaveEpisodeResponse>> SaveEpisode(SaveEpisodeParameter parameter)
        {
            try
            {
                _logger.LogInformation($"Request received from client '{Request.HttpContext.Connection.RemoteIpAddress}'");
                return await _episodesService.SaveEpisode(parameter);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception Occured");
                return new ApiResponse<SaveEpisodeResponse>(new OperationResult(e));
            }
        }
    }
}