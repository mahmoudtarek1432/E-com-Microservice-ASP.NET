using Ordering.Application.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Infrastructure
{
    internal interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
