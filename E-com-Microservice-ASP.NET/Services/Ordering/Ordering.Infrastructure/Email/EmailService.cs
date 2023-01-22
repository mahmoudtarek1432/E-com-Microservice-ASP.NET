using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Model;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.E_mail
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailSettings> _emailService;
        public EmailService(IOptions<EmailSettings> EmailService, ILogger<EmailService> Logger) 
        { 
            _emailService = EmailService?? throw new ArgumentNullException(nameof(EmailService));
            Logger = Logger ?? throw new ArgumentNullException();

        }

        public Task<bool> SendEmailAsync(Email email)
        {
            var client = new SendGridClient
        }
    }
}
