using System.Threading.Tasks;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;

namespace EstusShots.Shared.Interfaces
{
    /// <summary>
    /// Access to episodes
    /// </summary>
    public interface IEpisodesController
    {
        /// <summary>
        /// Load all episodes for a season
        /// </summary>
        /// <param name="parameter">The parameter object</param>
        /// <returns>The GetEpisodes response object</returns>
        Task<ApiResponse<GetEpisodesResponse>> GetEpisodes(GetEpisodesParameter parameter);
    }
}