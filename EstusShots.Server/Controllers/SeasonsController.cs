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
    public class SeasonsController : ControllerBase, ISeasonsController
    {
        private readonly ILogger _logger;
        private readonly SeasonsService _seasonsService;

        
        public SeasonsController(ILogger<SeasonsController> logger, SeasonsService seasonsService)
        {
            _seasonsService = seasonsService;
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<ApiResponse<GetSeasonsResponse>> GetSeasons(GetSeasonsParameter parameter)
        {
            try
            {
                _logger.LogInformation($"Request received from client '{Request.HttpContext.Connection.RemoteIpAddress}'");
                return await _seasonsService.GetSeasons(parameter);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception Occured");
                return new ApiResponse<GetSeasonsResponse>(new OperationResult(e));
            }
        }
        
        [HttpPost]
        public async Task<ApiResponse<GetSeasonResponse>> GetSeason(GetSeasonParameter parameter)
        {
            try
            {
                _logger.LogInformation($"Request received from client '{Request.HttpContext.Connection.RemoteIpAddress}'");
                return await _seasonsService.GetSeason(parameter);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception Occured");
                return new ApiResponse<GetSeasonResponse>(new OperationResult(e));
            }
        }

        [HttpPost]
        public async Task<ApiResponse<SaveSeasonResponse>> SaveSeason(SaveSeasonParameter parameter)
        {
            try
            {
                _logger.LogInformation($"Request received from client '{Request.HttpContext.Connection.RemoteIpAddress}'");
                return await _seasonsService.SaveSeason(parameter);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception Occured");
                return new ApiResponse<SaveSeasonResponse>(new OperationResult(e));
            }
        }
    }
}