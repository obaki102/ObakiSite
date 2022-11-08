using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ObakiSite.Application.Features.Animelist.Services;

namespace ObakiSite.Api.Functions
{
    public class AnimeList
    {
        private readonly IAnimeListService _animeListService;
        public AnimeList(IAnimeListService animeListService)
        {
            _animeListService = animeListService;
        }
        [FunctionName("GetAnimeListBySeasonAndYear")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "animelists/{season?}/{year:int?}")] HttpRequest req,
            string season,
            int year,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var response = await _animeListService.GetAnimeListBySeasonAndYear(year, season);
            if (response.IsSuccess)
            {
                return new OkObjectResult(response.Data);
            }
            return new BadRequestResult();

        }
    }
}
