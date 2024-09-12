using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderBookAPI.Models
{
    public class OrderDTO
    {
        [JsonPropertyName("orderid")]
        public int OrderID { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public Type Type { get; set; }

        [JsonPropertyName("deliverydate")]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; } 

        [JsonPropertyName("orderdate")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("pickupplace")]
        public PickupPlace PickupPlace { get; set; }

        [JsonPropertyName("delivered")]
        public bool Delivered { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

    }
}
