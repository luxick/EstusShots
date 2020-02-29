using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;

namespace EstusShots.Client.Routes
{
    public class Episodes : IEpisodesController
    {
        private readonly EstusShotsClient _client;

        public Episodes(EstusShotsClient client)
        {
            _client = client;
        }
        
        private string ActionUrl([CallerMemberName]string caller = "") =>
            $"{_client.ApiUrl}{nameof(Episodes)}/{caller}";

        public async Task<ApiResponse<GetEpisodesResponse>> GetEpisodes(GetEpisodesParameter parameter)
            => await _client.PostToApi<GetEpisodesResponse, GetEpisodesParameter>(ActionUrl(), parameter);
    }
}