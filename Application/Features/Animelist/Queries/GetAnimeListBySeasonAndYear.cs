using MediatR;
using ObakiSite.Application.Features.LocalStorageCache.Services;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.DTO;
using ObakiSite.Shared.Models.Response;
using Polly;
using Polly.Retry;
using System.Net.Http.Json;

namespace ObakiSite.Application.Features.Animelist.Queries
{
    public record GetAnimeListBySeasonAndYear(Season Season) : IRequest<ApplicationResponse<AnimeListRoot>>;

    public class GetAnimeListBySeasonAndYearHandler : IRequestHandler<GetAnimeListBySeasonAndYear, ApplicationResponse<AnimeListRoot>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageCache<AnimeListRoot> _localStorageCache;
        private readonly AsyncRetryPolicy<ApplicationResponse<AnimeListRoot>> _retryPolicy;
        public GetAnimeListBySeasonAndYearHandler(IHttpClientFactory httpClientFactory, ILocalStorageCache<AnimeListRoot> localStorageCache)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageCache = localStorageCache;
            _retryPolicy = Policy<ApplicationResponse<AnimeListRoot>>.Handle<HttpRequestException>()
                            .WaitAndRetryAsync(3, times => TimeSpan.FromMilliseconds(times * 100));

            _localStorageCache.Options = new LocalStorageCacheOptions
            {
                CreationDateKey = AnimeList.CacheDataCreateDateKey,
                DataKey = AnimeList.CacheDataKey,
                NumberOfHrsToRefreshCache = 6
            };
        }
        public async Task<ApplicationResponse<AnimeListRoot>> Handle(GetAnimeListBySeasonAndYear request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClient.Default);
            var uriRequest = $"/api/animelists/{request.Season.SeasonOfTheYear}/{request.Season.Year}";
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                if (_localStorageCache.IsCacheEmpty())
                {
                    _localStorageCache.Data = await httpClient.GetFromJsonAsync<AnimeListRoot>(uriRequest);
                }
                return await _localStorageCache.GetCacheData();
            });

        }
    }
}
