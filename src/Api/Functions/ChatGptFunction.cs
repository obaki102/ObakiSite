using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Api;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using ObakiSite.Application.Features.ChatGPT.Services;
using ObakiSite.Application.Shared;

namespace ObakiSite.AzureFunction.Functions
{
    public class ChatGptFunction
    {
        private readonly ILogger _logger;
        private readonly IChatGPTService _chatGptService;
        public ChatGptFunction(ILoggerFactory loggerFactory, IChatGPTService chatGPTService)
        {
            _logger = loggerFactory.CreateLogger<ChatGptFunction>();
            _chatGptService = chatGPTService;
        }

        [Function("ChatGptFunction")]
        [OpenApiOperation(operationId: "Chat", tags: new[] { "ChatGpt" }, Summary = "Ask chatGpt.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/text", bodyType: typeof(string), Description = "Message", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "Successful operation")]
        public async Task<HttpResponseData> AskChatGpt([HttpTrigger(AuthorizationLevel.Function, "post", Route = "ask-chatgpt")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");


            var response = req.CreateResponse(HttpStatusCode.OK);
            var request = req.Body;

            if (request.Length == 0)
            {
                await response.WriteAsJsonAsync(Result.Fail("No data posted."));
                return response;
            }

            try
            {
                var message =  await new StreamReader(req.Body).ReadToEndAsync();
                var result = _chatGptService.AskChatGpt(message);

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
