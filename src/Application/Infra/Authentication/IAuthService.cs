using ObakiSite.Application.Shared.DTO;
using System.Security.Claims;

namespace ObakiSite.Application.Infra.Authentication
{
    public interface IAuthService
    {
        Task<string> GenerateTokenForNewUser(ApplicationUserDTO user);
        Task<string> GenerateTokenForExistingUser(ApplicationUserDTO user);
        ClaimsPrincipal ValidateTokenAndGetClaimsPrincipal(string token);
        Task<bool> IsUserExist(string email);
        
    }
}
