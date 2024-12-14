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

        // Get: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingTb>>> GetBooking()
        {
            return await _context.BookingTbs.ToListAsync();
        }

        // Post: api/Booking
        [HttpPost]
        public async Task<ActionResult<BookingTb>> PostBooking(BookingTb booking)
        {
            _context.BookingTbs.Add(booking);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBooking", new { Id = booking.BookingId }, booking);
        }

        // Get: api/Booking/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BookingTb>>> GetBookingsByUserId(int userId)
        {
            var bookings = await _context.BookingTbs
                                         .Where(b => b.UserId == userId)
                                         .ToListAsync();

            if (bookings == null || !bookings.Any())
            {
                return NotFound(new { Message = "No bookings found for this user." });
            }

            return bookings;
        }

        // Get: api/Booking/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingTb>> GetBooking(int id)
        {
            var booking = await _context.BookingTbs.FindAsync(id);
            if (booking == null)
            {
                return NotFound(new { Message = "Booking not found." });
            }
            return booking;
        }

        // Put: api/Booking/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutBooking(int id, BookingTb bookingTb)
        {
            if (id != bookingTb.BookingId)
            {
                return BadRequest(new { Message = "Booking ID mismatch." });
            }

            var existingBooking = await _context.BookingTbs.FindAsync(id);
            if (existingBooking == null)
            {
                return NotFound(new { Message = "Booking not found." });
            }

            // Only update booking status
            existingBooking.BookingStatus = bookingTb.BookingStatus;

            //existingBooking.RemainingTicket = bookingTb.RemainingTicket; 
            _context.Entry(existingBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingTbExists(id))
                {
                    return NotFound(new { Message = "Booking not found." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        } // Put: api/Booking/{id}
       

        private bool BookingTbExists(int id)
        {
            return _context.BookingTbs.Any(e => e.BookingId == id);
        }
    }
}
