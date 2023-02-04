using Microsoft.AspNetCore.Mvc;
using ObakiSite.Application.Infra.Authentication;
using ObakiSite.Application.Shared.DTO;

namespace ObakiSite.WebApi.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("api/get-token")]
        public async Task<IActionResult> SendEmail(ApplicationUserDTO user)
        {
            var result = await _authService.TryCreateUserAndToken(user);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Messages);
        }
    }
}
