using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Infra.Authentication
{
   public interface IAuthService
    {
        Task<ApplicationResponse> TryCreateAndValidateUser(ApplicationUserDTO user);

        Task<ApplicationResponse<ApplicationUserDTO>> GetUserById (Guid id);
    }
}
