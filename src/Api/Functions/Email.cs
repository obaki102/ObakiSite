using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using ObakiSite.Application.Features.Email.Services;
using System.Text.Json;
using System;
using ObakiSite.Shared.Models.Response;
using ObakiSite.Shared.Models;

namespace ObakiSite.Api.Functions
{
    public class Email
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<Email> _logger;
        public Email(IEmailService emailService, ILogger<Email> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }
        [Function("SendEmail")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "sendEmail")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
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
                var result = _emailService.SendEmail(deserializedEmailMessage).Result;

                if (result.IsSuccess)
                {
                    await response.WriteAsJsonAsync(result);
                    return response;
                }

                await response.WriteAsJsonAsync(ApplicationResponse.Fail("Email not sent."));
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
