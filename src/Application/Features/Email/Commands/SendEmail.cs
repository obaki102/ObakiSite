using MediatR;
using ObakiSite.Application.Features.Email.Constants;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.Extensions;
using System.Text.Json;

namespace ObakiSite.Application.Features.Email.Commands
{
    public record SendEmail(EmailMessageDTO EmailMessage) : IRequest<Result>;

    public class SendEmailHandler : IRequestHandler<SendEmail, Result>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SendEmailHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result> Handle(SendEmail request, CancellationToken cancellationToken)
        {
            //todo: Implement Web workers once .net 8 comes out.
            var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var serializedEmailMessage = JsonSerializer.Serialize(request.EmailMessage).ToJsonStringContent();
            var response = await httpClient.PostAsync(EmailConstants.Endpoint, serializedEmailMessage).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadJson<bool>();

                if (!result )
                {
                    return Result.Fail(Error.EmptyValue);
                }

                return Result.Success();
            }

            return Result.Fail(Error.HttpError(response.StatusCode.ToString()));
        }
    }
}
