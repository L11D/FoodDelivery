namespace FoodDelivery.Models
{
    public class ExceptionWithStatusCode : Exception
    {
        public int StatusCode { get; set; }
        public ExceptionWithStatusCode(int statusCode, string? message) : base(message) 
        {
            StatusCode = statusCode;
        }
    }
}
