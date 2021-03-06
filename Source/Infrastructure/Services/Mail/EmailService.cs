using Application.Contracts.Infrastructure;
using Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> mailSettings)
        {
            _emailSettings = mailSettings.Value;
        }

        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);
            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;
            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };
            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var respose = await client.SendEmailAsync(sendGridMessage);

            if (respose.StatusCode == System.Net.HttpStatusCode.Accepted ||
                respose.StatusCode == System.Net.HttpStatusCode.OK) return true;
            return false;
        }
    }
}