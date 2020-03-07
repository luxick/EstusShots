using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;

namespace EstusShots.Client.Routes
{
    public class Enemies : IEnemiesController
    {
        private readonly EstusShotsClient _client;

        public Enemies(EstusShotsClient client)
        {
            _client = client;
        }
        
        public async Task<ApiResponse<GetEnemiesResponse>> GetEnemies(GetEnemiesParameter parameter)
            => await _client.PostToApi<GetEnemiesResponse, GetEnemiesParameter>(ActionUrl(), parameter);

        public async Task<ApiResponse<GetEnemyResponse>> GetEnemy(GetEnemyParameter parameter)
            => await _client.PostToApi<GetEnemyResponse, GetEnemyParameter>(ActionUrl(), parameter);

        public async Task<ApiResponse<SaveEnemyResponse>> SaveEnemy(SaveEnemyParameter parameter)
            => await _client.PostToApi<SaveEnemyResponse, SaveEnemyParameter>(ActionUrl(), parameter);

        public async Task<ApiResponse<DeleteEnemyResponse>> DeleteEnemy(DeleteEnemyParameter parameter)
            => await _client.PostToApi<DeleteEnemyResponse, DeleteEnemyParameter>(ActionUrl(), parameter);
        
        private string ActionUrl([CallerMemberName]string caller = "") =>
            $"{_client.ApiUrl}{GetType().Name}/{caller}";
    }
}