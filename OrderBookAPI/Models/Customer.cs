namespace OrderBookAPI.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public int TelephoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string NameCustomer { get; set; }
        public bool ActiveCustomer { get; set; }  
    }
}
