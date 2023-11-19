using FoodDelivery.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FoodDelivery.Models.Enums;

namespace FoodDelivery.Models.Dish
{
    [Table("dishes")]
    public class DishModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public double Price { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("is_vegetarian")]
        public bool IsVegetarian { get; set; }

        [Column("photo")]
        public string Photo {  get; set; }

        [Column("category", TypeName = "text")]
        [EnumDataType(typeof(DishCategory))]
        public DishCategory Category { get; set; }

        public DishModel(string name, double price, string description, bool isVegetarian, string photo, DishCategory category)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Description = description;
            IsVegetarian = isVegetarian;
            Photo = photo;
            Category = category;
        }

        public DishModel() { }
    }
}
