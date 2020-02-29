using System;
using System.Collections.Generic;
using EstusShots.Shared.Dto;
using EstusShots.Shared.Interfaces;

namespace EstusShots.Shared.Models.Parameters
{
    // GetSeasons
    
    /// <summary>
    /// Parameter class for loading a list of all seasons from the database
    /// </summary>
    public class GetSeasonsParameter : IApiParameter { }

    /// <summary>
    /// Parameter class returned from the API with all loaded seasons
    /// </summary>
    public class GetSeasonsResponse : IApiResponse
    {
        /// <summary>
        /// All existing seasons in the database
        /// </summary>
        public List<Season> Seasons { get; set; }

        public GetSeasonsResponse(List<Season> seasons)
        {
            Seasons = seasons;
        }

        public GetSeasonsResponse()
        {
            Seasons = new List<Season>();
        }
    }
    
    // GetSeason
    
    /// <summary>
    /// Parameter class for loading a single season object
    /// </summary>
    public class GetSeasonParameter : IApiParameter
    {
        /// <summary>
        /// ID of the season that should be loaded
        /// </summary>
        public Guid SeasonId { get; set; }

        public GetSeasonParameter()
        {
            SeasonId = Guid.Empty;
        }
        
        public GetSeasonParameter(Guid seasonId)
        {
            SeasonId = seasonId;
        }

    }
    
    /// <summary>
    /// Parameter class returned from the API after loading a single season object
    /// </summary>
    public class GetSeasonResponse : IApiResponse
    {
        /// <summary>
        /// The loaded season
        /// </summary>
        public Season Season { get; set; }

        public GetSeasonResponse()
        {
            Season = new Season();
        }

        public GetSeasonResponse(Season season)
        {
            Season = season;
        }
    }
    
    // SaveSeason

    /// <summary>
    /// Parameter class for saving season objects
    /// </summary>
    public class SaveSeasonParameter : IApiParameter
    {
        /// <summary>
        /// The season object that should be saved
        /// </summary>
        public Season Season { get; set; }

        public SaveSeasonParameter()
        {
            Season = new Season();
        }

        public SaveSeasonParameter(Season season)
        {
            Season = season;
        }
    }
    
    /// <summary>
    /// Parameter class returned from the API after saving a season object
    /// </summary>
    public class SaveSeasonResponse : IApiResponse
    {
        /// <summary>
        /// ID of the season that was updated or created
        /// </summary>
        public Guid SeasonId { get; set; }

        public SaveSeasonResponse()
        {
            SeasonId = Guid.Empty;
        }
        
        public SaveSeasonResponse(Guid seasonId)
        {
            SeasonId = seasonId;
        }
    }
}