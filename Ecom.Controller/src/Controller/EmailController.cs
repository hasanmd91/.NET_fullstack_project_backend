using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Microsoft.AspNetCore.Mvc;


namespace SimpleEmailApp.Controllers
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

        [HttpPost]
        public IActionResult SendEmail(EmailDto request)
        {
            _emailService.SendEmailAsync(request);
            return Ok();
        }
    }
}