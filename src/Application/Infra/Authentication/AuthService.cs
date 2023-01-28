using Microsoft.EntityFrameworkCore;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Application.Infra.Data;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Infra.Authentication
{
    public class AuthService : IAuthService
    {

        private readonly IDbContextFactory<ApplicationUserContext> _factory;
        public AuthService(IDbContextFactory<ApplicationUserContext> factory)
        {
            _factory = factory;
        }
        public Task<ApplicationResponse<ApplicationUser>> GetUserById(Guid id)
        {
           throw new NotImplementedException();
        }

        public async Task<bool> TryCreateAndValidateUser(ApplicationUser user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(ApplicationUser));

            using var context = _factory.CreateDbContext();
            var checkIfUserAlreadyExist = await context.ApplicationUsers.FindAsync(user.Id).ConfigureAwait(false);

            if (checkIfUserAlreadyExist is not null)
            {
                //create user
            }
            //validate

            return true;
         
        }

      
    }
}
