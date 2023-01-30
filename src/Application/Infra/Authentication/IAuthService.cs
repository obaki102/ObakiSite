using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Infra.Authentication
{
   public interface IAuthService
    {
        Task<ApplicationResponse<string>> TryCreateUserAndToken(ApplicationUserDTO user);

        Task<ApplicationResponse<ApplicationUserDTO>> GetUserById (Guid id);
    }
}
