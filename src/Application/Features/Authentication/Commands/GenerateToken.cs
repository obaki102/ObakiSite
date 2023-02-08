using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Features.Authentication.Constants;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.Extensions;
using System.Text.Json;

namespace ObakiSite.Application.Features.Authentication.Commands
{
    public record GenerateToken(ApplicationUserDTO User) : IRequest<Result>;

    public class GenerateTokenHandler : IRequestHandler<GenerateToken, Result>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageCache _localStorageCache;
        public GenerateTokenHandler(IHttpClientFactory httpClientFactory, ILocalStorageCache localStorageCache)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageCache = localStorageCache;
        }
        public async Task<Result> Handle(GenerateToken request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var serializedPost = JsonSerializer.Serialize(request.User).ToJsonStringContent();
            var response = await httpClient.PostAsync(AuthenticationConstants.GetToken.EndPoint, serializedPost).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadJson<string>();

                if (string.IsNullOrEmpty(result))
                {
                    return Result.Fail<string>("Invalid Token");
                }

                await _localStorageCache.SetCacheAsync(AuthenticationConstants.GetToken.CacheDataKey, result);
                return Result.Success();
            }

            return Result.Fail<string>(Error.HttpError(response.StatusCode.ToString()));
        }
    }
}
