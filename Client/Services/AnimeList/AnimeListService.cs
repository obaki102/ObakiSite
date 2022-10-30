using ObakiSite.Client.Services.Animelist;
using ObakiSite.Shared.DTO;

using System.Net.Http.Json;

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
           
        }

        public IReadOnlyList<Datum> AnimeLists { get; set; } = default;

        public async Task<IReadOnlyList<Datum>> GetAnimeListBySeasonAndYear(Season season)
        {
            var uriRequest = $"/v2/anime/season/{season.Year}/{season.SeasonOfTheYear}?limit=100&fields=id,title,main_picture,alternative_titles,start_date,end_date,synopsis,mean,rank,popularity,num_list_users,num_scoring_users,nsfw,created_at,updated_at,media_type,status,genres,my_list_status,num_episodes,start_season,broadcast,source,average_episode_duration,rating,pictures,background,related_anime,related_manga,recommendations,studios,statistics";
            var uriRequest2 = $"/api/animelists/{season.SeasonOfTheYear}/{season.Year}";
            var response = await _httpClient.GetFromJsonAsync<AnimeListRoot>(uriRequest2);
            if (response is not null )
            {
                    AnimeLists = response.Data;
            }
            return AnimeLists;
        }
    }
}
