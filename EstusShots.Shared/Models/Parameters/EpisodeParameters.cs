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
    
    // GetEpisode

    /// <summary>
    /// Parameter class for loading a single episode
    /// </summary>
    public class GetEpisodeParameter : IApiParameter
    {
        /// <summary>
        /// ID of the episode
        /// </summary>
        public Guid EpisodeId { get; set; }
        
        public GetEpisodeParameter(Guid episodeId)
        {
            EpisodeId = episodeId;
        }

        public GetEpisodeParameter()
        {
            EpisodeId = Guid.Empty;
        }
    }

    /// <summary>
    /// Parameter class returned from the API with a single loaded episode
    /// </summary>
    public class GetEpisodeResponse : IApiResponse
    {
        /// <summary>
        /// The loaded episode
        /// </summary>
        public Episode Episode { get; set; }
        
        public GetEpisodeResponse(Episode episode)
        {
            Episode = episode;
        }

        public GetEpisodeResponse()
        {
            
        }
    }
    
    // SaveEpisode

    /// <summary>
    /// Parameter class for creating or updating episode objects
    /// </summary>
    public class SaveEpisodeParameter : IApiParameter
    {
        /// <summary>
        /// The new or updated episode
        /// </summary>
        public Episode Episode { get; set; }

        public SaveEpisodeParameter(Episode episode)
        {
            Episode = episode;
        }

        public SaveEpisodeParameter()
        {
            Episode = new Episode();
        }
    }

    /// <summary>
    /// Parameter class returned from the API after creating or updating an episode
    /// </summary>
    public class SaveEpisodeResponse : IApiResponse
    {
        /// <summary>
        /// ID of the created or updated episode
        /// </summary>
        public Guid EpisodeId { get; set; }

        public SaveEpisodeResponse(Guid episodeId)
        {
            EpisodeId = episodeId;
        }

        public SaveEpisodeResponse()
        {
            
        }
    }
}