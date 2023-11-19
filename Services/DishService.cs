using FoodDelivery.Models;
using FoodDelivery.Models.Dish;
using FoodDelivery.Models.Order;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using static FoodDelivery.Models.Enums;

namespace FoodDelivery.Services
{
    public interface IDishService
    {
        Task<DishDTO> GetDishById(Guid id);
        Task<DishPagedListDTO> GetDishPagedList(DishCategory[]? categories, bool? vegetarian, SortingTypes? sorting, int pageSize, int page);
        Task<bool> ChekDishAbleToRaiting(Guid dishId, Guid userId);
        Task RateDish(Guid dishId, Guid userId, int score);
    }

    public class DishService : IDishService
    {
        private readonly FDDbContext _context;
        private readonly AppSettings _appSettings;

        public DishService(FDDbContext context, AppSettings appSettings)
        {
            _context = context;
            _appSettings = appSettings;

        }

        public async Task RateDish(Guid dishId, Guid userId, int score)
        {
            if (await ChekDishAlreadyRaited(dishId, userId))
            {
                if (await ChekDishAbleToRaiting(dishId, userId))
                {
                    await _context.DishRaitings.AddAsync(new DishRaitingModel(userId, dishId, score));
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //throw new ExceptionWithStatusCode(403, "User has never ordered a dish");
                    throw new ExceptionWithStatusCode(_appSettings.Exeptions[6].Code, _appSettings.Exeptions[6].Message);
                }
            }
            else
            {
                //throw new ExceptionWithStatusCode(403, "Dish is alredy raited");
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[7].Code, _appSettings.Exeptions[7].Message);
            }
        }

        public async Task<bool> ChekDishAbleToRaiting(Guid dishId, Guid userId)
        {
            List<Guid> userOrdersIds = await _context.Orders.Where(o => o.UserId == userId && o.Status == OrderStatus.Delivered).Select(o => o.Id).ToListAsync();

            List<Guid> userOrderedDishesIds = 
                await _context.OrderToDishes
                .Where(o => userOrdersIds.Contains(o.OrderId))
                .Select(o => o.DishId).ToListAsync();   

            if (userOrderedDishesIds.Contains(dishId))
            {
                return true;
            }
            else return false;
        }

        private async Task<bool> ChekDishAlreadyRaited(Guid dishId, Guid userId)
        {
            List<DishRaitingModel> dishRaitings = await _context.DishRaitings.Where(r => r.DishId == dishId && r.UserId == userId).ToListAsync();
            if (dishRaitings.Count == 0)
            {
                return true;
            }
            else return false;
        }

        public async Task<DishPagedListDTO> GetDishPagedList(DishCategory[] categories, bool? vegetarian, SortingTypes? sorting, int pageSize, int page)
        {
            List<DishModel> dishModels = new List<DishModel>();

            if (categories.Length != 0)
            {
                dishModels = await _context.Dishes.Where(d => categories.Contains(d.Category)).ToListAsync();
            }
            else
            {
                dishModels = await _context.Dishes.ToListAsync();
            }

            if (vegetarian != null)
            {
                dishModels = dishModels.Where(d => d.IsVegetarian == vegetarian).ToList();
            }

            List<DishDTO> dishDTOs = new List<DishDTO>();
            foreach(DishModel d in dishModels)
            {
                dishDTOs.Add(new DishDTO(d, await solveRaiting(d.Id)));
            }

            if (sorting != null)
            {
                switch (sorting)
                {
                    case SortingTypes.NameAsc:
                        dishDTOs = dishDTOs.OrderBy(d => d.Name).ToList();
                        break;
                    case SortingTypes.NameDesc:
                        dishDTOs = dishDTOs.OrderByDescending(d => d.Name).ToList();
                        break;
                    case SortingTypes.PriceAsc:
                        dishDTOs = dishDTOs.OrderBy(d => d.Price).ToList();
                        break;
                    case SortingTypes.PriceDesc:
                        dishDTOs = dishDTOs.OrderByDescending(d => d.Price).ToList();
                        break;
                    case SortingTypes.RatingAsc:
                        dishDTOs = dishDTOs.OrderBy(d => d.Raiting).ToList();
                        break;
                    case SortingTypes.RatingDesc:
                        dishDTOs = dishDTOs.OrderByDescending(d => d.Raiting).ToList();
                        break;
                }
            }

            int totalItems = dishDTOs.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            int skip = (page - 1) * pageSize;
            dishDTOs = dishDTOs.Skip(skip).Take(pageSize).ToList();

            return new DishPagedListDTO(dishDTOs, new PageInfoModel(dishDTOs.Count, totalPages, page));
        }

        public async Task<DishDTO> GetDishById(Guid id)
        {
            DishModel? dish = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == id);

            if (dish != null)
            {
                return new DishDTO(dish, await solveRaiting(id));
            }
            else
            {
                //throw new ExceptionWithStatusCode(404, "Dish not found");
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[4].Code, _appSettings.Exeptions[4].Message);
            }
        }

        private async Task<double?> solveRaiting(Guid id)
        {
            List<DishRaitingModel> dishRaitings = await _context.DishRaitings.Where(d => d.DishId == id).ToListAsync();

            int sum = 0, count = 0;
            foreach (var d in dishRaitings)
            {
                sum += d.Score;
                count++;
            }

            if (count != 0)
            {
                return (double)sum / count;
            }
            else return null;
        }
    }
}
