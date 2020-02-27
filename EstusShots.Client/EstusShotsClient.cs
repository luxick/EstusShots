using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EstusShots.Shared.Dto;
using EstusShots.Shared.Models;

namespace EstusShots.Client
{
    public class EstusShotsClient
    {
        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private string ApiUrl { get; }
        private HttpClient HttpClient { get; }

        public EstusShotsClient(string apiUrl)
        {
            ApiUrl = apiUrl;
            HttpClient = new HttpClient {Timeout = TimeSpan.FromSeconds(10)};
        }

        public async Task<(OperationResult, List<Season>)> GetSeasons()
        {
            try
            {
                var response = await HttpClient.GetAsync(ApiUrl + "seasons");
                var jsonData = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<Season>>(jsonData, _serializerOptions);
                return (new OperationResult(), data);
            }
            catch (Exception e)
            {
                return (new OperationResult(e), new List<Season>());
            }
        }

        public async Task<(OperationResult, Guid)> CreateSeason(Season season)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(season), Encoding.UTF8, "application/json");
                var response = await HttpClient.PostAsync(ApiUrl + "season", content);
                if (!response.IsSuccessStatusCode)
                {
                    return (new OperationResult(false, response.ReasonPhrase), Guid.Empty);
                }
                // TODO should give the created id
                return (new OperationResult(), Guid.Empty);
            }
            catch (Exception e)
            {
                return (new OperationResult(e), Guid.Empty);
            }
        }
        
    }
}