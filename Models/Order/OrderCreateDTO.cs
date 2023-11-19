using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models.Order
{
    public class OrderCreateDTO
    {
        public DateTime DeliveryTime { get; set; }

        public Guid AddressId { get; set; }

        public OrderCreateDTO(DateTime deliveryTime, Guid addressId) 
        {
            DeliveryTime = deliveryTime;
            AddressId = addressId;
        }
    }
}
