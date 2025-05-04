using EmailSender.Models;
using EmailSender.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [Route("sendemail")]
        public async Task<ActionResult> SendEmailAsync(EmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _emailService.SendEmailAsync(request.To,request.Subject,request.Body, request.Attachments);

            return Ok("Email sent successfully.");
        }


    }
}
