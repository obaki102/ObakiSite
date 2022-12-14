using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using ObakiSite.Application.Features.Email.Services;
using System.Text.Json;
using System;
using ObakiSite.Shared.DTO.Response;
using ObakiSite.Shared.DTO;

namespace ObakiSite.Api.Functions
{
    public class EmailFunctions
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailFunctions> _logger;
        public EmailFunctions(IEmailService emailService, ILogger<EmailFunctions> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }
        [Function("SendEmail")]
        public async Task<HttpResponseData> SendEmail(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "send-email")] HttpRequestData req)
        {
            _logger.LogInformation("EmailFunction function processed a request.");
            var response = req.CreateResponse(HttpStatusCode.OK);
            var request = req.Body;

            if (request.Length == 0)
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail("No data posted."));
                return response;
            }

            try
            {
                var deserializedEmailMessage = await JsonSerializer.DeserializeAsync<EmailMessage>(request);
                var result = await _emailService.SendEmail(deserializedEmailMessage);

                await response.WriteAsJsonAsync(result);
                return response;
            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail(ex.Message));
                return response;
            }

        }


    }
}
