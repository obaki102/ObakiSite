using ObakiSite.Application.Features.Animelist.Constants;
using ObakiSite.Application.Features.Animelist.DTO;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO.Response;
using ObakiSite.Application.Shared.Extensions;

namespace ObakiSite.Application.Features.Animelist.Services
{
    public class AnimeListService : IAnimeListService
    {
        private readonly HttpClient _httpClient;
        public AnimeListService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(HttpNameClientConstants.AnimeList);
        }

        public async Task<ApplicationResponse<AnimeListRoot>> GetAnimeListBySeasonAndYear(int year, string season)
        {
            //todo: clean the magic strings
            var uriRequest = $"v2/anime/season/{year}/{season}{AnimelistConstants.UrLQuery}";
            var response = await _httpClient.GetAsync(uriRequest).ConfigureAwait(false);
            
            if (response.IsSuccessStatusCode)
            {
                var data = await response.ConvertStreamToTAsync<AnimeListRoot>();

                if (data is null)
                {
                    return ApplicationResponse<AnimeListRoot>.Fail("Content is empty.");
                }

                return ApplicationResponse<AnimeListRoot>.Success(data);
            }

            return ApplicationResponse<AnimeListRoot>.Fail(response.StatusCode.ToString());
        }
    }
}
