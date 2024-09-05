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
        public IActionResult SendApproveEmail(string to, string fname, string lname)
        {
            _emailService.ApproveUserMail(to, fname, lname);
            return Ok();
        }

        [HttpPost("Reject")]
        public IActionResult SendRejectEmail(string to, string fname, string lname)
        {
            _emailService.RejectUserMail(to, fname, lname);
            return Ok();
        }
    }
}
