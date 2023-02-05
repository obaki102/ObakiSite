using ObakiSite.Application.Features.Animelist.Constants;
using ObakiSite.Application.Features.Animelist.DTO;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.Extensions;
using ObakiSite.Application.Errors;

namespace ObakiSite.Application.Features.Animelist.Services
{
    public class AnimeListService : IAnimeListService
    {
        private readonly HttpClient _httpClient;
        public AnimeListService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(HttpNameClientConstants.AnimeList);
        }

        public async Task<Result<AnimeListRoot>> GetAnimeListBySeasonAndYear(int year, string season)
        {
            //todo: clean the magic strings
            var uriRequest = $"v2/anime/season/{year}/{season}{AnimelistConstants.UrLQuery}";
            var response = await _httpClient.GetAsync(uriRequest).ConfigureAwait(false);
            
            if (response.IsSuccessStatusCode)
            {
                var data = await response.ConvertStreamToTAsync<AnimeListRoot>();

                if (data is null)
                {
                    return Result.Fail<AnimeListRoot>(AnimelistErrors.NullResult);
                }

                return data;
            }

            return Result.Fail<AnimeListRoot>(AnimelistErrors.HttpError(response.StatusCode.ToString()));
        }
    }
}
