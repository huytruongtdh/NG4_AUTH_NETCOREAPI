using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NG4_REG_LOG.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
