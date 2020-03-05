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
    public class DrinksController : ControllerBase, IDrinksController
    {
        private readonly ILogger _logger;
        private readonly DrinksService _service;

        public DrinksController(ILogger<DrinksController> logger, DrinksService drinksService)
        {
            _logger = logger;
            _service = drinksService;
        }

        [HttpPost]
        public async Task<ApiResponse<GetDrinksResponse>> GetDrinks(GetDrinksParameter parameter) 
            => await ServiceCall(() => _service.GetDrinks(parameter));

        [HttpPost]
        public async Task<ApiResponse<GetDrinkDetailsResponse>> GetDrinkDetails(GetDrinkDetailsParameter parameter)
            => await ServiceCall(() => _service.GetDrinkDetails(parameter));

        [HttpPost]
        public async Task<ApiResponse<SaveDrinkResponse>> SaveDrink(SaveDrinkParameter parameter)
            => await ServiceCall(() => _service.SaveDrink(parameter));

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