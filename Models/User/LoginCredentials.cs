using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.User
{
    public class LoginCredentials
    {
        [Required(ErrorMessage = "Email является обязательным полем.")]
        [EmailAddress(ErrorMessage = "Некорректный формат адреса электронной почты.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль является обязательным полем.")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать не менее 6 символов.")]
        public string Password { get; set; }

        public LoginCredentials(string email, string password) 
        {
            Email = email;
            Password = password;    
        }
    }
}
