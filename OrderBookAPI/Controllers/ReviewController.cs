using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderBookAPI.Data;
using OrderBookAPI.Models;

namespace OrderBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly OrderBookDBContext _context;

        public ReviewController(OrderBookDBContext context)
        {
            _context = context;            
        }

        // Read review from arduino
        // Post review to client
    }
}