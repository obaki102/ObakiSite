using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Application.Infra.Data;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ObakiSite.Application.Infra.Authentication
{
    public class AuthService : IAuthService
    {

        private readonly IDbContextFactory<ApplicationUserContext> _factory;
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private readonly IConfiguration _configuration;

        public AuthService(IDbContextFactory<ApplicationUserContext> factory, IConfiguration configuration)
        {
            _factory = factory;
            _configuration = configuration;
        }

        public Task<ApplicationResponse<ApplicationUserDTO>> GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationResponse> TryCreateAndValidateUser(ApplicationUserDTO user)
        {
            await _semaphore.WaitAsync();
            try
            {

                if (user is null)
                    throw new ArgumentNullException(nameof(ApplicationUserDTO));

                using var context = _factory.CreateDbContext();
                var checkIfUserAlreadyExist = await context.ApplicationUsers.FindAsync(user.Id).ConfigureAwait(false);

                if (checkIfUserAlreadyExist is null)
                {
                    ApplicationUser newUser = user;
                    context.ApplicationUsers.Add(newUser);
                    var result = await context.SaveChangesAsync().ConfigureAwait(false);

                    if (result == 0)
                        return ApplicationResponse.Fail($"User with id {user.Id} - creation failed.");
                }

                //validate

                return ApplicationResponse.Success();
            }
            finally
            {
                _semaphore.Release();
            }

        }

        //Todo: Check if can be translated to extension, predicate for bool validation and iterate through property reflection  
        private ClaimsIdentity GenerateClaimsIdentityFromUser(ApplicationUser user)
        {
            var claimsIdentity = new ClaimsIdentity(DefaultConstants.Bearer);
            claimsIdentity.AddClaim(new(ClaimTypes.NameIdentifier, user.Id.ToString()));

            if (!string.IsNullOrEmpty(user.DisplayName))
            {
                claimsIdentity.AddClaims(new[] {
                new Claim(ClaimTypes.Name, user.DisplayName)
            });
            }
            if (!string.IsNullOrEmpty(user.Email))
            {
                claimsIdentity.AddClaims(new[] {
                new Claim(ClaimTypes.Email, user.Email)
            });
            }
            if (!string.IsNullOrEmpty(user.Role))
            {
                claimsIdentity.AddClaims(new[] {
                new Claim(ClaimTypes.Role, user.Role)
            });
            }

            return claimsIdentity;
        }

        private string CreateToken(ClaimsIdentity claimsIdentity)
        {
            try
            {
                if (string.IsNullOrEmpty(_configuration.GetSection(DefaultConstants.TokenKey).Value))
                    throw new ArgumentNullException("Cannot find token key.");

                var key = new SymmetricSecurityKey(Encoding.UTF8
                       .GetBytes(_configuration.GetSection(DefaultConstants.TokenKey).Value));
                var signingCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var token = new JwtSecurityToken(
                        claims: claimsIdentity.Claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: signingCredential);
                var generatedJwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                return generatedJwtToken;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public ApplicationResponse<ClaimsPrincipal> ValidateTokenAndGetClaimsPrincipal(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(_configuration.GetSection(DefaultConstants.TokenKey).Value))
                    throw new ArgumentNullException("Cannot find token key.");

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection(DefaultConstants.TokenKey).Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    return ApplicationResponse<ClaimsPrincipal>.Fail("Invalid token.");
                }

                return ApplicationResponse<ClaimsPrincipal>.Success(principal);
            }
            catch (Exception)
            {
                return ApplicationResponse<ClaimsPrincipal>.Fail("Invalid token.");
            }
        }




        //Verify Credential based on Identity Provider

        //Google

        //Microsoft

    }
}
