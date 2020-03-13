﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.Threading.Tasks;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;
using EstusShots.Shared.Interfaces.Controllers;

namespace EstusShots.Client
{
    // Generated part of the client class
    public partial class EstusShotsClient
    {
        public Drinks Drinks { get; internal set; }
        public Enemies Enemies { get; internal set; }
        public Episodes Episodes { get; internal set; }
        public Players Players { get; internal set; }
        public Seasons Seasons { get; internal set; }

        private void CreateApiRoutes()
        {
            Drinks = new Drinks(this);
            Enemies = new Enemies(this);
            Episodes = new Episodes(this);
            Players = new Players(this);
            Seasons = new Seasons(this);
        }
    }

    public class Drinks : IDrinksController
    {
        private readonly EstusShotsClient _client;

        public Drinks(EstusShotsClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse<GetDrinksResponse>> GetDrinks(GetDrinksParameter parameter)
            => await _client.PostToApi<GetDrinksResponse, GetDrinksParameter>($"{_client.ApiUrl}Drinks/GetDrinks", parameter);

        public async Task<ApiResponse<GetDrinkDetailsResponse>> GetDrinkDetails(GetDrinkDetailsParameter parameter)
            => await _client.PostToApi<GetDrinkDetailsResponse, GetDrinkDetailsParameter>($"{_client.ApiUrl}Drinks/GetDrinkDetails", parameter);

        public async Task<ApiResponse<SaveDrinkResponse>> SaveDrink(SaveDrinkParameter parameter)
            => await _client.PostToApi<SaveDrinkResponse, SaveDrinkParameter>($"{_client.ApiUrl}Drinks/SaveDrink", parameter);

    }

    public class Enemies : IEnemiesController
    {
        private readonly EstusShotsClient _client;

        public Enemies(EstusShotsClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse<GetEnemiesResponse>> GetEnemies(GetEnemiesParameter parameter)
            => await _client.PostToApi<GetEnemiesResponse, GetEnemiesParameter>($"{_client.ApiUrl}Enemies/GetEnemies", parameter);

        public async Task<ApiResponse<GetEnemyResponse>> GetEnemy(GetEnemyParameter parameter)
            => await _client.PostToApi<GetEnemyResponse, GetEnemyParameter>($"{_client.ApiUrl}Enemies/GetEnemy", parameter);

        public async Task<ApiResponse<SaveEnemyResponse>> SaveEnemy(SaveEnemyParameter parameter)
            => await _client.PostToApi<SaveEnemyResponse, SaveEnemyParameter>($"{_client.ApiUrl}Enemies/SaveEnemy", parameter);

        public async Task<ApiResponse<DeleteEnemyResponse>> DeleteEnemy(DeleteEnemyParameter parameter)
            => await _client.PostToApi<DeleteEnemyResponse, DeleteEnemyParameter>($"{_client.ApiUrl}Enemies/DeleteEnemy", parameter);

    }

    public class Episodes : IEpisodesController
    {
        private readonly EstusShotsClient _client;

        public Episodes(EstusShotsClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse<GetEpisodesResponse>> GetEpisodes(GetEpisodesParameter parameter)
            => await _client.PostToApi<GetEpisodesResponse, GetEpisodesParameter>($"{_client.ApiUrl}Episodes/GetEpisodes", parameter);

        public async Task<ApiResponse<GetEpisodeResponse>> GetEpisode(GetEpisodeParameter parameter)
            => await _client.PostToApi<GetEpisodeResponse, GetEpisodeParameter>($"{_client.ApiUrl}Episodes/GetEpisode", parameter);

        public async Task<ApiResponse<SaveEpisodeResponse>> SaveEpisode(SaveEpisodeParameter parameter)
            => await _client.PostToApi<SaveEpisodeResponse, SaveEpisodeParameter>($"{_client.ApiUrl}Episodes/SaveEpisode", parameter);

    }

    public class Players : IPlayersController
    {
        private readonly EstusShotsClient _client;

        public Players(EstusShotsClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse<GetPlayersResponse>> GetPlayers(GetPlayersParameter parameter)
            => await _client.PostToApi<GetPlayersResponse, GetPlayersParameter>($"{_client.ApiUrl}Players/GetPlayers", parameter);

        public async Task<ApiResponse<GetPlayerDetailsResponse>> GetPlayerDetails(GetPlayerDetailsParameter parameter)
            => await _client.PostToApi<GetPlayerDetailsResponse, GetPlayerDetailsParameter>($"{_client.ApiUrl}Players/GetPlayerDetails", parameter);

        public async Task<ApiResponse<SavePlayerResponse>> SavePlayer(SavePlayerParameter parameter)
            => await _client.PostToApi<SavePlayerResponse, SavePlayerParameter>($"{_client.ApiUrl}Players/SavePlayer", parameter);

        public async Task<ApiResponse<DeletePlayerResponse>> DeletePlayer(DeletePlayerParameter parameter)
            => await _client.PostToApi<DeletePlayerResponse, DeletePlayerParameter>($"{_client.ApiUrl}Players/DeletePlayer", parameter);

    }

    public class Seasons : ISeasonsController
    {
        private readonly EstusShotsClient _client;

        public Seasons(EstusShotsClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse<GetSeasonsResponse>> GetSeasons(GetSeasonsParameter parameter)
            => await _client.PostToApi<GetSeasonsResponse, GetSeasonsParameter>($"{_client.ApiUrl}Seasons/GetSeasons", parameter);

        public async Task<ApiResponse<GetSeasonResponse>> GetSeason(GetSeasonParameter parameter)
            => await _client.PostToApi<GetSeasonResponse, GetSeasonParameter>($"{_client.ApiUrl}Seasons/GetSeason", parameter);

        public async Task<ApiResponse<SaveSeasonResponse>> SaveSeason(SaveSeasonParameter parameter)
            => await _client.PostToApi<SaveSeasonResponse, SaveSeasonParameter>($"{_client.ApiUrl}Seasons/SaveSeason", parameter);

    }

}