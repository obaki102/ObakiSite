using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.Extensions;

namespace ObakiSite.Application.Features.Posts.Queries
{
    public record GetPostSummaries : IRequest<Result<IReadOnlyList<PostSummaryDTO>>>;

    public class GetPostSummariesHandler : IRequestHandler<GetPostSummaries, Result<IReadOnlyList<PostSummaryDTO>>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageCache _localStorageCache;
        public GetPostSummariesHandler(IHttpClientFactory httpClientFactory, ILocalStorageCache localStorageCache)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageCache = localStorageCache;
        }
      
        public async Task<Result<IReadOnlyList<PostSummaryDTO>>> Handle(GetPostSummaries request, CancellationToken cancellationToken)
        {
            var cache = await _localStorageCache.GetOrCreateCacheAsync(
                PostConstants.GetPostSummaries.CacheDataKey,
                TimeSpan.FromMinutes(5),
                async () =>
                {
                    var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
                    var response = await httpClient.GetAsync(PostConstants.GetPostSummaries.EndPoint).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.ReadJson<IReadOnlyList<PostSummaryDTO>>();
                        if (result is not null)
                        {
                            return Result.Success(result);
                        }

                        return Result.Fail<IReadOnlyList<PostSummaryDTO>>(new Error("GetPostSummariesHandlerError","No data retrieved."));
                    }

                    return Result.Fail<IReadOnlyList<PostSummaryDTO>>(Error.HttpError(response.StatusCode.ToString()));
                });

            return cache ?? Result.Fail<IReadOnlyList<PostSummaryDTO>>(new Error("GetPostSummariesHandlerError", "No data retrieved."));


        }
    }
}
