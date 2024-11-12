//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using OrderBookAPI.Data;
//using OrderBookAPI.Models;
//using OrderBookAPI.Services;
//using System.Web;

//using Ganss.Xss;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace OrderBookAPI.Controllers
//{

//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrderController : ControllerBase
//    {
//        private readonly OrderBookDBContext _context;
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly EncodingService _encodingService;

//        public OrderController(OrderBookDBContext context, UserManager<ApplicationUser> userManager, EncodingService encodingService)  
//        { 
//            _context = context;
//            _userManager = userManager;
//            _encodingService = encodingService;
//        }

//        [HttpPost("CreateOrder")]
//        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO orderDTO) 
//        {
//            if(ModelState.IsValid) 
//            {
//                var Order = new Order
//                {                 
//                    Price = orderDTO.Price,
//                    nameOrder = _encodingService.HtmlEncode(orderDTO.Name),
//                    Type = orderDTO.Type,
//                    DeliveryDate = orderDTO.DeliveryDate,
//                    OrderDate = orderDTO.OrderDate,
//                    Quantity = orderDTO.Quantity,
//                    Note = _encodingService.HtmlEncode(orderDTO.Note),
//                    PickupPlace = orderDTO.PickupPlace,
//                    PickupPlaceAsString = orderDTO.PickupPlace.ToString(),
//                    Delivered = orderDTO.Delivered,
//                    Image = _encodingService.HtmlEncode(orderDTO.Image),
//                    CustomerID = orderDTO.CustomerID
//                };
//                _context.Orders.Add(Order);
//                await _context.SaveChangesAsync();
//                return Ok();
//            }   
//            else { return BadRequest(); }   
//        }

//        // New endpoint to get all orders
//        [HttpGet("GetOrders")]
//        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
//        {
//            // Create a new HtmlSanitizer instance
//            var sanitizer = new HtmlSanitizer();

//            var orders = await _context.Orders
//                .Select(order => new OrderDTO
//                {
//                    Price = order.Price,
//                    Name = _encodingService.HtmlEncode(order.nameOrder),  // Sanitize nameOrder
//                    Type = order.Type,
//                    DeliveryDate = order.DeliveryDate,
//                    OrderDate = order.OrderDate,  // Corrected to use OrderDate, not DeliveryDate
//                    Quantity = order.Quantity,
//                    Note = _encodingService.HtmlEncode(order.Note),  // Sanitize Note
//                    PickupPlace = order.PickupPlace,
//                    Delivered = order.Delivered,
//                    Image = _encodingService.HtmlEncode(order.Image),  // Sanitize Image
//                    CustomerID = order.CustomerID
//                })
//                .ToListAsync();

//            // Post-process the enum to get the name
//            foreach (var order in orders)
//            {                
//                order.PickupPlaceAsString = Enum.GetName(typeof(PickupPlace), order.PickupPlace); // Get enum name after fetching data
//            }

//            return Ok(orders);
//        }


//    }
//}
