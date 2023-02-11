using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ObakiSite.Application.Features.Animelist.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace ObakiSite.Api.Functions
{
    public class AnimelistFunction
    {
        private readonly IAnimeListService _animeListService;
        private readonly ILogger<AnimelistFunction> _logger;
        public AnimelistFunction(IAnimeListService animeListService, ILogger<AnimelistFunction> logger)
        {
            _animeListService = animeListService;
            _logger = logger;
        }

        [Function(nameof(AnimelistFunction))]
        [OpenApiOperation(operationId: "animelists", tags: new[] { "animelists" }, Summary = "Get animelist from myanimelist api", Description = "Retrieve top 100 anime list based on the given season and year", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "season", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Season")]
        [OpenApiParameter(name: "year", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "Year")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "Successful operation", Description = "Lists successfully  retrieved")]
        public async Task<HttpResponseData> GetAnimeListBySeasonAndYear(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "animelists/{season?}/{year:int?}")] HttpRequestData req,
            string season,
            int year)
        {
            _logger.LogInformation("AnimelistFunction processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var result = await _animeListService.GetAnimeListBySeasonAndYear(year, season);

            await response.WriteAsJsonAsync(result.Value);
            return response;
        }

    }
}
