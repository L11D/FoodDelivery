namespace FoodDelivery.Models
{
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public Response(string staus, string message) 
        {
            Status = staus;
            Message = message;
        }

        public Response(string message)
        {
            Status = "error";
            Message = message;
        }
    }
}
