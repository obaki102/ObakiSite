using AutoMapper;
using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Extensions;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Posts.Queries
{
    public record GetPostSummaries : IRequest<ApplicationResponse<IReadOnlyList<PostSummaryDTO>>>;

    public class GetPostSummariesHandler : IRequestHandler<GetPostSummaries, ApplicationResponse<IReadOnlyList<PostSummaryDTO>>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageCache _localStorageCache;
        public GetPostSummariesHandler(IHttpClientFactory httpClientFactory, ILocalStorageCache localStorageCache)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageCache = localStorageCache;
        }
      
        public async Task<ApplicationResponse<IReadOnlyList<PostSummaryDTO>>> Handle(GetPostSummaries request, CancellationToken cancellationToken)
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
                        //To do implement caching
                        var result = await response.ReadJson<ApplicationResponse<IReadOnlyList<PostSummaryDTO>>>();
                        if (result is not null)
                        {
                            return result;
                        }

                        return ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Fail("No data retrieved.");
                    }

                    return ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Fail(response.StatusCode.ToString());
                });

            return cache ?? ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Fail();


        }
    }
}
