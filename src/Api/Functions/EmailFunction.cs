using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using ObakiSite.Application.Features.Email.Services;
using System.Text.Json;
using System;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.DTO;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;


namespace ObakiSite.Api.Functions
{
    public class EmailFunction
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailFunction> _logger;
        public EmailFunction(IEmailService emailService, ILogger<EmailFunction> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }
        [Function("SendEmail")]
        [OpenApiOperation(operationId: "SendEmail", tags: new[] { "Email" }, Summary = "Send email notification.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json; charset=utf-8", bodyType: typeof(EmailMessageDTO), Description = "EmailMessageDTO", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Summary = "Successful operation")]
        public async Task<HttpResponseData> SendEmail(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "send-email")] HttpRequestData req)
        {
            _logger.LogInformation("EmailFunction function processed a request.");
            var response = req.CreateResponse(HttpStatusCode.OK);
            var request = req.Body;

            if (request.Length == 0)
            {
                await response.WriteAsJsonAsync(Result.Fail("No data posted."));
                return response;
            }

            try
            {
                var deserializedEmailMessage = await JsonSerializer.DeserializeAsync<EmailMessageDTO>(request);
                var result = await _emailService.SendEmail(deserializedEmailMessage);

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
