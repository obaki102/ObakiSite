using Microsoft.AspNetCore.Mvc;
using ObakiSite.Application.Features.Authentication.Constants;
using ObakiSite.Application.Features.Authentication.Services;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.Settings;


namespace ObakiSite.WebApi.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        public AuthenticationController(IAuthService authService, IConfiguration config)
        {
            _authService = authService;
            _config = config;
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

        [HttpGet("api/auth/get-google-config")]
        public  IActionResult GetGoogleAuthConfig()
        {
            var config = _config.GetSection(AuthenticationConstants.GetGoogleAuthConfig.GoogleAuth2Config).Get<GoogleAuth2Config>();
            if (config == null)
                return BadRequest(config);

            return Ok(config);
        }
    }
}
