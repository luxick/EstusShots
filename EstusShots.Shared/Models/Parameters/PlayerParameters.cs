using System;
using System.Collections.Generic;
using EstusShots.Shared.Dto;
using EstusShots.Shared.Interfaces;

namespace EstusShots.Shared.Models.Parameters
{
    # region GetPlayers

    /// <summary>
    /// Parameter class for the GetPlayers API controller.
    /// </summary>
    public class GetPlayersParameter : IApiParameter
    {
        public GetPlayersParameter()
        {
        }
    }

    /// <summary>
    /// Parameter class returned from the GetPlayers API controller.
    /// </summary>
    public class GetPlayersResponse : IApiResponse
    {
        /// <summary>
        /// All players in the database
        /// </summary>
        public List<Player> Players { get; set; }

        public GetPlayersResponse(List<Player> players)
        {
            Players = players;
        }

        public GetPlayersResponse()
        {
        }
    }

    # endregion

    # region GetPlayerDetails

    /// <summary>
    /// Parameter class for the GetPlayerDetails API controller.
    /// </summary>
    public class GetPlayerDetailsParameter : IApiParameter
    {
        /// <summary>
        /// ID of the player that should be loaded
        /// </summary>
        public Guid PlayerId { get; set; }

        public GetPlayerDetailsParameter(Guid playerId)
        {
            PlayerId = playerId;
        }

        public GetPlayerDetailsParameter()
        {
        }
    }

    /// <summary>
    /// Parameter class returned from the GetPlayerDetails API controller.
    /// </summary>
    public class GetPlayerDetailsResponse : IApiResponse
    {
        /// <summary>
        /// The loaded player
        /// </summary>
        public Player Player { get; set; }

        public GetPlayerDetailsResponse(Player player)
        {
            Player = player;
        }

        public GetPlayerDetailsResponse()
        {
        }
    }

    # endregion

    # region SavePlayer

    /// <summary>
    /// Parameter class for the SavePlayer API controller.
    /// </summary>
    public class SavePlayerParameter : IApiParameter
    {
        /// <summary>
        /// The player to save
        /// </summary>
        public Player Player { get; set; }

        public SavePlayerParameter(Player player)
        {
            Player = player;
        }

        public SavePlayerParameter()
        {
        }
    }

    /// <summary>
    /// Parameter class returned from the SavePlayer API controller.
    /// </summary>
    public class SavePlayerResponse : IApiResponse
    {
        /// <summary>
        /// ID of the newly created or updated player
        /// </summary>
        public Guid PlayerId { get; set; }

        public SavePlayerResponse(Guid playerId)
        {
            PlayerId = playerId;
        }

        public SavePlayerResponse()
        {
        }
    }

    # endregion

    # region DeletePlayer

    /// <summary>
    /// Parameter class for the DeletePlayer API controller.
    /// </summary>
    public class DeletePlayerParameter : IApiParameter
    {
        /// <summary>
        /// ID of the player that should be deleted
        /// </summary>
        public Guid PlayerId { get; set; }

        public DeletePlayerParameter(Guid playerId)
        {
            PlayerId = playerId;
        }

        public DeletePlayerParameter()
        {
        }
    }

    /// <summary>
    /// Parameter class returned from the DeletePlayer API controller.
    /// </summary>
    public class DeletePlayerResponse : IApiResponse
    {
        public DeletePlayerResponse()
        {
        }
    }

    # endregion
}