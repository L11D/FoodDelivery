using static FoodDelivery.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Order
{
    public class OrderInfoDTO
    {
        public Guid Id { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public OrderStatus Status { get; set; }
        public double Price {  get; set; }  

        public OrderInfoDTO(OrderModel model, double price) 
        { 
            Id = model.Id;
            OrderTime = model.OrderTime;
            DeliveryTime = model.DeliveryTime;
            Status = model.Status;
            Price = price;
        }
    }
}
