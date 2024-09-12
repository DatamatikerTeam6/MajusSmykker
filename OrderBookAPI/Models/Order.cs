namespace OrderBookAPI.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
        public PickupPlace PickupPlace { get; set; }
        public bool Delivered { get; set; }
        public string Image { get; set; }

    
    }
}
