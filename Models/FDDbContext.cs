using Microsoft.EntityFrameworkCore;
using FoodDelivery.Models.User;
using FoodDelivery.Models.Dish;
using FoodDelivery.Models.Basket;
using FoodDelivery.Models.Order;
using FoodDelivery.Models.Address;

namespace FoodDelivery.Models
{
    public class FDDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<BannedToken> BannedTokens { get; set; }
        public DbSet<DishModel> Dishes { get; set; }
        public DbSet<DishRaitingModel> DishRaitings { get; set; }
        public DbSet<BasketModel> Baskets { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderToDishesModel> OrderToDishes { get; set; }
        public DbSet<HouseModel> AsHouses { get; set; }
        public DbSet<HierarchyModel> Hierarchies { get; set; }
        public DbSet<AddressElementModel> AddressElements { get; set; }

        public FDDbContext(DbContextOptions<FDDbContext> options) : base(options)
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception)
            {
                throw new InvalidDataException("DB is unavailable");
            }
        }
    }
}
