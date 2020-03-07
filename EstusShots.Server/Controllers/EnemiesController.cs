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
    public class EnemiesController : ControllerBase, IEnemiesController
    {
        private readonly EnemiesService _service;
        private readonly ILogger _logger;
        
        public EnemiesController(EnemiesService service, ILogger<EnemiesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ApiResponse<GetEnemiesResponse>> GetEnemies(GetEnemiesParameter parameter) 
            => await ServiceCall(() => _service.GetEnemies(parameter));

        [HttpPost]
        public async Task<ApiResponse<GetEnemyResponse>> GetEnemy(GetEnemyParameter parameter) 
            => await ServiceCall(() => _service.GetEnemy(parameter));
        
        [HttpPost]
        public async Task<ApiResponse<SaveEnemyResponse>> SaveEnemy(SaveEnemyParameter parameter) 
            => await ServiceCall(() => _service.SaveEnemy(parameter));

        [HttpPost]
        public async Task<ApiResponse<DeleteEnemyResponse>> DeleteEnemy(DeleteEnemyParameter parameter) 
            => await ServiceCall(() => _service.DeleteEnemy(parameter));
        
        private async Task<ApiResponse<T>> ServiceCall<T>(Func<Task<ApiResponse<T>>> serviceCall)
            where T : class, IApiResponse, new()
        {
            try
            {
                if (!ModelState.IsValid) _logger.LogError($"Model invalid");
                _logger.LogInformation(
                    $"Request received from client '{Request.HttpContext.Connection.RemoteIpAddress}'");
                return await serviceCall();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception Occured");
                return new ApiResponse<T>(new OperationResult(e));
            }
        }
    }
}