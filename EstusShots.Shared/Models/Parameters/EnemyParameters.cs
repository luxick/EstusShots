using System;
using System.Collections.Generic;
using EstusShots.Shared.Dto;
using EstusShots.Shared.Interfaces;

namespace EstusShots.Shared.Models.Parameters
{
    # region GetEnemies

    /// <summary>
    /// Parameter class for the GetEnemies API controller.
    /// </summary>
    public class GetEnemiesParameter : IApiParameter
    {
        /// <summary>
        /// (Optional) Load enemies for this season
        /// </summary>
        public Guid SeasonId { get; set; }

        public GetEnemiesParameter(Guid seasonId)
        {
            SeasonId = seasonId;
        }

        public GetEnemiesParameter()
        {
            SeasonId = Guid.Empty;
        }
    }

    /// <summary>
    /// Parameter class returned from the GetEnemies API controller.
    /// </summary>
    public class GetEnemiesResponse : IApiResponse
    {
        /// <summary>
        /// The loaded enemies
        /// </summary>
        public List<Enemy> Enemies { get; set; }

        public GetEnemiesResponse(List<Enemy> enemies)
        {
            Enemies = enemies;
        }

        public GetEnemiesResponse()
        {
        }
    }

    # endregion

    # region GetEnemy

    /// <summary>
    /// Parameter class for the GetEnemy API controller.
    /// </summary>
    public class GetEnemyParameter : IApiParameter
    {
        /// <summary>
        /// ID of the enemy to load 
        /// </summary>
        public Guid EnemyId { get; set; }

        public GetEnemyParameter(Guid enemyId)
        {
            EnemyId = enemyId;
        }

        public GetEnemyParameter()
        {
        }
    }

    /// <summary>
    /// Parameter class returned from the GetEnemy API controller.
    /// </summary>
    public class GetEnemyResponse : IApiResponse
    {
        /// <summary>
        /// A detailed Enemy object
        /// </summary>
        public Enemy Enemy { get; set; }

        public GetEnemyResponse(Enemy enemy)
        {
            Enemy = enemy;
        }

        public GetEnemyResponse()
        {
        }
    }

    # endregion

    # region SaveEnemy

    /// <summary>
    /// Parameter class for the SaveEnemy API controller.
    /// </summary>
    public class SaveEnemyParameter : IApiParameter
    {
        /// <summary>
        /// The enemy object to create or update
        /// </summary>
        public Enemy Enemy { get; set; }

        public SaveEnemyParameter(Enemy enemy)
        {
            Enemy = enemy;
        }

        public SaveEnemyParameter()
        {
        }
    }

    /// <summary>
    /// Parameter class returned from the SaveEnemy API controller.
    /// </summary>
    public class SaveEnemyResponse : IApiResponse
    {
        /// <summary>
        /// ID of the created or updated enemy object
        /// </summary>
        public Guid EnemyId { get; set; }

        public SaveEnemyResponse(Guid enemyId)
        {
            EnemyId = enemyId;
        }

        public SaveEnemyResponse()
        {
        }
    }

    # endregion

    # region DeleteEnemy

    /// <summary>
    /// Parameter class for the DeleteEnemy API controller.
    /// </summary>
    public class DeleteEnemyParameter : IApiParameter
    {
        /// <summary>
        /// ID of the enemy to delete
        /// </summary>
        public Guid EnemyId { get; set; }

        public DeleteEnemyParameter(Guid enemyId)
        {
            EnemyId = enemyId;
        }

        public DeleteEnemyParameter()
        {
        }
    }

    /// <summary>
    /// Parameter class returned from the DeleteEnemy API controller.
    /// </summary>
    public class DeleteEnemyResponse : IApiResponse
    {
        public DeleteEnemyResponse()
        {
        }
    }

    # endregion
}