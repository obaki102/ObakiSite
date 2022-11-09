using Blazored.LocalStorage;
using MediatR;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.DTO;
using ObakiSite.Shared.Models.Response;
using System.Net.Http.Json;

namespace ObakiSite.Application.Features.Animelist.Queries
{
    public record GetAnimeListBySeasonAndYear(Season Season) : IRequest<ApplicationResponse<AnimeListRoot>>;

    public class GetAnimeListBySeasonAndYearHandler : IRequestHandler<GetAnimeListBySeasonAndYear, ApplicationResponse<AnimeListRoot>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorageService;
        public GetAnimeListBySeasonAndYearHandler(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageService = localStorageService;
        }
        public async Task<ApplicationResponse<AnimeListRoot>> Handle(GetAnimeListBySeasonAndYear request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClient.Default);
            var uriRequest = $"/api/animelists/{request.Season.SeasonOfTheYear}/{request.Season.Year}";
            //todo create a reusable cache service
            var cacheData = await _localStorageService.GetItemAsync<AnimeListRoot>(AnimeList.CacheDataKey);
            var data = cacheData;
            var cacheDataCreateDate = await _localStorageService.GetItemAsync<DateTime?>(AnimeList.CacheDataCreateDateKey);
            double totalHrsSinceCacheCreated = 0;
            if (cacheDataCreateDate is not null)
            {
                totalHrsSinceCacheCreated = DateTime.UtcNow.Subtract((DateTime)cacheDataCreateDate).TotalHours;
            }

            if (cacheData is null || totalHrsSinceCacheCreated > 6)
            {
                var response = await httpClient.GetFromJsonAsync<AnimeListRoot>(uriRequest);
                data = response;
                await _localStorageService.SetItemAsync(AnimeList.CacheDataKey, response);
                await _localStorageService.SetItemAsync(AnimeList.CacheDataCreateDateKey, DateTime.UtcNow);
            }

            if (data is null)
            {
                return ApplicationResponse<AnimeListRoot>.Fail("No response returned.");
            }

            return ApplicationResponse<AnimeListRoot>.Success(data);

        }
    }
}
