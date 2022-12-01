using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ObakiSite.Application.Features.Animelist.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using ObakiSite.Shared.Models.Response;

namespace ObakiSite.Api.Functions
{
    public class AnimeList
    {
        private readonly IAnimeListService _animeListService;
        private readonly ILogger<AnimeList> _logger;
        public AnimeList(IAnimeListService animeListService, ILogger<AnimeList> logger)
        {
            _animeListService = animeListService;
            _logger = logger;
        }
        [Function("GetAnimeListBySeasonAndYear")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "animelists/{season?}/{year:int?}")] HttpRequestData req,
            string season,
            int year)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = req.CreateResponse(HttpStatusCode.OK);
            var result = await _animeListService.GetAnimeListBySeasonAndYear(year, season);

            if (result.IsSuccess)
            {
                await response.WriteAsJsonAsync(result.Data);
                return response;
            }

            await response.WriteAsJsonAsync(ApplicationResponse.Fail("Unable to fetch data."));
            return response;

        }

    }
}
