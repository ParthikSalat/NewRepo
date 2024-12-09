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
    public class UserTbsController : ControllerBase
    {
        private readonly EventDbContext _context;

        public UserTbsController(EventDbContext context)
        {
            _context = context;
        }
            //login
            [HttpPost("login")]
            public async Task<IActionResult> login(string email, string password)
            {
                var a = await _context.UserTbs
                    .FirstOrDefaultAsync(o => o.UserEmail == email && o.UserPassword == password);

                if (a == null)
                {
                    return NotFound();
                }



                return Ok(a);
            }

        // GET: api/UserTbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTb>>> GetUserTbs()
        {
            return await _context.UserTbs.ToListAsync();
        }

        // GET: api/UserTbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTb>> GetUserTb(int id)
        {
            var userTb = await _context.UserTbs.FindAsync(id);

            if (userTb == null)
            {
                return NotFound();
            }

            return userTb;
        }

        // PUT: api/UserTbs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTb(int id, UserTb userTb)
        {
            if (id != userTb.UserId)
            {
                Console.WriteLine($"ID mismatch: {id} != {userTb.UserId}");
                return BadRequest();
            }

            Console.WriteLine($"Received User Data: {userTb.UserName}, {userTb.UserEmail}, {userTb.UserPassword}");

            _context.Entry(userTb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine("Update successful.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTbExists(id))
                {
                    Console.WriteLine("User not found.");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // POST: api/UserTbs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserTb>> PostUserTb(UserTb userTb)
        {
            // Optionally set UserId to 0 to be explicit, or just do not set it at all
            userTb.UserId = 0; // Optional, as it will be ignored during insertion

            _context.UserTbs.Add(userTb);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTb", new { id = userTb.UserId }, userTb);
        }


        // DELETE: api/UserTbs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTb(int id)
        {
            var userTb = await _context.UserTbs.FindAsync(id);
            if (userTb == null)
            {
                return NotFound();
            }

            _context.UserTbs.Remove(userTb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserTbExists(int id)
        {
            return _context.UserTbs.Any(e => e.UserId == id);
        }
    }
}
