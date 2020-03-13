using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EstusShots.Server.Models;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Interfaces.Controllers;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Dto = EstusShots.Shared.Dto;

namespace EstusShots.Server.Services
{
    public class EnemiesService : IEnemiesController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly EstusShotsContext _context;

        public EnemiesService(ILogger<EnemiesService> logger, IMapper mapper, EstusShotsContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ApiResponse<GetEnemiesResponse>> GetEnemies(GetEnemiesParameter parameter)
        {
            List<Enemy> enemies;
            if (parameter.SeasonId.IsEmpty())
            {
                enemies = await _context.Enemies
                    .Include("SeasonEnemies.Season")
                    .ToListAsync();
                _logger.LogInformation($"Found {enemies.Count} enemies in database");
            }
            else
            {
                enemies = await _context.Enemies
                    .Include("SeasonEnemies.Season")
                    .Where(e => e.SeasonEnemies.Any(x => x.SeasonId == parameter.SeasonId))
                    .ToListAsync();
                _logger.LogInformation($"Found {enemies.Count} enemies for season '{parameter.SeasonId}'");
            }
            var dtos = _mapper.Map<List<Dto.Enemy>>(enemies);
            return new ApiResponse<GetEnemiesResponse>(new GetEnemiesResponse(dtos));
        }

        public async Task<ApiResponse<GetEnemyResponse>> GetEnemy(GetEnemyParameter parameter)
        {
            var enemy = await _context.Enemies.FindAsync(parameter.EnemyId);
            if (enemy == null)
            {
                _logger.LogWarning($"Enemy {parameter.EnemyId} not found in database");
                return new ApiResponse<GetEnemyResponse>(new OperationResult(false, "Object not found"));
            }

            var dto = _mapper.Map<Dto.Enemy>(enemy);
            return new ApiResponse<GetEnemyResponse>(new GetEnemyResponse(dto));
        }

        public async Task<ApiResponse<SaveEnemyResponse>> SaveEnemy(SaveEnemyParameter parameter)
        {
            if (parameter.Enemy.EnemyId.IsEmpty()) 
                return await CreateEnemy(parameter);
            return await UpdateEnemy(parameter);
        }

        public async Task<ApiResponse<DeleteEnemyResponse>> DeleteEnemy(DeleteEnemyParameter parameter)
        {
            var enemy = await _context.Enemies.FindAsync(parameter.EnemyId);
            if (enemy == null)
            {
                _logger.LogError($"Enemy '{parameter.EnemyId}' not found in database");
                return new ApiResponse<DeleteEnemyResponse>(new OperationResult(false, "Object not found"));
            }
            
            _context.Enemies.Remove(enemy);
            var count = _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted enemy '{parameter.EnemyId}' ({count} rows)");
            return new ApiResponse<DeleteEnemyResponse>(new DeleteEnemyResponse());
        }
        
        // Private Methods

        private async Task<ApiResponse<SaveEnemyResponse>> CreateEnemy(SaveEnemyParameter parameter)
        {
            var enemy = _mapper.Map<Enemy>(parameter.Enemy);
            _context.Enemies.Add(enemy);
            var count = await _context.SaveChangesAsync();
            _logger.LogInformation($"Created enemy '{enemy.EnemyId}' ({count} rows)");

            // Relation mapping
            enemy.SeasonEnemies = new List<SeasonEnemy>();
            foreach (var season in parameter.Enemy.Seasons)
            {
                var relation = new SeasonEnemy
                {
                    SeasonId = season.SeasonId,
                    EnemyId = enemy.EnemyId
                };
                enemy.SeasonEnemies.Add(relation);
            }
            
            count = await _context.SaveChangesAsync();
            _logger.LogInformation($"Added enemy '{enemy.EnemyId}' to {parameter.Enemy.Seasons.Count} seasons ({count} rows)");

            return new ApiResponse<SaveEnemyResponse>(new SaveEnemyResponse(enemy.EnemyId));
        }
        
        private async Task<ApiResponse<SaveEnemyResponse>> UpdateEnemy(SaveEnemyParameter parameter)
        {
            var enemy = await _context.Enemies.FindAsync(parameter.Enemy.EnemyId);
            if (enemy == null)
            {
                _logger.LogError($"Enemy '{parameter.Enemy.EnemyId}' not found in database");
                return new ApiResponse<SaveEnemyResponse>(new OperationResult(false, "Object not found"));
            }

            _context.Enemies.Update(enemy);
            _mapper.Map(parameter.Enemy, enemy);
            var count = await _context.SaveChangesAsync();
            _logger.LogInformation($"Updated enemy '{enemy.EnemyId}' ({count} rows)");
            return new ApiResponse<SaveEnemyResponse>(new SaveEnemyResponse(enemy.EnemyId));
        }
    }
}