using System.Threading.Tasks;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;

namespace EstusShots.Shared.Interfaces.Controllers
{
    /// <summary>
    /// Access many seasons with one request
    /// </summary>
    public interface ISeasonsController
    {
        /// <summary>
        /// Get a list of all seasons in the system
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<ApiResponse<GetSeasonsResponse>> GetSeasons(GetSeasonsParameter parameter);

        Task<ApiResponse<GetSeasonResponse>> GetSeason(GetSeasonParameter parameter);

        Task<ApiResponse<SaveSeasonResponse>> SaveSeason(SaveSeasonParameter parameter);
    }
}