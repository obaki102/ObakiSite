using ObakiSite.Client.Services.Animelist;
using ObakiSite.Shared.DTO;

using System.Net.Http.Json;

namespace ObakiSite.Client.Services.AnimeList
{
    public class AnimeListService : IAnimeListService
    {
        private readonly HttpClient _httpClient;
        public AnimeListService(HttpClient httpClient)
        {
            _httpClient = httpClient;
           
        }

        public IReadOnlyList<Datum> AnimeLists { get; set; } = default;

        public async Task<IReadOnlyList<Datum>> GetAnimeListBySeasonAndYear(Season season)
        {
            var uriRequest = $"/api/animelists/{season.SeasonOfTheYear}/{season.Year}";
            //todo: Cache it to local storage
            var response = await _httpClient.GetFromJsonAsync<AnimeListRoot>(uriRequest);
            if (response is not null )
            {
                    AnimeLists = response.Data;
            }
            return AnimeLists;
        }
    }
}
