using System.Collections.Generic;
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
            var players = await _context.Players.ToListAsync();
            var dtos = _mapper.Map<List<Dto.Player>>(players);
            return new ApiResponse<GetPlayersResponse>(new GetPlayersResponse(dtos));
        }

        public async Task<ApiResponse<GetPlayerDetailsResponse>> GetPlayerDetails(GetPlayerDetailsParameter parameter)
        {
            var player = await _context.Players.FindAsync(parameter.PlayerId);
            if (player == null)
            {
                _logger.LogWarning($"Player '{parameter.PlayerId}' not found in database");
                return new ApiResponse<GetPlayerDetailsResponse>(new OperationResult(false, "Player not found"));
            }

            var dto = _mapper.Map<Dto.Player>(player);
            return new ApiResponse<GetPlayerDetailsResponse>(new GetPlayerDetailsResponse(dto));
        }

        public async Task<ApiResponse<SavePlayerResponse>> SavePlayer(SavePlayerParameter parameter)
        {
            if (parameter.Player.PlayerId.IsEmpty())
            {
                var player = _mapper.Map<Player>(parameter.Player);
                _context.Players.Add(player);
                var count = await _context.SaveChangesAsync();
                _logger.LogInformation($"Created player '{player.PlayerId}' ({count} rows updated)");
                return new ApiResponse<SavePlayerResponse>(new SavePlayerResponse(player.PlayerId));
            }
            else
            {
                var player = await _context.Players.FindAsync(parameter.Player.PlayerId);
                _context.Players.Update(player);
                _mapper.Map(parameter.Player, player);
                var count = await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated player '{player.PlayerId}' ({count} rows)");
                return new ApiResponse<SavePlayerResponse>(new SavePlayerResponse(player.PlayerId));
            }
        }

        public Task<ApiResponse<DeletePlayerResponse>> DeletePlayer(DeletePlayerParameter parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}