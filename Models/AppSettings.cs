using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FoodDelivery.Models
{
    public class ExceptionSettings
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class AppSettings
    {
        public string JwtKey { get; set; }
        public int JwtKeyLifeTime { get; set; }
        public int MinOrderTimeInMinuts {  get; set; }
        public List<ExceptionSettings> Exeptions { get; set; }
    }
}
