using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ichi.Services
{
    internal class EmailService : IEmailService
    {

        private readonly IConfiguration _configuration;


        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public void SendEmail(string toEmail, string subject, string message)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:Server"];
                var port = int.Parse(_configuration["EmailSettings:Port"] ?? "587");
                var senderEmail = _configuration["EmailSettings:SenderEmail"];
                var senderName = _configuration["EmailSettings:SenderName"] ?? "Console App";
                var password = _configuration["EmailSettings:Password"];

                using var client = new SmtpClient(smtpServer, port)
                {
                    Credentials = new NetworkCredential(senderEmail, password),
                    EnableSsl = true
                };

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail!, senderName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                client.Send(mailMessage);
                Console.WriteLine($"Mail sent successfully to: {toEmail}", toEmail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}: Error sending email to: {toEmail}", toEmail);
                throw;
            }
        }



    }
}
