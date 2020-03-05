using System;
using System.Collections.Generic;
using EstusShots.Shared.Dto;
using EstusShots.Shared.Interfaces;

namespace EstusShots.Shared.Models.Parameters
{
    # region GetDrinks

    /// <summary>
    /// Parameter class for the GetDrinks API controller.
    /// </summary>
    public class GetDrinksParameter : IApiParameter
    {
        public GetDrinksParameter()
        {
        }
    }

    /// <summary>
    /// Parameter class returned from the GetDrinks API controller.
    /// </summary>
    public class GetDrinksResponse : IApiResponse
    {
        /// <summary>
        /// List of all drinks in the database
        /// </summary>
        public List<Drink> Drinks { get; set; }

        public GetDrinksResponse(List<Drink> drinks)
        {
            Drinks = drinks;
        }

        public GetDrinksResponse()
        {
        }
    }

    # endregion

    # region GetDrinkDetails

    /// <summary>
    /// Parameter class for the GetDrinkDetails API controller.
    /// </summary>
    public class GetDrinkDetailsParameter : IApiParameter
    {
        /// <summary>
        /// ID of the drink for which to load details
        /// </summary>
        public Guid DrinkId { get; set; }

        public GetDrinkDetailsParameter(Guid drinkId)
        {
            DrinkId = drinkId;
        }

        public GetDrinkDetailsParameter()
        {
        }
    }

    /// <summary>
    /// Parameter class returned from the GetDrinkDetails API controller.
    /// </summary>
    public class GetDrinkDetailsResponse : IApiResponse
    {
        /// <summary>
        /// Detailed information on a drink
        /// </summary>
        public Drink Drink { get; set; }

        public GetDrinkDetailsResponse(Drink drink)
        {
            Drink = drink;
        }

        public GetDrinkDetailsResponse()
        {
        }
    }

    # endregion

    # region SaveDrink

    /// <summary>
    /// Parameter class for the SaveDrink API controller.
    /// </summary>
    public class SaveDrinkParameter : IApiParameter
    {
        /// <summary>
        /// The object to save in the database
        /// </summary>
        public Drink Drink { get; set; }

        public SaveDrinkParameter(Drink drink)
        {
            Drink = drink;
        }

        public SaveDrinkParameter()
        {
        }
    }

    /// <summary>
    /// Parameter class returned from the SaveDrink API controller.
    /// </summary>
    public class SaveDrinkResponse : IApiResponse
    {
        /// <summary>
        /// ID of the created or updated drink object
        /// </summary>
        public Guid DrinkId { get; set; }

        public SaveDrinkResponse(Guid drinkId)
        {
            DrinkId = drinkId;
        }

        public SaveDrinkResponse()
        {
        }
    }

    # endregion
}