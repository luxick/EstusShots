using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
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

        public async Task<List<Season>> GetSeasons()
        {
            var response = HttpClient.GetAsync(ApiUrl + "seasons").Result;
            var jsonData = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<Season>>(jsonData, _serializerOptions);
            return data;
        }
    }
}