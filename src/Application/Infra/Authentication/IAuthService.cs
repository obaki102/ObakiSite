using ObakiSite.Application.Domain.Entities;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Infra.Authentication
{
   public interface IAuthService
    {
        Task<bool> TryCreateAndValidateUser(ApplicationUser user);

        Task<ApplicationResponse<ApplicationUser>> GetUserById (Guid id);
    }
}
