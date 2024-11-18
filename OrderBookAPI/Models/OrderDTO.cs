using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderBookAPI.Models
{
    public class OrderDTO
    {
        [JsonPropertyName("price")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public double Price { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Name cannot contain special characters.")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        [Required(ErrorMessage = "Type is required.")]
        public Type Type { get; set; }

        [JsonPropertyName("deliverydate")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Delivery date is required.")]
        public DateTime DeliveryDate { get; set; }

        [JsonPropertyName("orderdate")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Order date is required.")]
        public DateTime OrderDate { get; set; }

        [JsonPropertyName("quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [JsonPropertyName("note")]
        [StringLength(500, ErrorMessage = "Note cannot be longer than 500 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Note cannot contain special characters.")]
        public string Note { get; set; }

        [JsonPropertyName("pickupplace")]
        [Required(ErrorMessage = "Pickup place is required.")]
        public PickupPlace PickupPlace { get; set; }

        [JsonPropertyName("pickupplaceasstring")]
        [StringLength(100, ErrorMessage = "Pickup place description cannot be longer than 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Pickup place description cannot contain special characters.")]
        public string PickupPlaceAsString { get; set; }

        [JsonPropertyName("delivered")]
        public bool Delivered { get; set; }
 

        [JsonPropertyName("customerid")]
        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerID { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }
    }
}
