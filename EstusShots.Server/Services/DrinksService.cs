using System.Threading.Tasks;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;

namespace EstusShots.Server.Services
{
    public class DrinksService : IDrinksController
    {
        public Task<ApiResponse<GetDrinksResponse>> GetDrinks(GetDrinksParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<GetDrinkDetailsResponse>> GetDrinkDetails(GetDrinkDetailsParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<SaveDrinkResponse>> SaveDrink(SaveDrinkParameter parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}