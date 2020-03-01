using System.Threading.Tasks;
using AutoMapper;
using EstusShots.Server.Models;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;
using Microsoft.Extensions.Logging;

namespace EstusShots.Server.Services
{
    public class PlayersService : IPlayersController
    {
        private readonly ILogger _logger;
        private readonly EstusShotsContext _context;
        private readonly IMapper _mapper;
        
        public PlayersService(ILogger<PlayersService> logger, EstusShotsContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetPlayersResponse>> GetPlayers(GetPlayersParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ApiResponse<GetPlayerDetailsResponse>> GetPlayerDetails(GetPlayerDetailsParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ApiResponse<SavePlayerResponse>> SavePlayer(SavePlayerParameter parameter)
        {
            var player = _mapper.Map<Player>(parameter.Player);
            if (player.PlayerId.IsEmpty())
            {
                _context.Players.Add(player);
                var count = await _context.SaveChangesAsync();
                _logger.LogInformation($"Created {count} rows");
                return new ApiResponse<SavePlayerResponse>(new SavePlayerResponse(player.PlayerId));
            }
            // TODO Update Player
            return new ApiResponse<SavePlayerResponse>(new OperationResult(false, "NotImplemented"));
        }

        public async Task<ApiResponse<DeletePlayerResponse>> DeletePlayers(DeletePlayerParameter parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}