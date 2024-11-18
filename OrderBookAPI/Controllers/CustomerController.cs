using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderBookAPI.Data;
using OrderBookAPI.Models;
using OrderBookAPI.Services;

namespace OrderBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly OrderBookDBContext _context;
        private readonly EncodingService _encodingService;

        public CustomerController(OrderBookDBContext context, EncodingService encodingService)
        {
            _context = context;
            _encodingService = encodingService;
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
              

        // New endpoint to get all orders
        [HttpGet("GetCustomers")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetCustomers()
        {
            // Create a new HtmlSanitizer instance
            var sanitizer = new HtmlSanitizer();

            var customers = await _context.Customers
                .Select(customer => new CustomerDTO
                {
                    TelephoneNumber = customer.TelephoneNumber,
                    Address = _encodingService.HtmlEncode(customer.Address),
                    Email = _encodingService.HtmlEncode(customer.Email),
                    CustomerNote = _encodingService.HtmlEncode(customer.Note),
                    NameCustomer = _encodingService.HtmlEncode(customer.NameCustomer),
                    CustomerID = customer.CustomerID
                })
                .ToListAsync();           

            return Ok(customers);
        }

        [HttpGet("GetCustomerByName")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetCustomerByName(string name)
        {
            // Create a new HtmlSanitizer instance
            var sanitizer = new HtmlSanitizer();

            var customers = await _context.Customers
                .Where(m => !string.IsNullOrEmpty(name) && m.NameCustomer.ToLower().Contains(name.ToLower()))  // Case-insensitive partial match
                .Select(customer => new CustomerDTO
                {
                    TelephoneNumber = customer.TelephoneNumber,
                    Address = _encodingService.HtmlEncode(customer.Address),
                    Email = _encodingService.HtmlEncode(customer.Email),
                    CustomerNote = _encodingService.HtmlEncode(customer.Note),
                    NameCustomer = _encodingService.HtmlEncode(customer.NameCustomer)
                })
                .ToListAsync();

            return Ok(customers);
        }

        // GET: Delete order
        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);


            if (customer == null)
            {
                return NotFound();
            }

            // Save the order to the database
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // New endpoint for updating customer
        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDTO customerDTO)
        {
            

            var customer = await _context.Customers.FirstOrDefaultAsync(o => o.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            // Update customer details
            customer.TelephoneNumber = customerDTO.TelephoneNumber;
            customer.Address = customerDTO.Address;
            customer.Email = customerDTO.Email;
            customer.Note = customerDTO.CustomerNote;
            customer.NameCustomer = customerDTO.NameCustomer;            

            // Save changes to the database
            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}
