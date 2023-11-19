using System.ComponentModel.DataAnnotations;
using static FoodDelivery.Models.Enums;

namespace FoodDelivery.Models.User
{
    public class UserEditModel
    {
        [Required(ErrorMessage = "Имя является обязательным полем.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Дата рождения является обязательным полем.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Пол является обязательным полем.")]
        public UserGender Gender { get; set; }

        [Phone(ErrorMessage = "Некорректный формат номера телефона.")]
        public string PhoneNumber { get; set; }
        public Guid AddressId { get; set; }

        public UserEditModel(string fullName, DateTime birthDate, UserGender gender, string phoneNumber, Guid addressId)
        {
            FullName = fullName;
            BirthDate = birthDate;
            Gender = gender;
            PhoneNumber = phoneNumber;
            AddressId = addressId;
        }
    }
}
