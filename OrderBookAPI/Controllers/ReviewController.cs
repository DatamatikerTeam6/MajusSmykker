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
    public class ReviewController : ControllerBase
    {

        private readonly OrderBookDBContext _context;

        public ReviewController(OrderBookDBContext context)
        {
            _context = context;        
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewDTO reviewDTO)
        {
            if (ModelState.IsValid)
            {
                var Review = new Review
                {
                    Star = reviewDTO.Star
                };
                _context.Reviews.Add(Review);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else { return BadRequest(); }
        }

        [HttpGet("GetReviews")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var rawReviews = await _context.Reviews.ToListAsync();

            var reviews = rawReviews
            .Select(review => new ReviewDTO
            {
                Star = review.Star
            })
            .ToList();

            return Ok(reviews);
        }

        // Read review from arduino
        // Post review to client
    }
}