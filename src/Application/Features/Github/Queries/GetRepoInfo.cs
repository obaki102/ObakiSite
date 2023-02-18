using MediatR;
using ObakiSite.Application.Features.Github.Constants;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.Extensions;

namespace ObakiSite.Application.Features.Github.Queries
{

    public record GetRepoInfo(string user, string repository) : IRequest<Result<GithubRepoInfo>>;

    public class GetRepoInfoHandler : IRequestHandler<GetRepoInfo, Result<GithubRepoInfo>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GetRepoInfoHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<GithubRepoInfo>> Handle(GetRepoInfo request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var uriRequest = $"{GithubConstants.Endpoint}{request.user}/{request.repository}";
            var response = await httpClient.GetAsync(uriRequest).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadJson<GithubRepoInfo>();

                if (result is null)
                {
                    return Result.Fail<GithubRepoInfo>(Error.EmptyValue);
                }

                return result;
            }
            return Result.Fail<GithubRepoInfo>(Error.EmptyValue);
        }

    }
}
