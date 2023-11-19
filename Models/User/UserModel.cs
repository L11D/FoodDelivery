using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static FoodDelivery.Models.Enums;

namespace FoodDelivery.Models.User
{
    [Table("users")]
    public class UserModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("birth_date")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1900-01-01", "2100-01-01")]
        public DateTime BirthDate { get; set; }
        [Column("gender", TypeName = "text")]
        [EnumDataType(typeof(UserGender))]
        public UserGender Gender { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("address_id")]
        public Guid AddressId { get; set; }

        public UserModel(string fullName, string email, string password, DateTime birthDate, UserGender gender, string phoneNumber, Guid addressId)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email;
            Password = password;
            BirthDate = birthDate.ToUniversalTime();
            Gender = gender;
            PhoneNumber = phoneNumber;
            AddressId = addressId;
        }

        public UserModel() { }
    }
}
