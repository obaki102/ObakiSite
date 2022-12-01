using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using ObakiSite.Shared.Models;
using ObakiSite.Application.Features.Email.Services;

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
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "sendEmail/{emailMessage?")] HttpRequestData req,
            EmailMessage emailMessage,
            int year)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var result = _emailService.SendEmail(emailMessage).Result;
            var response = req.CreateResponse(HttpStatusCode.OK);
            if (result.IsSuccess)
            {
                await response.WriteAsJsonAsync(result);
                return response;
            }

            return req.CreateResponse(HttpStatusCode.BadRequest);

        }


    }
}
