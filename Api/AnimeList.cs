using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using static ObakiSite.Shared.DTO.AnimeListResponse;
using System;
using ObakiSite.Shared.Constants;

namespace ObakiApi
{
    public static class AnimeList
    {
        public static HttpClient httpClient = new HttpClient();

        [FunctionName("GetAnimeListBySeasonAndYear")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "animelists/{season?}/{year:int?}")] HttpRequest req, 
            string season,
            int year,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            httpClient.BaseAddress = new Uri("https://api.myanimelist.net/");
            httpClient.DefaultRequestHeaders.Add(AppSecrets.AnimeList.XmalClientId, Environment.GetEnvironmentVariable(AppSecrets.AnimeList.AnimelistClientId));
            var uriRequest = $"v2/anime/season/{year}/{season}?limit=100&fields=id,title,main_picture,alternative_titles,start_date,end_date,synopsis,mean,rank,popularity,num_list_users,num_scoring_users,nsfw,created_at,updated_at,media_type,status,genres,my_list_status,num_episodes,start_season,broadcast,source,average_episode_duration,rating,pictures,background,related_anime,related_manga,recommendations,studios,statistics";
            var response = await httpClient.GetAsync(uriRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var data = await JsonSerializer.DeserializeAsync<AnimeListRoot>(result);
                return new OkObjectResult(data);
            }
            return new BadRequestResult();

        }
    }
}
