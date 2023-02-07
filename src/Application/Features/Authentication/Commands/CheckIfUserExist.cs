using MediatR;
using ObakiSite.Application.Features.Authentication.Constants;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.Extensions;
using System.Text.Json;

namespace ObakiSite.Application.Features.Authentication.Commands
{
    public record CheckIfUserExist(string Email) : IRequest<Result<bool>>;

    public class CheckIfUserExistHandler : IRequestHandler<CheckIfUserExist, Result<bool>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CheckIfUserExistHandler(IHttpClientFactory httpClientFactory)
        {
             _httpClientFactory = httpClientFactory;
        }
        public async Task<Result<bool>> Handle(CheckIfUserExist request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var serializedPost = JsonSerializer.Serialize(request.Email).ToJsonStringContent();
            var response = await httpClient.PostAsync(AuthenticationConstants.CheckIfUserExist.EndPoint, serializedPost).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadJson<bool>();
                return result;

            }

            return Result.Fail<bool>(Error.HttpError(response.StatusCode.ToString()));
        }
    }
}


