using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.Content.Mail
{
    public interface IMailService
    {
        bool SendEmailAsync(MailRequest mailRequest);
    }
}
