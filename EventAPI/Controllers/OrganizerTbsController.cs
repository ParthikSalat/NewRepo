using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerTbsController : ControllerBase
    {
        private readonly EventDbContext _context;

        public OrganizerTbsController(EventDbContext context)
        {
            _context = context;
        }
        [HttpPost("login")]
        public async Task<ActionResult>login(string email, string password)
        {
            var a=await _context.OrganizerTbs.FirstOrDefaultAsync(o=>o.OrganizerEmail== email && o.OrganizerPassword==password);
            if(a==null)
            {
                return NotFound();
            }
            return Ok(a);
        }
        // GET: api/OrganizerTbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizerTb>>> GetOrganizerTbs()
        {
            return await _context.OrganizerTbs.ToListAsync();
        }

        // GET: api/OrganizerTbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizerTb>> GetOrganizerTb(int id)
        {
            var organizerTb = await _context.OrganizerTbs.FindAsync(id);

            if (organizerTb == null)
            {
                return NotFound();
            }

            return organizerTb;
        }

        // PUT: api/OrganizerTbs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizerTb(int id, OrganizerTb organizerTb)
        {
            if (id != organizerTb.OrganizerId)
            {
                return BadRequest();
            }

            _context.Entry(organizerTb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizerTbExists(id))
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

        // POST: api/OrganizerTbs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrganizerTb>> PostOrganizerTb(OrganizerTb organizerTb)
        {
            _context.OrganizerTbs.Add(organizerTb);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganizerTb", new { id = organizerTb.OrganizerId }, organizerTb);
        }

        // DELETE: api/OrganizerTbs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizerTb(int id)
        {
            var organizerTb = await _context.OrganizerTbs.FindAsync(id);
            if (organizerTb == null)
            {
                return NotFound();
            }

            _context.OrganizerTbs.Remove(organizerTb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizerTbExists(int id)
        {
            return _context.OrganizerTbs.Any(e => e.OrganizerId == id);
        }
    }
}
