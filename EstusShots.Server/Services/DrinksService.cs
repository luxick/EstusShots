using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EstusShots.Server.Models;
using EstusShots.Shared.Interfaces;
using EstusShots.Shared.Interfaces.Controllers;
using EstusShots.Shared.Models;
using EstusShots.Shared.Models.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Dto = EstusShots.Shared.Dto;

namespace EstusShots.Server.Services
{
    public class DrinksService : IDrinksController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly EstusShotsContext _context;

        public DrinksService(ILogger<DrinksService> logger, IMapper mapper, EstusShotsContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ApiResponse<GetDrinksResponse>> GetDrinks(GetDrinksParameter parameter)
        {
            var drinks = await _context.Drinks.ToListAsync();
            var dtos = _mapper.Map<List<Dto.Drink>>(drinks);
            return new ApiResponse<GetDrinksResponse>(new GetDrinksResponse(dtos));
        }

        public async Task<ApiResponse<GetDrinkDetailsResponse>> GetDrinkDetails(GetDrinkDetailsParameter parameter)
        {
            var drink = await _context.Drinks.FindAsync(parameter.DrinkId);
            if (drink == null)
            {
                _logger.LogWarning($"Drink with ID '{parameter.DrinkId}' was not found in database");
                return new ApiResponse<GetDrinkDetailsResponse>(new OperationResult(false, "Object not found"));
            }

            var dto = _mapper.Map<Dto.Drink>(drink);
            return new ApiResponse<GetDrinkDetailsResponse>(new GetDrinkDetailsResponse(dto));
        }

        public async Task<ApiResponse<SaveDrinkResponse>> SaveDrink(SaveDrinkParameter parameter)
        {
            if (parameter.Drink.DrinkId.IsEmpty())
            {
                var drink = _mapper.Map<Drink>(parameter.Drink);
                _context.Drinks.Add(drink);
                var count = await _context.SaveChangesAsync();
                _logger.LogInformation($"Created drink '{drink.DrinkId}' ({count} rows updated)");
                return new ApiResponse<SaveDrinkResponse>(new SaveDrinkResponse(drink.DrinkId));
            }
            else
            {
                var drink = await _context.Drinks.FindAsync(parameter.Drink.DrinkId);
                if (drink == null)
                {
                    _logger.LogError($"Cannot update drink '{parameter.Drink.DrinkId}'. Not in database.");
                    return new ApiResponse<SaveDrinkResponse>(new OperationResult(false, "Object not found"));
                }
                _context.Drinks.Update(drink);
                _mapper.Map(parameter.Drink, drink);
                var count = await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated drink '{drink.DrinkId}' ({count} rows updated)");
                return new ApiResponse<SaveDrinkResponse>(new SaveDrinkResponse(drink.DrinkId));
            }
        }
    }
}