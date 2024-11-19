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
    public class AdminTbsController : ControllerBase
    {
        private readonly EventDbContext _context;

        public AdminTbsController(EventDbContext context)
        {
            _context = context;
        }

        // GET: api/AdminTbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminTb>>> GetAdminTbs()
        {
            return await _context.AdminTbs.ToListAsync();
        }

        // GET: api/AdminTbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminTb>> GetAdminTb(int id)
        {
            var adminTb = await _context.AdminTbs.FindAsync(id);

            if (adminTb == null)
            {
                return NotFound();
            }

            return adminTb;
        }

        // PUT: api/AdminTbs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminTb(int id, AdminTb adminTb)
        {
            if (id != adminTb.AdminId)
            {
                return BadRequest();
            }

            _context.Entry(adminTb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminTbExists(id))
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

        // POST: api/AdminTbs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdminTb>> PostAdminTb(AdminTb adminTb)
        {
            _context.AdminTbs.Add(adminTb);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdminTbExists(adminTb.AdminId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdminTb", new { id = adminTb.AdminId }, adminTb);
        }

        // DELETE: api/AdminTbs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminTb(int id)
        {
            var adminTb = await _context.AdminTbs.FindAsync(id);
            if (adminTb == null)
            {
                return NotFound();
            }

            _context.AdminTbs.Remove(adminTb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminTbExists(int id)
        {
            return _context.AdminTbs.Any(e => e.AdminId == id);
        }
    }
}
