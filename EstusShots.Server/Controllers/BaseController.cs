using System;
using System.Threading.Tasks;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EstusShots.Server.Controllers
{
    /// <summary>
    /// Base class for all API controllers.
    /// Contains shared methods and properties that get used by the generated code.
    /// </summary>
    public abstract class BaseController : ControllerBase
    {
        private readonly ILogger _logger;

        protected BaseController(ILogger logger)
        {
            _logger = logger;
        }
        
        /// <summary>
        /// Generic method to handle the boilerplate code when calling an api service.
        /// </summary>
        /// <param name="serviceCall">Function that calls an API service</param>
        /// <typeparam name="T">A response parameter that implements <see cref="IApiResponse"/></typeparam>
        /// <returns>An <see cref="ApiResponse{T}"/></returns>
        protected async Task<ApiResponse<T>> ServiceCall<T>(Func<Task<ApiResponse<T>>> serviceCall)
            where T : class, IApiResponse, new()
        {
            try
            {
                if (!ModelState.IsValid) _logger.LogError($"Model invalid");
                _logger.LogInformation($"Request received from client '{Request.HttpContext.Connection.RemoteIpAddress}'");
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