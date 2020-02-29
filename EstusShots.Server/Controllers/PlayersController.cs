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
    public class PlayersController : ControllerBase, IPlayersController
    {
        private readonly ILogger _logger;
        private readonly PlayersService _service;

        public PlayersController(ILogger<PlayersController> logger, PlayersService service)
        {
            _logger = logger;
            _service = service;
        }

        private async Task<ApiResponse<T>> ServiceCall<T>(Func<Task<ApiResponse<T>>> serviceCall)
            where T : class, IApiResponse, new()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Model invalid");

                }
                _logger.LogInformation($"Request received from client '{Request.HttpContext.Connection.RemoteIpAddress}'");
                return await serviceCall();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception Occured");
                return new ApiResponse<T>(new OperationResult(e));
            }
        }

        [HttpPost]
        public async Task<ApiResponse<GetPlayersResponse>> GetPlayers(GetPlayersParameter parameter) 
            => await ServiceCall(() => _service.GetPlayers(parameter));

        [HttpPost]
        public async Task<ApiResponse<GetPlayerDetailsResponse>> GetPlayerDetails(GetPlayerDetailsParameter parameter)
            => await ServiceCall(() => _service.GetPlayerDetails(parameter));

        [HttpPost]
        public async Task<ApiResponse<SavePlayerResponse>> SavePlayer(SavePlayerParameter parameter)
            => await ServiceCall(() => _service.SavePlayer(parameter));

        [HttpPost]
        public async Task<ApiResponse<DeletePlayerResponse>> DeletePlayers(DeletePlayerParameter parameter)
            => await ServiceCall(() => _service.DeletePlayers(parameter));
    }
}