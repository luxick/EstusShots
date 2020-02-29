using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EstusShots.Client.Routes;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Models;

namespace EstusShots.Client
{
    public class EstusShotsClient
    {
        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        private HttpClient HttpClient { get; }

        public string ApiUrl { get; }
        
        // API Routes
        public Seasons Seasons { get; }

        /// <summary>
        /// Creates a new instance of <see cref="EstusShotsClient"/>
        /// </summary>
        /// <param name="apiUrl">Base URL of the Estus Shots API host</param>
        public EstusShotsClient(string apiUrl)
        {
            ApiUrl = apiUrl;
            HttpClient = new HttpClient {Timeout = TimeSpan.FromSeconds(10)};
            
            Seasons = new Seasons(this);
        }
        
        /// <summary>
        /// Generic method to post a request to the API
        /// </summary>
        /// <param name="url">URL to the desired action</param>
        /// <param name="parameter">The API parameter object instance</param>
        /// <typeparam name="TResult">API response class that implements <see cref="IApiResponse"/></typeparam>
        /// <typeparam name="TParam">API parameter class that implements <see cref="IApiParameter"/></typeparam>
        /// <returns></returns>
        public async Task<ApiResponse<TResult>> PostToApi<TResult, TParam>(string url, TParam parameter) 
            where TParam : IApiParameter, new()
            where TResult : class, IApiResponse, new()
        {
            try
            {
                var serialized = JsonSerializer.Serialize(parameter);
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");
                var response = await HttpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<TResult>>(json, _serializerOptions);
                return result;
            }
            catch (Exception e)
            {
                return new ApiResponse<TResult>(new OperationResult(e));
            }
        }
        
    }
}