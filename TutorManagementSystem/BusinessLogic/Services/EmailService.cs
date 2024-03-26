using BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(string toAddress, string subject, string message)
        {
            try
            {
                var fromAddress = _configuration["DefaultEmail:Email"];
                var appPassword = _configuration["DefaultEmail:AppPassword"];

                var smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress, appPassword)
                };

                var mailMessage = new MailMessage(fromAddress, toAddress, subject, message);
                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage).ConfigureAwait(false);

                return true;
            }
            catch (SmtpException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
