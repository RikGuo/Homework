using Homework2021.Content.Mail;
using Homework2021.DAO.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Homework2021.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        IMailService _emailService = null;
        public EmailController(IMailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public bool SendEmailAsync(RS_MailRequest emailData)
        {
            return _emailService.SendEmailAsync(emailData);
        }

    }
}