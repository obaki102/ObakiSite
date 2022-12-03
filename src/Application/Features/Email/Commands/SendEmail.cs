using MediatR;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.Models;
using ObakiSite.Shared.Models.Response;
using System.Text;
using System.Text.Json;

namespace ObakiSite.Application.Features.Email.Commands
{
    public record SendEmail(EmailMessage EmailMessage) : IRequest<ApplicationResponse>;

    public class SendEmailHandler : IRequestHandler<SendEmail, ApplicationResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SendEmailHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApplicationResponse> Handle(SendEmail request, CancellationToken cancellationToken)
        {
            //todo: Implement Web workers once .net 8 comes out.
            var httpClient = _httpClientFactory.CreateClient(HttpNameClient.Default);
            var serializedEmailMessage = JsonSerializer.Serialize(request.EmailMessage);
            var uriRequest = "/api/sendEmail";
            var response = await httpClient.PostAsync(uriRequest, new StringContent(serializedEmailMessage, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<ApplicationResponse>(content);

                if (result is null || !result.IsSuccess)
                {
                    return ApplicationResponse.Fail();
                }

                return result;
            }

            return ApplicationResponse.Fail(response.StatusCode.ToString());
        }
    }

}
