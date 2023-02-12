using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.IO;
using System.Net;
using ObakiSite.Application.Features.Chat.Constants;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

namespace ObakiSite.Api.Functions
{
    public static class SignalRFunction
    {
        //todo:mugrate to web api.
        [Function("negotiate")]
        [OpenApiOperation(operationId: "GetSignalRInfo", tags: new[] { "SignalR" }, Summary = "Retrieved signalR information from the specified hub.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "Successful operation")]
        public static async Task<HttpResponseData> GetSignalRInfo(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            [SignalRConnectionInfoInput(HubName = "chat")] string connectionInfo)
        {
            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");
            await response.WriteStringAsync(connectionInfo);
            return response;
        }

        [Function("messages")]
        [SignalROutput(HubName = "chat", ConnectionStringSetting = "AzureSignalRConnectionString")]
        [OpenApiOperation(operationId: "SendMessage", tags: new[] { "SignalR" }, Summary = "Send message to specifed hub handler..", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "Successful operation")]
        public static SignalRMessageAction SendMessage(
         [HttpTrigger(AuthorizationLevel.Function, "post")] 
         HttpRequestData req)
        {
            using var bodyReader = new StreamReader(req.Body);
            return new SignalRMessageAction(HubHandlerConstants.ReceivedMessage)
            {
                Arguments = new[] { bodyReader.ReadToEnd() },
            };
        }
    }
}
