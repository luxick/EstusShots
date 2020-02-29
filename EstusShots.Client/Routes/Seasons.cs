using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;

namespace EstusShots.Client.Routes
{
    public class Seasons : ISeasonsController
    {
        private readonly EstusShotsClient _client;

        public Seasons(EstusShotsClient client)
        {
            _client = client;
        }
        
        private string ActionUrl([CallerMemberName]string caller = "") =>
            $"{_client.ApiUrl}{nameof(Seasons)}/{caller}";

        public async Task<ApiResponse<GetSeasonsResponse>> GetSeasons(GetSeasonsParameter parameter) => 
            await _client.PostToApi<GetSeasonsResponse, GetSeasonsParameter>(ActionUrl(), parameter);

        public async Task<ApiResponse<GetSeasonResponse>> GetSeason(GetSeasonParameter parameter) =>
            await _client.PostToApi<GetSeasonResponse, GetSeasonParameter>(ActionUrl(), parameter);

        public async Task<ApiResponse<SaveSeasonResponse>> SaveSeason(SaveSeasonParameter parameter)=>
            await _client.PostToApi<SaveSeasonResponse, SaveSeasonParameter>(ActionUrl(), parameter);
    }
}