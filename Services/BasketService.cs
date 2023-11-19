using FoodDelivery.Models;
using FoodDelivery.Models.Basket;
using FoodDelivery.Models.Dish;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FoodDelivery.Services
{
    public interface IBasketService
    {
        Task AddDish(Guid dishId, Guid userId);
        Task<List<DishBasketDTO>> GetBasket(Guid userId);
        Task DeleteDish(Guid dishId, Guid userId, bool increase);
        Task ClearBusket(Guid userId);
    }
    public class BasketService : IBasketService
    {
        private readonly FDDbContext _context;
        private readonly AppSettings _appSettings;

        public BasketService(FDDbContext context, AppSettings appSettings)
        {
            _context = context;
            _appSettings = appSettings;
        }

        public async Task<List<DishBasketDTO>> GetBasket(Guid userId)
        {
            List<BasketModel> baskets = await _context.Baskets.Where(b => b.UserId == userId).ToListAsync();
            List<DishBasketDTO> dishBasketDTOs = new List<DishBasketDTO>();

            foreach (var basket in baskets)
            {
                DishModel? dish = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == basket.DishId);
                if (dish == null)
                {
                    //throw new ExceptionWithStatusCode(404, "Dish not found");
                    throw new ExceptionWithStatusCode(_appSettings.Exeptions[4].Code, _appSettings.Exeptions[4].Message);
                }
                dishBasketDTOs.Add(new DishBasketDTO(dish, basket.Count));
            }

            return dishBasketDTOs;
        }

        public async Task AddDish(Guid dishId, Guid userId)
        {
            if (await _context.Dishes.FirstOrDefaultAsync(d => d.Id == dishId) == null)
            {
                //throw new ExceptionWithStatusCode(404, "Dish not found");
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[4].Code, _appSettings.Exeptions[4].Message);
            }

            BasketModel? basket = await _context.Baskets.FirstOrDefaultAsync(b => b.DishId == dishId && b.UserId == userId);
            if (basket == null)
            {
                await _context.Baskets.AddAsync(new BasketModel(userId, dishId));
                await _context.SaveChangesAsync();
            }
            else
            {
                basket.Add();
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDish(Guid dishId, Guid userId, bool increase)
        {
            if (await _context.Dishes.FirstOrDefaultAsync(d => d.Id == dishId) == null)
            {
                //throw new ExceptionWithStatusCode(404, "Dish not found");
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[4].Code, _appSettings.Exeptions[4].Message);
            }

            BasketModel? basket = await _context.Baskets.FirstOrDefaultAsync(b => b.DishId == dishId && b.UserId == userId);
            if (basket != null)
            {
                if (increase)
                {
                    basket.Decrease();
                    if (basket.Count < 1)
                    {
                        _context.Baskets.Remove(basket);
                    }
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Baskets.Remove(basket);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                //throw new ExceptionWithStatusCode(400, "Dish is not in basket");
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[5].Code, _appSettings.Exeptions[5].Message);
            }
        }

        public async Task ClearBusket(Guid userId)
        {
            List<BasketModel> baskets = await _context.Baskets.Where(b => b.UserId == userId).ToListAsync();
            foreach (var item in baskets)
            {
                _context.Baskets.Remove(item);
            }
            await _context.SaveChangesAsync();
        }
    }
}
