using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using ObakiSite.Application.Features.Authentication.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ObakiSite.Application.Shared.DTO;
using System;
using ObakiSite.Application.Shared;
using System.Text.Json;
using ObakiSite.Api.Functions;

namespace ObakiSite.AzureFunction.Functions
{
    public class AuthenticationFunction
    {
        private readonly ILogger _logger;
        private readonly IAuthService _authService;

        public AuthenticationFunction(ILogger<AuthenticationFunction> logger, IAuthService authService) 
        {
            _logger = logger;
            _authService = authService;
        }

        [Function("IsUserExist")]
        public async Task<HttpResponseData> IsUserExist(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "auth/is-user-exist/{email?}")] HttpRequestData req,
            string email)
        {
            _logger.LogInformation("AnimelistFunction processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var result = await _authService.IsUserExist(email);

            await response.WriteAsJsonAsync(result);
            return response;
        }

        [Function("GetToken")]
        public async Task<HttpResponseData> GetToken([HttpTrigger(AuthorizationLevel.Function, "post", Route = "auth/get-token")] HttpRequestData req)
        {
            _logger.LogInformation("PostFunction trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var request = req.Body;

            if (request.Length == 0)
            {
                await response.WriteAsJsonAsync(Result.Fail("No data posted."));
                return response;
            }

            try
            {
                var user = await JsonSerializer.DeserializeAsync<ApplicationUserDTO>(request);
                var result = user.IsNewUser ? await _authService.GenerateTokenForNewUser(user) 
                        : await _authService.GenerateTokenForExistingUser(user);

                await response.WriteAsJsonAsync(result);
                return response;
            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(Result.Fail(ex.Message));
                return response;
            }
        }
    }
}
