using System.Threading.Tasks;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;
using Microsoft.Extensions.Logging;

namespace EstusShots.Server.Services
{
    public class PlayersService : IPlayersController
    {
        private readonly ILogger _logger;
        
        public PlayersService(ILogger<PlayersService> logger)
        {
            _logger = logger;
        }

        public Task<ApiResponse<GetPlayersResponse>> GetPlayers(GetPlayersParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<GetPlayerDetailsResponse>> GetPlayerDetails(GetPlayerDetailsParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<SavePlayerResponse>> SavePlayer(SavePlayerParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<DeletePlayerResponse>> DeletePlayers(DeletePlayerParameter parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}