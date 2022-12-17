using MediatR;
using ObakiSite.Application.Extensions;
using ObakiSite.Application.Features.LocalStorageCache.Services;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.DTO;
using ObakiSite.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Animelist.Queries
{
    public record GetAnimeListBySeasonAndYear(Season Season) : IRequest<ApplicationResponse<AnimeListRoot>>;

    public class GetAnimeListBySeasonAndYearHandler : IRequestHandler<GetAnimeListBySeasonAndYear, ApplicationResponse<AnimeListRoot>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageCache<AnimeListRoot> _localStorageCache;
        public GetAnimeListBySeasonAndYearHandler(IHttpClientFactory httpClientFactory, ILocalStorageCache<AnimeListRoot> localStorageCache)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageCache = localStorageCache;
            _localStorageCache.Options = new LocalStorageCacheOptions
            {
                CreationDateKey = AnimeList.CacheDataCreateDateKey,
                DataKey = AnimeList.CacheDataKey,
                NumberOfHrsToRefreshCache = 6
            };
        }

        public async Task<ApplicationResponse<AnimeListRoot>> Handle(GetAnimeListBySeasonAndYear request, CancellationToken cancellationToken)
        {
            var isRefreshNeeded = await _localStorageCache.IsCacheNeedsDataRefresh().ConfigureAwait(false);

            if (isRefreshNeeded)
            {
                var httpClient = _httpClientFactory.CreateClient(HttpNameClient.Default);
                var uriRequest = $"{AnimeList.Endpoint}{request.Season.SeasonName}/{request.Season.Year}";
                var response = await httpClient.GetAsync(uriRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.ConvertStreamToTAsync<AnimeListRoot>();

                    if (result is null)
                    {
                        return ApplicationResponse<AnimeListRoot>.Fail("No data.");
                    }

                    try
                    {
                        await _localStorageCache.SetData(result);
                    }catch(Exception ex)
                    {
                        return ApplicationResponse<AnimeListRoot>.Fail(ex.Message);
                    }
                }
            }

            return await _localStorageCache.GetCacheData();
        }
    }
}
