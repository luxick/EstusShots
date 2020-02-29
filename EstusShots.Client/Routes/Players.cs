using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;

namespace EstusShots.Client.Routes
{
    public class Players : IPlayersController
    {
        private readonly EstusShotsClient _client;

        public Players(EstusShotsClient client)
        {
            _client = client;
        }
        private string ActionUrl([CallerMemberName]string caller = "") =>
            $"{_client.ApiUrl}{GetType().Name}/{caller}";

        public async Task<ApiResponse<GetPlayersResponse>> GetPlayers(GetPlayersParameter parameter)
            => await _client.PostToApi<GetPlayersResponse, GetPlayersParameter>(ActionUrl(), parameter);
        
        public async Task<ApiResponse<GetPlayerDetailsResponse>> GetPlayerDetails(GetPlayerDetailsParameter parameter)
            => await _client.PostToApi<GetPlayerDetailsResponse, GetPlayerDetailsParameter>(ActionUrl(), parameter);

        public async Task<ApiResponse<SavePlayerResponse>> SavePlayer(SavePlayerParameter parameter)
            => await _client.PostToApi<SavePlayerResponse, SavePlayerParameter>(ActionUrl(), parameter);

        public async Task<ApiResponse<DeletePlayerResponse>> DeletePlayers(DeletePlayerParameter parameter)
            => await _client.PostToApi<DeletePlayerResponse, DeletePlayerParameter>(ActionUrl(), parameter);
    }
}