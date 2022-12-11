using System.Text.Json;
using ObakiSite.Application.Features.Animelist.DTO;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Animelist.Services
{
    public class AnimeListService : IAnimeListService
    {
        private readonly HttpClient _httpClient;
        public AnimeListService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(HttpNameClient.AnimeList);
        }

        public async Task<ApplicationResponse<AnimeListRoot>> GetAnimeListBySeasonAndYear(int year, string season)
        {
            //todo: clean the magic strings
            var uriRequest = $"v2/anime/season/{year}/{season}?limit=100&fields=id,title,main_picture,alternative_titles,start_date,end_date,synopsis,mean,popularity,num_list_users,num_scoring_users,nsfw,created_at,updated_at,media_type,status,genres,my_list_status,num_episodes,start_season,broadcast,source,average_episode_duration,rating,pictures,background,related_anime,related_manga,recommendations,studios,statistics";
            var response = await _httpClient.GetAsync(uriRequest).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                if (result is null)
                {
                    return ApplicationResponse<AnimeListRoot>.Fail("Response is empty.");
                }
                var data = await JsonSerializer.DeserializeAsync<AnimeListRoot>(result);
                if (data is null)
                {
                    return ApplicationResponse<AnimeListRoot>.Fail("Data is empty.");
                }
                return ApplicationResponse<AnimeListRoot>.Success(data);
            }
            return ApplicationResponse<AnimeListRoot>.Fail(response.StatusCode.ToString());
        }
    }
}
