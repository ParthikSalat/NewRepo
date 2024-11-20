using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace EventAPI.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendPaymentConfirmationEmail(string userEmail, string paymentDetails)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var message = new MimeMessage();

            // Here, passing an empty string as the display name (or you can use a custom name)
            message.From.Add(new MailboxAddress("", emailSettings["FromEmail"]));
            message.To.Add(new MailboxAddress("", userEmail));  // Use the user's email address here
            message.Subject = "Payment Confirmation";

            var bodyBuilder = new BodyBuilder
            {
                TextBody = $"Dear User,\n\nYour payment has been successfully processed. Here are the details:\n\n{paymentDetails}\n\nThank you for your payment."
            };

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), false);
                await client.AuthenticateAsync(emailSettings["Username"], emailSettings["Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
