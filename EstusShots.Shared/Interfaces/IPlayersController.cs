using System.Threading.Tasks;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;

namespace EstusShots.Shared.Interfaces
{
    /// <summary>
    /// Load and manipulate player objects
    /// </summary>
    public interface IPlayersController
    {
        /// <summary>
        /// Loads a list of all players in the database
        /// </summary>
        /// <param name="parameter">An <see cref="GetPlayersParameter"/> instance</param>
        /// <returns>An ApiResponse instance of type <see cref="GetPlayersResponse"/></returns>
        Task<ApiResponse<GetPlayersResponse>> GetPlayers(GetPlayersParameter parameter);

        /// <summary>
        /// Loads detailed information on a single player
        /// </summary>
        /// <param name="parameter">An <see cref="GetPlayerDetailsParameter"/> instance</param>
        /// <returns>An ApiResponse instance of type <see cref="GetPlayerDetailsResponse"/></returns>
        Task<ApiResponse<GetPlayerDetailsResponse>> GetPlayerDetails(GetPlayerDetailsParameter parameter);

        /// <summary>
        /// Creates or updates a player object
        /// </summary>
        /// <param name="parameter">An <see cref="SavePlayerParameter"/> instance</param>
        /// <returns>An ApiResponse instance of type <see cref="SavePlayerResponse"/></returns>
        Task<ApiResponse<SavePlayerResponse>> SavePlayer(SavePlayerParameter parameter);

        /// <summary>
        /// Deletes a player
        /// </summary>
        /// <param name="parameter">An <see cref="DeletePlayerParameter"/> instance</param>
        /// <returns>An ApiResponse instance of type <see cref="DeletePlayerResponse"/></returns>
        Task<ApiResponse<DeletePlayerResponse>> DeletePlayers(DeletePlayerParameter parameter);
    }
}