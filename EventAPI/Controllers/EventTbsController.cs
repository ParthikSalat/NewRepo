using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventAPI.Models;
using System.IO;
using Microsoft.Extensions.FileProviders; // Add this to Program.cs for serving static files


namespace EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTbsController : ControllerBase
    {
        private readonly EventDbContext _context;

        public EventTbsController(EventDbContext context)
        {
            _context = context;
        }

        // GET: api/EventTbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventTb>>> GetEventTbs()
        {
            return await _context.EventTbs.ToListAsync();
        }

        // GET: api/EventTbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventTb>> GetEventTb(int id)
        {
            var eventTb = await _context.EventTbs.FindAsync(id);

            if (eventTb == null)
            {
                return NotFound();
            }

            return eventTb;
        }

        // PUT: api/EventTbs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventTb(int id, EventTb eventTb)
        {
            if (id != eventTb.EventId)
            {
                return BadRequest();
            }

            _context.Entry(eventTb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventTbExists(id))
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

        [HttpPost]
        public async Task<ActionResult<EventTb>> PostEventTb([FromForm] EventTb eventTb, IFormFile eventImage)
        {
            if (eventImage != null)
            {
                // Ensure the "Uploads" directory exists
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                // Create a unique filename for the image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(eventImage.FileName);
                var filePath = Path.Combine(uploadDirectory, fileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await eventImage.CopyToAsync(stream);
                }

                // Store only the filename in the EventImage property
                eventTb.EventImage = fileName;
            }

            // Add the event to the database and save changes
            _context.EventTbs.Add(eventTb);
            await _context.SaveChangesAsync();

            // Return the created event with the proper URL for accessing it
            return CreatedAtAction("GetEventTb", new { id = eventTb.EventId }, eventTb);
        }


        // DELETE: api/EventTbs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventTb(int id)
        {
            var eventTb = await _context.EventTbs.FindAsync(id);
            if (eventTb == null)
            {
                return NotFound();
            }

            _context.EventTbs.Remove(eventTb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventTbExists(int id)
        {
            return _context.EventTbs.Any(e => e.EventId == id);
        }
    }
}
