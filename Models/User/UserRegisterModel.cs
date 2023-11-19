using System.ComponentModel.DataAnnotations;
using static FoodDelivery.Models.Enums;

namespace FoodDelivery.Models.User
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Имя является обязательным полем.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email является обязательным полем.")]
        [EmailAddress(ErrorMessage = "Некорректный формат адреса электронной почты.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль является обязательным полем.")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать не менее 6 символов.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Дата рождения является обязательным полем.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Пол является обязательным полем.")]
        public UserGender Gender { get; set; }

        [Phone(ErrorMessage = "Некорректный формат номера телефона.")]
        public string PhoneNumber { get; set; }
        public Guid AddressId { get; set; }

        public UserRegisterModel(string fullName, string email, string password, DateTime birthDate, UserGender gender, string phoneNumber, Guid addressId)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            BirthDate = birthDate;
            Gender = gender;
            PhoneNumber = phoneNumber;
            AddressId = addressId;
        }
    }
}
