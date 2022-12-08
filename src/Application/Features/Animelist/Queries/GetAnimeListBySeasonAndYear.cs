using MediatR;
using ObakiSite.Application.Features.LocalStorageCache.Services;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.DTO;
using ObakiSite.Shared.DTO.Response;
using System.Text.Json;

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
            var httpClient = _httpClientFactory.CreateClient(HttpNameClient.Default);
            var uriRequest = $"/api/animelists/{request.Season.SeasonName}/{request.Season.Year}";
            var isRefreshNeeded = await _localStorageCache.IsDataNeedsRefresh().ConfigureAwait(false);

            if (isRefreshNeeded)
            {
                 var response = await httpClient.GetAsync(uriRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    var result  = await JsonSerializer.DeserializeAsync<AnimeListRoot>(content).ConfigureAwait(false);

                    if (result is null)
                    {
                        return ApplicationResponse<AnimeListRoot>.Fail("No data.");
                    }
                    _localStorageCache.Data = result;

                }
            }

            return await _localStorageCache.GetCacheData();
        }
    }
}
