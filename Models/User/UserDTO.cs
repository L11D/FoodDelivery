using System.ComponentModel.DataAnnotations;
using static FoodDelivery.Models.Enums;

namespace FoodDelivery.Models.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public UserGender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public Guid AddressId { get; set; }

        public UserDTO(Guid id, string fullName, string email, DateTime birthDate, UserGender gender, string phoneNumber, Guid addressId)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Gender = gender;
            PhoneNumber = phoneNumber;
            AddressId = addressId;
        }
    }
}
