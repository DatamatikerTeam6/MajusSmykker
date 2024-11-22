using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderBookAPI.Data;
using OrderBookAPI.Models;
using OrderBookAPI.Services;
using System.Web;

namespace OrderBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly OrderBookDBContext _context;
        private readonly EncodingService _encodingService;
        private readonly SanitizeService _sanitizeService;

        public CustomerController(OrderBookDBContext context, EncodingService encodingService, SanitizeService sanitizeService)
        {
            _context = context;
            _encodingService = encodingService;
            _sanitizeService = sanitizeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                var Customer = new Customer
                {                  
                    TelephoneNumber = customerDTO.TelephoneNumber,
                    Address = _encodingService.CustomUrlEncode((customerDTO.Address)),
                    Email = _encodingService.CustomUrlEncode((customerDTO.Email)),
                    Note = _encodingService.CustomUrlEncode((customerDTO.CustomerNote)),
                    NameCustomer = _encodingService.CustomUrlEncode((customerDTO.NameCustomer))                                  
                };
                _context.Customers.Add(Customer);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else { return BadRequest(); }
        }
        
        [HttpGet("GetLastCustomerID")]
        public async Task<IActionResult> GetLastCustomerID()
        {
            // Find det senest oprettede ID
            var lastCreatedId = await _context.Customers
                .OrderByDescending(c => c.CustomerID) // Sorter efter ID i faldende rækkefølge
                .Select(c => c.CustomerID) // Vælg kun ID
                .FirstOrDefaultAsync(); // Tag den første eller standardværdi  

            var lastCreatedIdPlusOne = lastCreatedId += 1;
            // Returnér det fundne ID som JSON
            return Ok(new { lastCreatedIdPlusOne });
        }
              

        [HttpGet("GetCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
        // Fetch raw customers from the database
        var rawCustomers = await _context.Customers.ToListAsync();

        // Process and sanitize the data after fetching it
        var customers = rawCustomers
        .Select(customer => new CustomerDTO
        {
            TelephoneNumber = customer.TelephoneNumber,
            Address = _sanitizeService.SanitizeHtml(HttpUtility.HtmlDecode(customer.Address)),
            Email = _sanitizeService.SanitizeHtml(HttpUtility.HtmlDecode(customer.Email)),
            CustomerNote = _sanitizeService.SanitizeHtml(HttpUtility.HtmlDecode(customer.Note)),
            NameCustomer = _sanitizeService.SanitizeHtml(HttpUtility.HtmlDecode(customer.NameCustomer)),
            CustomerID = customer.CustomerID
        })
        .ToList();

        return Ok(customers);
        }


        [HttpGet("GetCustomerByName")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetCustomerByName(string name)
        {
            // Fetch raw customers from the database
            var rawCustomers = await _context.Customers.ToListAsync();
            
            var customers = rawCustomers
                .Where(m => !string.IsNullOrEmpty(name) && m.NameCustomer.ToLower().Contains(name.ToLower()))  // Case-insensitive partial match
                .Select(customer => new CustomerDTO
                {
                    TelephoneNumber = customer.TelephoneNumber,
                    Address = _encodingService.HtmlDecode(customer.Address),
                    Email = _encodingService.HtmlDecode(customer.Email),
                    CustomerNote = _encodingService.HtmlDecode(customer.Note),
                    NameCustomer = _encodingService.HtmlDecode(customer.NameCustomer),
                    CustomerID = customer.CustomerID
                })
                .ToList();

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
            customer.Address = _encodingService.CustomUrlDecode(customerDTO.Address);
            customer.Email = _encodingService.CustomUrlDecode(customerDTO.Email);
            customer.Note = _encodingService.CustomUrlDecode(customerDTO.CustomerNote);
            customer.NameCustomer = _encodingService.CustomUrlDecode(customerDTO.NameCustomer);            

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
