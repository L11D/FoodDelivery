using static FoodDelivery.Models.Enums;

namespace FoodDelivery.Models.Dish
{
    public class DishDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsVegetarian { get; set; }
        public string Photo { get; set; }
        public DishCategory Category { get; set; }
        public double? Raiting { get; set; }

        public DishDTO(DishModel model, double? raiting)
        {
            Id = model.Id;
            Name = model.Name;
            Price = model.Price;
            Description = model.Description;
            IsVegetarian = model.IsVegetarian;
            Photo = model.Photo;
            Category = model.Category;
            Raiting = raiting;
        }
    }
}
