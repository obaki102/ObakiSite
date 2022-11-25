using ObakiSite.Shared.Constants;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.IO;
using System.Net;

namespace ObakiSite.Api.Functions
{
    public static class SignalR
    {
        [Function("negotiate")]
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
        public static SignalRMessageAction SendMessage(
         [HttpTrigger(AuthorizationLevel.Function, "post")] 
         HttpRequestData req)
        {
            using var bodyReader = new StreamReader(req.Body);
            return new SignalRMessageAction(HubHandler.ReceivedMessage)
            {
                Arguments = new[] { bodyReader.ReadToEnd() },
            };
        }
    }
}
