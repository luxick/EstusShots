using System;
using System.Collections.Generic;
using EstusShots.Shared.Dto;
using EstusShots.Shared.Interfaces;

namespace EstusShots.Shared.Models.Parameters
{
    // GetEpisodes

    /// <summary>
    /// Parameter class for loading all episodes for a season
    /// </summary>
    public class GetEpisodesParameter : IApiParameter
    {
        /// <summary>
        /// ID of the season for which to load the episode list
        /// </summary>
        public Guid SeasonId { get; set; }

        public GetEpisodesParameter(Guid seasonId)
        {
            SeasonId = seasonId;
        }

        public GetEpisodesParameter()
        {
            SeasonId = Guid.Empty;
        }
    }

    /// <summary>
    /// Parameter class returned from the API with all loaded episodes for a season
    /// </summary>
    public class GetEpisodesResponse : IApiResponse
    {
        /// <summary>
        /// List of all episodes in the requested season
        /// </summary>
        public List<Episode> Episodes { get; set; }

        public GetEpisodesResponse(List<Episode> episodes)
        {
            Episodes = episodes;
        }

        public GetEpisodesResponse()
        {
            Episodes = new List<Episode>();
        }
    }
}