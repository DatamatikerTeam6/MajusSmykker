using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderBookAPI.Models
{
    public class ReviewDTO
    {
        [JsonPropertyName("star")]
        public int Star { get; set; }
    }
}