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
    public class PaymentTbsController : ControllerBase
    {
        private readonly EventDbContext _context;

        public PaymentTbsController(EventDbContext context)
        {
            _context = context;
        }

        // GET: api/PaymentTbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentTb>>> GetPaymentTbs()
        {
            return await _context.PaymentTbs.ToListAsync();
        }

        // GET: api/PaymentTbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentTb>> GetPaymentTb(int id)
        {
            var paymentTb = await _context.PaymentTbs.FindAsync(id);

            if (paymentTb == null)
            {
                return NotFound();
            }

            return paymentTb;
        }

        // PUT: api/PaymentTbs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentTb(int id, PaymentTb paymentTb)
        {
            if (id != paymentTb.PaymentId)
            {
                return BadRequest();
            }

            _context.Entry(paymentTb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTbExists(id))
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

        // POST: api/PaymentTbs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentTb>> PostPaymentTb(PaymentTb paymentTb)
        {
            _context.PaymentTbs.Add(paymentTb);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentTb", new { id = paymentTb.PaymentId }, paymentTb);
        }

        // DELETE: api/PaymentTbs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentTb(int id)
        {
            var paymentTb = await _context.PaymentTbs.FindAsync(id);
            if (paymentTb == null)
            {
                return NotFound();
            }

            _context.PaymentTbs.Remove(paymentTb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentTbExists(int id)
        {
            return _context.PaymentTbs.Any(e => e.PaymentId == id);
        }
    }
}
