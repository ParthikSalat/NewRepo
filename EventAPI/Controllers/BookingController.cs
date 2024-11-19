using EventAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly EventDbContext _context;

        public BookingController(EventDbContext context)
        {
            _context = context;
        }

        //Get:api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingTb>>> GetBooking()
        {
            return await _context.BookingTbs.ToListAsync();
        }

        //Post:api/Booking
        [HttpPost]
        public async Task<ActionResult<BookingTb>> PostBooking(BookingTb booking)
        {
            _context.BookingTbs.Add(booking);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBooking", new { Id = booking.BookingId }, booking);
        }


      
    }
}
