using System.Threading.Tasks;
using AutoMapper;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;
using Microsoft.Extensions.Logging;

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

        public Task<ApiResponse<GetEnemiesResponse>> GetEnemies(GetEnemiesParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<GetEnemyResponse>> GetEnemy(GetEnemyParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<SaveEnemyResponse>> SaveEnemy(SaveEnemyParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<DeleteEnemyResponse>> DeleteEnemy(DeleteEnemyParameter parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}