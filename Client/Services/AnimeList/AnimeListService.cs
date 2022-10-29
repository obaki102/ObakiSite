using ObakiSite.Client.Services.Animelist;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.DTO;
using static ObakiSite.Shared.DTO.AnimeListResponse;
using System.Text.Json;
using System.Net.Http;

namespace ObakiSite.Client.Services.AnimeList
{
    public class AnimeListService : IAnimeListService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public AnimeListService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri("https://api.myanimelist.net/");
            _httpClient.DefaultRequestHeaders.Add(AppSecrets.AnimeList.XmalClientId, _configuration[AppSecrets.AnimeList.AnimelistClientId]);

        }

        public IEnumerable<AnimeListResponse.Datum> AnimeLists { get; set; } = default;

        public async Task<IEnumerable<Datum>> GetAnimeListBySeasonAndYear(Season season)
        {
            var uriRequest = $"v2/anime/season/{season.Year}/{season.SeasonOfTheYear}?limit=100&fields=id,title,main_picture,alternative_titles,start_date,end_date,synopsis,mean,rank,popularity,num_list_users,num_scoring_users,nsfw,created_at,updated_at,media_type,status,genres,my_list_status,num_episodes,start_season,broadcast,source,average_episode_duration,rating,pictures,background,related_anime,related_manga,recommendations,studios,statistics";
            var response = await _httpClient.GetAsync(uriRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var animeListResult = await JsonSerializer.DeserializeAsync<AnimeListRoot>(result);
                if (animeListResult is not null && animeListResult.Data is not null)
                {
                    AnimeLists = animeListResult.Data;
                }
            }
            return AnimeLists;
        }
    }
}
