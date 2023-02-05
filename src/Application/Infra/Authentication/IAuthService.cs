using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.DTO;
using System.Security.Claims;

namespace ObakiSite.Application.Infra.Authentication
{
   public interface IAuthService
    {
        Task<string> GenerateToken(ApplicationUserDTO user);

        ClaimsPrincipal ValidateTokenAndGetClaimsPrincipal(string token);
    }
}
