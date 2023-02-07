using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Application.Infra.Data;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ObakiSite.Application.Features.Authentication.Services
{
    public class AuthService : IAuthService
    {

        private readonly IDbContextFactory<ApplicationUserContext> _factory;
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private readonly AuthServiceOptions _authServiceOptions;

        public AuthService(IDbContextFactory<ApplicationUserContext> factory, IOptions<AuthServiceOptions> authServiceOptions)
        {
            _factory = factory;
            _authServiceOptions = authServiceOptions.Value;
        }
        public async Task<bool> IsUserExist(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("Value of email specified is null.");

            using var context = _factory.CreateDbContext();
            var isExistingUser = await context.ApplicationUsers.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Email == email).ConfigureAwait(false);

            return isExistingUser is null ? false : true;
        }

        public async Task<string> GenerateTokenForNewUser(ApplicationUserDTO user)
        {
            await _semaphore.WaitAsync();
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(ApplicationUserDTO));

                using var context = _factory.CreateDbContext();
                ApplicationUser newUser = user;
                context.ApplicationUsers.Add(newUser);
                var result = await context.SaveChangesAsync().ConfigureAwait(false);

                if (result == 0)
                    return string.Empty;
                // todo: log return $"User with email {user.Id} - creation failed.";

                var claimsIdentity = GenerateClaimsIdentityFromUser(newUser);
                var token = CreateToken(claimsIdentity);

                return token;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<string> GenerateTokenForExistingUser(ApplicationUserDTO user)
        {
            await _semaphore.WaitAsync();
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(ApplicationUserDTO));

                ApplicationUser existingUser = user;
                var claimsIdentity = GenerateClaimsIdentityFromUser(existingUser);
                var token = CreateToken(claimsIdentity);

                return token;
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
            if (!string.IsNullOrEmpty(user.UserRole.ToString()))
            {
                claimsIdentity.AddClaims(new[] {
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            });
            }

            return claimsIdentity;
        }

        private string CreateToken(ClaimsIdentity claimsIdentity)
        {
            try
            {
                if (_authServiceOptions is null)
                    throw new ArgumentNullException(nameof(_authServiceOptions));

                var key = new SymmetricSecurityKey(Encoding.UTF8
                       .GetBytes(_authServiceOptions.TokenKey));
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

        public ClaimsPrincipal ValidateTokenAndGetClaimsPrincipal(string token)
        {
            try
            {
                if (_authServiceOptions is null)
                    throw new ArgumentNullException(nameof(_authServiceOptions));

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authServiceOptions.TokenKey)),
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
                    throw new InvalidOperationException("Invalid Token");
                }

                return principal;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }


    }
}
