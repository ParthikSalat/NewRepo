using EventAPI.Models;
using EventAPI.Services;
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
        private readonly EmailService _emailService;

        // Injecting both EventDbContext and EmailService into the controller
        public PaymentController(EventDbContext dbContext, EmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
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

            // Assuming payment contains user email and relevant payment details
            string userEmail = "parvinaptl@gmail.com"; // Replace with actual field
            string paymentDetails = "hiiii"; // Example details

            // Send email to the user
            await _emailService.SendPaymentConfirmationEmail(userEmail, paymentDetails);

            return CreatedAtAction("GetPayment", new { Id = payment.PaymentId }, payment);
        }
    }
}
