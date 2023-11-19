using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models.User
{
    [Table("banned_tokens")]
    public class BannedToken
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("token")]
        public string Token { get; set; }

        public BannedToken(string token) 
        { 
            Id = Guid.NewGuid();
            Token = token;
        }
    }
}
