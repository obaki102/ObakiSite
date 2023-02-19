using MediatR;
using ObakiSite.Application.Features.Github.Constants;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.Extensions;

namespace ObakiSite.Application.Features.Github.Queries
{
   
    public record GetLastCommit() : IRequest<Result<GithubLastCommit>>;

    public class GetLastCommitHandler : IRequestHandler<GetLastCommit, Result<GithubLastCommit>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GetLastCommitHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<GithubLastCommit>> Handle(GetLastCommit request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var uriRequest = GithubConstants.GetLastCommit.Endpoint;
            var response = await httpClient.GetAsync(uriRequest).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadJson<GithubLastCommit>();

                if (result is null)
                {
                    return Result.Fail<GithubLastCommit>(Error.EmptyValue);
                }

                return result;
            }
            return Result.Fail<GithubLastCommit>(Error.EmptyValue);
        }
    }
}
