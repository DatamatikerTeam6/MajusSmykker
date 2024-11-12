using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderBookAPI.Data;
using OrderBookAPI.Models;

namespace OrderBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly OrderBookDBContext _context;

        public CustomerController(OrderBookDBContext context)
        {
            _context = context;            
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                var Customer = new Customer
                {                  
                    TelephoneNumber = customerDTO.TelephoneNumber,
                    Address = customerDTO.Address,
                    Email = customerDTO.Email,
                    Note = customerDTO.CustomerNote,
                    NameCustomer = customerDTO.NameCustomer                                     
                };
                _context.Customers.Add(Customer);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else { return BadRequest(); }
        }
        
        [HttpGet("numberofspaces")]
        public async Task<IActionResult> GetNumberofSpaces()
        {
            // Count the number of customers in the database
            int count = await _context.Customers.CountAsync();

            // Return the count as JSON
            return Ok(new { count });
        }

    }
}