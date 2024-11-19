using EventAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly EventDbContext _dbContext;


        public PaymentController(EventDbContext dbContext)  
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentTb>>> GetPayment()
        {
            return await _dbContext.PaymentTbs.ToArrayAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PaymentTb>> PostPayment(PaymentTb payment)
        {
            _dbContext.PaymentTbs.Add(payment);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction("GetPayment", new { Id = payment.PaymentId }, payment);

        }

    }
}
