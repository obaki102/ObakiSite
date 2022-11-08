using MediatR;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.DTO;
using ObakiSite.Shared.Models.Response;
using System.Net.Http.Json;

namespace ObakiSite.Application.Features.Animelist.Queries
{
    public record GetAnimeListBySeasonAndYear(Season Season) : IRequest<ApplicationResponse<AnimeListRoot>>;

    public class GetAnimeListBySeasonAndYearHandler : IRequestHandler<GetAnimeListBySeasonAndYear, ApplicationResponse<AnimeListRoot>>
    {
        private readonly HttpClient _httpClient;
        public GetAnimeListBySeasonAndYearHandler(IHttpClientFactory httpFactory)
        {
            _httpClient = httpFactory.CreateClient(HttpNameClient.AnimeList);
        }
        public async Task<ApplicationResponse<AnimeListRoot>> Handle(GetAnimeListBySeasonAndYear request, CancellationToken cancellationToken)
        {
            var uriRequest = $"/api/animelists/{request.Season.SeasonOfTheYear}/{request.Season.Year}";
            //todo: Cache it to local storage
            var response = await _httpClient.GetFromJsonAsync<AnimeListRoot>(uriRequest);
            if (response is not null)
            {
                return ApplicationResponse<AnimeListRoot>.Success(response);
            }
            return ApplicationResponse<AnimeListRoot>.Fail("No response returned.");

        }
    }
}
