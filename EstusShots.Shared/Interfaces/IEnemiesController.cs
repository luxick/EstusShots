using System.Threading.Tasks;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;

namespace EstusShots.Shared.Interfaces
{
    /// <summary>
    /// Load and manipulate Enemy objects
    /// </summary>
    public interface IEnemiesController
    {
        /// <summary>
        /// Load all enemies or enemies for a specific season
        /// </summary>
        /// <param name="parameter">An <see cref="GetEnemiesParameter"/> instance</param>
        /// <returns>An ApiResponse instance of type <see cref="GetEnemiesResponse"/></returns>
        Task<ApiResponse<GetEnemiesResponse>> GetEnemies(GetEnemiesParameter parameter);

        /// <summary>
        /// Load detailed information on a single enemy
        /// </summary>
        /// <param name="parameter">An <see cref="GetEnemyParameter"/> instance</param>
        /// <returns>An ApiResponse instance of type <see cref="GetEnemyResponse"/></returns>
        Task<ApiResponse<GetEnemyResponse>> GetEnemy(GetEnemyParameter parameter);

        /// <summary>
        /// Creates or updates an enemy object
        /// </summary>
        /// <param name="parameter">An <see cref="SaveEnemyParameter"/> instance</param>
        /// <returns>An ApiResponse instance of type <see cref="SaveEnemyResponse"/></returns>
        Task<ApiResponse<SaveEnemyResponse>> SaveEnemy(SaveEnemyParameter parameter);

        /// <summary>
        /// Deletes an enemy object
        /// </summary>
        /// <param name="parameter">An <see cref="DeleteEnemyParameter"/> instance</param>
        /// <returns>An ApiResponse instance of type <see cref="DeleteEnemyResponse"/></returns>
        Task<ApiResponse<DeleteEnemyResponse>> DeleteEnemy(DeleteEnemyParameter parameter);
    }
}