using Microsoft.AspNetCore.Mvc;
using ObakiSite.Application.Features.Authentication.Services;
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


        [HttpPost("api/auth/is-user-exist")]
        public async Task<IActionResult> IsUserExist(string email)
        {
            return Ok(await _authService.IsUserExist(email));

        }

        [HttpPost("api/auth/get-token")]
        public async Task<IActionResult> GenerateTokenForNewUser(ApplicationUserDTO user)
        {
            if(user.IsNewUser)
                return Ok(await _authService.GenerateTokenForNewUser(user));

            return Ok(await _authService.GenerateTokenForExistingUser(user));
        }
    }
}
