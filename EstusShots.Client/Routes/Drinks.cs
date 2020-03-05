using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;

namespace EstusShots.Client.Routes
{
    /// <inheritdoc/>
    public class Drinks : IDrinksController
    {
        private readonly EstusShotsClient _client;

        public Drinks(EstusShotsClient client)
        {
            _client = client;
        }
        
        private string ActionUrl([CallerMemberName]string caller = "") =>
            $"{_client.ApiUrl}{GetType().Name}/{caller}";
        
        /// <inheritdoc/>
        public async Task<ApiResponse<GetDrinksResponse>> GetDrinks(GetDrinksParameter parameter)
            => await _client.PostToApi<GetDrinksResponse, GetDrinksParameter>(ActionUrl(), parameter);
        
        /// <inheritdoc/>
        public async Task<ApiResponse<GetDrinkDetailsResponse>> GetDrinkDetails(GetDrinkDetailsParameter parameter)
            => await _client.PostToApi<GetDrinkDetailsResponse, GetDrinkDetailsParameter>(ActionUrl(), parameter);

        /// <inheritdoc/>
        public async Task<ApiResponse<SaveDrinkResponse>> SaveDrink(SaveDrinkParameter parameter)
            => await _client.PostToApi<SaveDrinkResponse, SaveDrinkParameter>(ActionUrl(), parameter);
    }
}