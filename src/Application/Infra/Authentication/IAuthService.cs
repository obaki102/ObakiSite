using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.DTO;
using System.Security.Claims;
using Result = ObakiSite.Application.Shared.Result;

namespace ObakiSite.Application.Infra.Authentication
{
   public interface IAuthService
    {
        Task<Result> GenerateToken(ApplicationUserDTO user);

        Result<ClaimsPrincipal> ValidateTokenAndGetClaimsPrincipal(string token);
    }
}
