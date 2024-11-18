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
        private readonly string _imagesFolderPath;
        private readonly string _baseImagesFolderPath;

        public OrderController(OrderBookDBContext context, UserManager<ApplicationUser> userManager, EncodingService encodingService, IConfiguration configuration)  
        { 
            _context = context;
            _userManager = userManager;
            _encodingService = encodingService;
            _imagesFolderPath = configuration["ImageSettings:ImagesFolderPath"];
            _baseImagesFolderPath = configuration["ImageSettings:BaseImagesFolderPath"];
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromForm] OrderDTO orderDTO, [FromForm] List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                Directory.CreateDirectory(_imagesFolderPath);  // Ensure the images folder exists

                string imageUrl = "";  // Initialize imageUrl variable

                if (files != null && files.Count > 0)
                {
                    var formFile = files[0];  // Assuming a single image file for simplicity
                    if (formFile.Length > 0)
                    {
                        // Construct the full file path including the folder and file name
                        var filePath = Path.Combine(_imagesFolderPath, formFile.FileName);

                        // Save the uploaded file to the file system
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                  
                        // Set the image URL (relative path for serving the image)
                        imageUrl = $"{_baseImagesFolderPath}{formFile.FileName}";
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
                    Image = _encodingService.HtmlEncode(order.Image),  // Sanitize Image
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

        // New endpoint to get all orders
        [HttpGet("GetOrdersByName")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByName(string name)
        {
            // Create a new HtmlSanitizer instance
            var sanitizer = new HtmlSanitizer();

            // Use Contains for partial match (case-insensitive)
            var orders = await _context.Orders
                .Where(m => !string.IsNullOrEmpty(name) && m.nameOrder.ToLower().Contains(name.ToLower()))  // Case-insensitive partial match
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
                    Image = _encodingService.HtmlEncode(order.Image),  // Sanitize Image
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


        // GET: Delete order
        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(string? name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.nameOrder == name);
            
            
            if (order == null)
            {
                return NotFound();
            }

            // Save the order to the database
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT: Update order
        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromForm] string name, [FromForm] OrderDTO orderDTO, [FromForm] List<IFormFile> files)
        {
            if (name == null)
            {
                return BadRequest("Order name must be provided.");
            }

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.nameOrder == name);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            // Define the path for the images folder
            string imagesFolder = Path.Combine("C:\\Users\\Justin\\Datamatiker\\Programmering\\Kode\\MajusSmykker\\OrderBookAPI\\wwwroot\\images");
            Directory.CreateDirectory(imagesFolder); // Ensure the images folder exists

            string imageUrl = order.Image; // Default to existing image URL

            // Update image if a new one is provided
            if (files != null && files.Count > 0)
            {
                var formFile = files[0]; // Assuming a single image file for simplicity
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(imagesFolder, formFile.FileName);

                    // Save the uploaded file to the file system
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    // Set the new image URL (relative path for serving the image)
                    imageUrl = $"https://localhost:7187/images/{formFile.FileName}";
                }
            }

            // Update order details
            order.Price = orderDTO.Price;
            order.nameOrder = _encodingService.HtmlEncode(orderDTO.Name);
            order.Type = orderDTO.Type;
            order.DeliveryDate = orderDTO.DeliveryDate;
            order.OrderDate = orderDTO.OrderDate;
            order.Quantity = orderDTO.Quantity;
            order.Note = _encodingService.HtmlEncode(orderDTO.Note);
            order.PickupPlace = orderDTO.PickupPlace;
            order.PickupPlaceAsString = orderDTO.PickupPlace.ToString();
            order.Delivered = orderDTO.Delivered;
            order.Image = imageUrl; // Update the image URL if a new one was uploaded
            order.CustomerID = orderDTO.CustomerID;

            // Save changes to the database
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return Ok("Order updated successfully.");
        }



    }
}
