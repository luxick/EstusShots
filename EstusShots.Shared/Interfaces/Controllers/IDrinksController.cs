using System.Threading.Tasks;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;

namespace EstusShots.Shared.Interfaces.Controllers
{
    public interface IDrinksController
    {
        /// <summary>
        /// Load all drinks from the database
        /// </summary>
        /// <param name="parameter">An <see cref="GetDrinksParameter"/> instance</param>
        /// <returns>An ApiResponse instance of type <see cref="GetDrinksResponse"/></returns>
        Task<ApiResponse<GetDrinksResponse>> GetDrinks(GetDrinksParameter parameter);

        /// <summary>
        /// Load detailed information on a single drink
        /// </summary>
        /// <param name="parameter">An <see cref="GetDrinkDetailsParameter"/> instance</param>
        /// <returns>An ApiResponse instance of type <see cref="GetDrinkDetailsResponse"/></returns>
        Task<ApiResponse<GetDrinkDetailsResponse>> GetDrinkDetails(GetDrinkDetailsParameter parameter);

        /// <summary>
        /// Creates or Updates a drink object
        /// </summary>
        /// <param name="parameter">An <see cref="SaveDrinkParameter"/> instance</param>
        /// <returns>An ApiResponse instance of type <see cref="SaveDrinkResponse"/></returns>
        Task<ApiResponse<SaveDrinkResponse>> SaveDrink(SaveDrinkParameter parameter);
    }
}