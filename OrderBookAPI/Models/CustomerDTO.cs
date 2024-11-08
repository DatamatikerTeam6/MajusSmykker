using System.ComponentModel.DataAnnotations;

namespace OrderBookAPI.Models
{
    public class CustomerDTO
    {
        [Required(ErrorMessage = "Telephone number is required.")]
        [RegularExpression(@"^\d{8,15}$", ErrorMessage = "Telephone number must be between 8 and 15 digits.")]
        public int TelephoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s,.-]*$", ErrorMessage = "Address cannot contain special characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email cannot contain special characters.")]
        public string Email { get; set; }

        [StringLength(500, ErrorMessage = "Customer note cannot be longer than 500 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Customer note cannot contain special characters.")]
        public string CustomerNote { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name cannot be longer than 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Customer name cannot contain special characters.")]
        public string NameCustomer { get; set; }
    }
}
