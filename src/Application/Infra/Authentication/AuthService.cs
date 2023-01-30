﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        private readonly AuthServiceOptions _authServiceOptions;

        public AuthService(IDbContextFactory<ApplicationUserContext> factory, IOptions<AuthServiceOptions> authServiceOptions)
        {
            _factory = factory;
            _authServiceOptions = authServiceOptions.Value;
        }

        public async Task<ApplicationResponse<ApplicationUserDTO>> GetUserById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            using var context = _factory.CreateDbContext();
            var isExistingUser = await context.ApplicationUsers.FindAsync(id).ConfigureAwait(false);

            var user = await context.ApplicationUsers.WithPartitionKey(id.ToString())
                      .AsNoTracking().SingleOrDefaultAsync(i => i.Id == id).ConfigureAwait(false);
            if (user is not null)
            {
                var userDTO = (ApplicationUserDTO)(user);
                return ApplicationResponse<ApplicationUserDTO>.Success(userDTO);
            }

            return ApplicationResponse<ApplicationUserDTO>.Fail($"User with id {id} - unable to retrieve.");
        }

        public async Task<ApplicationResponse<string>> TryCreateUserAndToken(ApplicationUserDTO user)
        {
            await _semaphore.WaitAsync();
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(ApplicationUserDTO));

                using var context = _factory.CreateDbContext();
                var isExistingUser = await context.ApplicationUsers.FindAsync(user.Id).ConfigureAwait(false);

                if (isExistingUser is null)
                {
                    ApplicationUser newUser = user;
                    context.ApplicationUsers.Add(newUser);
                    var result = await context.SaveChangesAsync().ConfigureAwait(false);

                    if (result == 0)
                        return ApplicationResponse<string>.Fail($"User with id {user.Id} - creation failed.");

                    isExistingUser = newUser;
                }

                var claimsIdentity = GenerateClaimsIdentityFromUser(isExistingUser);
                var token = CreateToken(claimsIdentity);

                return ApplicationResponse<string>.Success(token);
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

        public ApplicationResponse<ClaimsPrincipal> ValidateTokenAndGetClaimsPrincipal(string token)
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
