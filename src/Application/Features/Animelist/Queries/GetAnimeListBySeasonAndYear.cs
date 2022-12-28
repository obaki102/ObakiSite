using MediatR;
using Obaki.LocalStorageCache;
using Obaki.LocalStorageCache.Extensions;
using ObakiSite.Application.Extensions;
using ObakiSite.Application.Features.Animelist.Constants;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Animelist.Queries
{
    public record GetAnimeListBySeasonAndYear(Season Season) : IRequest<ApplicationResponse<AnimeListRoot>>;

    public class GetAnimeListBySeasonAndYearHandler : IRequestHandler<GetAnimeListBySeasonAndYear, ApplicationResponse<AnimeListRoot>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageCache _localStorageCache;
        public GetAnimeListBySeasonAndYearHandler(IHttpClientFactory httpClientFactory, ILocalStorageCache localStorageCache)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageCache = localStorageCache;
        }

        public async Task<ApplicationResponse<AnimeListRoot>> Handle(GetAnimeListBySeasonAndYear request, CancellationToken cancellationToken)
        {
            return await _localStorageCache.GetOrCreateCacheAsync(
                 AnimelistConstants.CacheDataKey,
            async cache =>
            {
                cache.SetExpirationHrs(1);

                var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
                var uriRequest = $"{AnimelistConstants.Endpoint}{request.Season.SeasonName}/{request.Season.Year}";
                var response = await httpClient.GetAsync(uriRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.ConvertStreamToTAsync<AnimeListRoot>();

                    if (result is null)
                    {
                        return ApplicationResponse<AnimeListRoot>.Fail("No data.");
                    }

                    return ApplicationResponse<AnimeListRoot>.Success(result);
                }
                return ApplicationResponse<AnimeListRoot>.Fail("No data.");
            });
        }
    }
}
