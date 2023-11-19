using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models.Basket
{
    [Table("baskets")]
    public class BasketModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("dish_id")]
        public Guid DishId { get; set; }

        [Column("count")]
        public int Count { get; set; }

        public BasketModel(Guid userId, Guid dishId) 
        {
            Id = Guid.NewGuid();
            UserId = userId;
            DishId = dishId;
            Count = 1;
        }

        public void Add()
        {
            Count++;
        }

        public void Decrease()
        {
            Count--;
        }
    }
}
