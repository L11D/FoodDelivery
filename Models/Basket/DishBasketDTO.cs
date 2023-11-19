using FoodDelivery.Models.Dish;

namespace FoodDelivery.Models.Basket
{
    public class DishBasketDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double TotalPrice {  get; set; }
        public int Count { get; set; } 
        public string Photo { get; set; }

        public DishBasketDTO(DishModel model, int count) 
        { 
            Id = model.Id;
            Name = model.Name;
            Price = model.Price;
            Photo = model.Photo;
            Count = count;
            TotalPrice = Count * Price;
        }
    }
}
