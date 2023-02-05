using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Errors;
using ObakiSite.Application.Features.Animelist.Constants;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.Extensions;

namespace ObakiSite.Application.Features.Animelist.Queries
{
    public record GetAnimeListBySeasonAndYear(Season Season) : IRequest<Result<AnimeListRoot>>;

    public class GetAnimeListBySeasonAndYearHandler : IRequestHandler<GetAnimeListBySeasonAndYear, Result<AnimeListRoot>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageCache _localStorageCache;
        public GetAnimeListBySeasonAndYearHandler(IHttpClientFactory httpClientFactory, ILocalStorageCache localStorageCache)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageCache = localStorageCache;
        }

        public async Task<Result<AnimeListRoot>> Handle(GetAnimeListBySeasonAndYear request, CancellationToken cancellationToken)
        {
            var cache = await _localStorageCache.GetOrCreateCacheAsync(
                 AnimelistConstants.CacheDataKey,
                 TimeSpan.FromHours(6),
                async () =>
                {
                    var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
                    var uriRequest = $"{AnimelistConstants.Endpoint}{request.Season.SeasonName}/{request.Season.Year}";
                    var response = await httpClient.GetAsync(uriRequest).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.ReadJson<AnimeListRoot>();

                        if (result is null)
                        {
                            return Result.Fail<AnimeListRoot>(AnimelistErrors.NullResult);
                        }

                        return result;
                    }
                    return Result.Fail<AnimeListRoot>(AnimelistErrors.NullResult);
                });

            return cache ??  Result.Fail<AnimeListRoot>(AnimelistErrors.NullResult); ;
        }
    }
}
