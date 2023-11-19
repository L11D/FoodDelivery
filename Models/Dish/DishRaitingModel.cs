using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Dish
{
    [Table("dish_raiting")]
    public class DishRaitingModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("dish_id")]
        public Guid DishId { get; set; }

        [Range(1, 5)]
        [Column("score")]
        public int Score { get; set; }

        public DishRaitingModel(Guid userId, Guid dishId, int score) 
        {
            Id = Guid.NewGuid();
            UserId = userId;
            DishId = dishId;
            Score = score;
        }
        public DishRaitingModel() { }
    }
}
