using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ObakiSite.Shared.Constants;

namespace ObakiSite.Api.Functions
{
    public class SecretKeys
    {
        private readonly ILogger<SecretKeys> _logger;
        public SecretKeys(ILogger<SecretKeys> logger)
        {
            _logger = logger;
        }

        [Function("GetSpeechServiceSubscriptionKey")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.System, "get", Route = "getSpeechSubKey")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");
            //THIS IS NOT SECURE!!!
            //Need to check azure key valut or any similar service.
            await response.WriteStringAsync(Environment.GetEnvironmentVariable(AppSecretKeys.SpeechSubKey));
            return response;

        }
    }
}
