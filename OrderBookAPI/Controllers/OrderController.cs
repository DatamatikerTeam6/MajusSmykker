using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderBookAPI.Data;
using OrderBookAPI.Models;
using OrderBookAPI.Services;
using System.Web;

using Ganss.Xss;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderBookDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EncodingService _encodingService;

        public OrderController(OrderBookDBContext context, UserManager<ApplicationUser> userManager, EncodingService encodingService)  
        { 
            _context = context;
            _userManager = userManager;
            _encodingService = encodingService;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromForm] OrderDTO orderDTO, [FromForm] List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                // Define the path for the images folder
                string imagesFolder = Path.Combine("C:\\Users\\Justin\\Datamatiker\\Programmering\\Kode\\MajusSmykker\\OrderBookAPI\\wwwroot\\images");
                Directory.CreateDirectory(imagesFolder);  // Ensure the images folder exists

                string imageUrl = "";  // Initialize imageUrl variable

                if (files != null && files.Count > 0)
                {
                    var formFile = files[0];  // Assuming a single image file for simplicity
                    if (formFile.Length > 0)
                    {
                        // Construct the full file path including the folder and file name
                        var filePath = Path.Combine(imagesFolder, formFile.FileName);

                        // Save the uploaded file to the file system
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }

                        // Set the image URL (relative path for serving the image)
                        imageUrl = $"/images/{formFile.FileName}";
                        Console.WriteLine($"Image uploaded: {filePath}");
                        Console.WriteLine($"Image URL: {imageUrl}");
                    }
                }

                // Create a new order object with the image URL and other details
                var order = new Order
                {
                    Price = orderDTO.Price,
                    nameOrder = _encodingService.HtmlEncode(orderDTO.Name),
                    Type = orderDTO.Type,
                    DeliveryDate = orderDTO.DeliveryDate,
                    OrderDate = orderDTO.OrderDate,
                    Quantity = orderDTO.Quantity,
                    Note = _encodingService.HtmlEncode(orderDTO.Note),
                    PickupPlace = orderDTO.PickupPlace,
                    PickupPlaceAsString = orderDTO.PickupPlace.ToString(),
                    Delivered = orderDTO.Delivered,
                    Image = imageUrl,  // Store the image URL
                    CustomerID = orderDTO.CustomerID
                };


                // Save the order to the database
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // New endpoint to get all orders
        [HttpGet("GetOrders")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            // Create a new HtmlSanitizer instance
            var sanitizer = new HtmlSanitizer();

            var orders = await _context.Orders
                .Select(order => new OrderDTO
                {
                    Price = order.Price,
                    Name = _encodingService.HtmlEncode(order.nameOrder),  // Sanitize nameOrder
                    Type = order.Type,
                    DeliveryDate = order.DeliveryDate,
                    OrderDate = order.OrderDate,  // Corrected to use OrderDate, not DeliveryDate
                    Quantity = order.Quantity,
                    Note = _encodingService.HtmlEncode(order.Note),  // Sanitize Note
                    PickupPlace = order.PickupPlace,
                    Delivered = order.Delivered,
                    //Image = _encodingService.HtmlEncode(order.Image),  // Sanitize Image
                    CustomerID = order.CustomerID
                })
                .ToListAsync();

            // Post-process the enum to get the name
            foreach (var order in orders)
            {                
                order.PickupPlaceAsString = Enum.GetName(typeof(PickupPlace), order.PickupPlace); // Get enum name after fetching data
            }

            return Ok(orders);
        }
    }
}
