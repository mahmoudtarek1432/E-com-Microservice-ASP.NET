using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Model;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.E_mail
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> EmailService, ILogger<EmailService> Logger) 
        { 
            _emailSettings = EmailService.Value?? throw new ArgumentNullException(nameof(EmailService));
            _logger = Logger ?? throw new ArgumentNullException();

        }

        public async Task<bool> SendEmailAsync(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var subject = email.Subject?? string.Empty;
            var to = new EmailAddress(email.To);
            var body = email.Body;

            var from = new EmailAddress(_emailSettings.FromAddress, _emailSettings.FromName);

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, body, body);

            var response = await client.SendEmailAsync(sendGridMessage);

            _logger.LogInformation("Email sent.");

            if(response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            _logger.LogError("Email sending failed.");
            return false;
        }
    }
}
