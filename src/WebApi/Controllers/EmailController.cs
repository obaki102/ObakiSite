using Microsoft.AspNetCore.Mvc;
using ObakiSite.Application.Features.Email.Services;
using ObakiSite.Application.Shared.DTO;

namespace ObakiSite.WebApi.Controllers
{
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        [HttpPost("api/send-email")]
        public async Task<IActionResult> SendEmail(EmailMessageDTO emailMessage)
        {
            var result = await _emailService.SendEmail(emailMessage);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Messages);
        }
    }
}
