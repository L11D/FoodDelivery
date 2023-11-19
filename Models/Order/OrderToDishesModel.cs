using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models.Order
{
    [Table("order_to_dishes")]
    public class OrderToDishesModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("order_id")]
        public Guid OrderId { get; set; }

        [Column("dish_id")]
        public Guid DishId { get; set; }

        [Column("count")]
        public int Count { get; set; }

        public OrderToDishesModel(Guid orderId, Guid dishId, int count)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            DishId = dishId;
            Count = count;  
        }
    }
}
