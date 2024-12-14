using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventAPI.Models;

namespace EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketTbsController : ControllerBase
    {
        private readonly EventDbContext _context;

        public TicketTbsController(EventDbContext context)
        {
            _context = context;
        }

        // GET: api/TicketTbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketTb>>> GetTicketTbs()
        {
            return await _context.TicketTbs.ToListAsync();
        }

        // GET: api/TicketTbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketTb>> GetTicketTb(int id)
        {
            var ticketTb = await _context.TicketTbs.FindAsync(id);

            if (ticketTb == null)
            {
                return NotFound();
            }

            return ticketTb;
        }
        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<TicketTb>> GetTicketByBookingId(int bookingId)
        {
            var ticketTb = await _context.TicketTbs
                                         .FirstOrDefaultAsync(t => t.BookingId == bookingId);

            if (ticketTb == null)
            {
                return NotFound(new { message = $"No ticket found with bookingId {bookingId}" });
            }

            return Ok(ticketTb);
        }


        // PUT: api/TicketTbs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketTb(int id, TicketTb ticketTb)
        {
            if (id != ticketTb.TicketId)
            {
                return BadRequest();
            }

            _context.Entry(ticketTb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketTbExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TicketTbs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketTb>> PostTicketTb(TicketTb ticketTb)
        {
            _context.TicketTbs.Add(ticketTb);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketTbExists(ticketTb.TicketId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTicketTb", new { id = ticketTb.TicketId }, ticketTb);
        }

        // DELETE: api/TicketTbs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketTb(int id)
        {
            var ticketTb = await _context.TicketTbs.FindAsync(id);
            if (ticketTb == null)
            {
                return NotFound();
            }

            _context.TicketTbs.Remove(ticketTb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketTbExists(int id)
        {
            return _context.TicketTbs.Any(e => e.TicketId == id);
        }
    }
}
