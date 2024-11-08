namespace OrderBookAPI.Models
{
    public class VerifyOtpRequest
    {
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
    }
}
