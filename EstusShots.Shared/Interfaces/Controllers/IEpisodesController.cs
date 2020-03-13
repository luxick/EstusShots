using System.Threading.Tasks;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;

namespace EstusShots.Shared.Interfaces.Controllers
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

        /// <summary>
        /// Load a single episode
        /// </summary>
        /// <param name="parameter">Parameter object for loading a single episode</param>
        /// <returns>The GetEpisode response object</returns>
        Task<ApiResponse<GetEpisodeResponse>> GetEpisode(GetEpisodeParameter parameter);

        /// <summary>
        /// Creates or updates an episode object
        /// </summary>
        /// <param name="parameter">The parameter object</param>
        /// <returns>The response object</returns>
        Task<ApiResponse<SaveEpisodeResponse>> SaveEpisode(SaveEpisodeParameter parameter);
    }
}