using static FoodDelivery.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FoodDelivery.Models.Basket;

namespace FoodDelivery.Models.Order
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public OrderStatus Status { get; set; }
        public double Price { get; set; }
        public List<DishBasketDTO> Dishes { get; set; }
        public Guid Address { get; set; }

        public OrderDTO(OrderModel model, List<DishBasketDTO> dishes, double price)
        {
            Id = model.Id;
            OrderTime = model.OrderTime.ToUniversalTime();
            DeliveryTime = model.DeliveryTime.ToUniversalTime();
            Address = model.Address;
            Status = model.Status;
            Price = price;
            Dishes = dishes;
        }
    }
}
