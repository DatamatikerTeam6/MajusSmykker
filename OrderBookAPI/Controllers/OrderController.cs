using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderBookAPI.Data;
using OrderBookAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderBookDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(OrderBookDBContext context, UserManager<ApplicationUser> userManager)  
        { 
            _context = context;
            _userManager = userManager; 
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO orderDTO) 
        {
            if(ModelState.IsValid) 
            {
                var Order = new Order
                {
                    OrderID = orderDTO.OrderID,
                    Price = orderDTO.Price,
                    Name = orderDTO.Name,
                    Type = orderDTO.Type,
                    DeliveryDate = orderDTO.DeliveryDate,
                    OrderDate = orderDTO.OrderDate,
                    Quantity = orderDTO.Quantity,
                    Note = orderDTO.Note,
                    PickupPlace = orderDTO.PickupPlace,
                    Delivered = orderDTO.Delivered,
                    Image = orderDTO.Image
                };
                _context.Orders.Add(Order);
                await _context.SaveChangesAsync();
                return Ok();
            }   
            else { return BadRequest(); }   
        }
    }
}
