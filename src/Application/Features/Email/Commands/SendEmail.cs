using MediatR;
using ObakiSite.Application.Extensions;
using ObakiSite.Application.Features.Email.Constants;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;
using System.Text.Json;

namespace ObakiSite.Application.Features.Email.Commands
{
    public record SendEmail(EmailMessageDTO EmailMessage) : IRequest<ApplicationResponse>;

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
            var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var serializedEmailMessage = JsonSerializer.Serialize(request.EmailMessage).ToJsonStringContent();
            var response = await httpClient.PostAsync(EmailConstants.Endpoint, serializedEmailMessage).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadJson<ApplicationResponse>();

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
