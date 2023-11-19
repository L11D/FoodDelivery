using static FoodDelivery.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Order
{
    [Table("orders")]
    public class OrderModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("order_time")]
        //[DataType(DataType.)]
        public DateTime OrderTime { get; set; }

        [Column("delivery_time")]
        //[DataType(DataType.)]
        public DateTime DeliveryTime { get; set; }

        [Column("status", TypeName = "text")]
        public OrderStatus Status { get; set; }

        [Column("address")]
        public Guid Address { get; set; }

        public OrderModel(Guid userId, DateTime orderTime, DateTime deliveryTime, Guid address)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            OrderTime = orderTime.ToUniversalTime();
            DeliveryTime = deliveryTime.ToUniversalTime();
            Address = address;
            Status = OrderStatus.InProgress;
        }
    }
}
