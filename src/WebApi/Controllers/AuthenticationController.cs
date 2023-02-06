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


        [HttpPost("api/is-user-exist")]
        public async Task<IActionResult> IsUserExist(string email)
        {
            return Ok(await _authService.IsUserExist(email));

        }

        [HttpPost("api/get-token-new")]
        public async Task<IActionResult> GenerateTokenForNewUser(ApplicationUserDTO user)
        {

            return Ok(await _authService.GenerateTokenForNewUser(user));
        }

        [HttpPost("api/get-token-exist")]
        public async Task<IActionResult> GenerateTokenForExistingUser(ApplicationUserDTO user)
        {
            return Ok(await _authService.GenerateTokenForExistingUser(user));
        }
    }
}
