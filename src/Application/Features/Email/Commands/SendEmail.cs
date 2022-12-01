using MediatR;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.Models;
using ObakiSite.Shared.Models.Response;
using System.Net.Http.Json;


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
            var httpClient = _httpClientFactory.CreateClient(HttpNameClient.Default);
            var uriRequest = $"/api/sendEmail/{request.EmailMessage}";
            var result = await httpClient.GetFromJsonAsync<ApplicationResponse>(uriRequest);

            if (result is not null && result.IsSuccess)
            {
                return result;
            }

            return ApplicationResponse.Fail();
        }
    }

}
