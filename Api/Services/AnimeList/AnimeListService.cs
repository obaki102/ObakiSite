using ObakiSite.Api.DTO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObakiSite.Api.Services.AnimeList
{
    public class AnimeListService : IAnimeListService
    {
        private readonly HttpClient _httpClient;
        public AnimeListService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AnimeList");
        }
        public async Task<AnimeListRoot> GetAnimeListBySeasonAndYear(int year, string season)
        {
            var uriRequest = $"v2/anime/season/{year}/{season}?limit=100&fields=id,title,main_picture,alternative_titles,start_date,end_date,synopsis,mean,rank,popularity,num_list_users,num_scoring_users,nsfw,created_at,updated_at,media_type,status,genres,my_list_status,num_episodes,start_season,broadcast,source,average_episode_duration,rating,pictures,background,related_anime,related_manga,recommendations,studios,statistics";
            var response = await _httpClient.GetAsync(uriRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<AnimeListRoot>(result);
            }

            return null;
        }
    }
}
