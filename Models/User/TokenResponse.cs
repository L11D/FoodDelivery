using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.User
{
    public class TokenResponse
    {
        [Required(ErrorMessage = "Token является обязательным полем.")]
        public string Token { get; set; }

        public TokenResponse(string token) 
        {
            Token = token;
        }
    }
}
