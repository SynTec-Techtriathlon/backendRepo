using Back.Services;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("Approve")]
        public IActionResult SendApproveEmail(string to, string fullName)
        {
            _emailService.ApproveUserMail(to, fullName);
            return Ok();
        }

        [HttpPost("Reject")]
        public IActionResult SendRejectEmail(string to, string fullName)
        {
            _emailService.RejectUserMail(to, fullName);
            return Ok();
        }
    }
}
