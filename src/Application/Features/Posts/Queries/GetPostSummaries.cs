using AutoMapper;
using MediatR;
using ObakiSite.Application.Extensions;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.DTO;
using ObakiSite.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Posts.Queries
{
    public record GetPostSummaries : IRequest<ApplicationResponse<IReadOnlyList<PostSummaryDTO>>>;

    public class GetPostSummariesHandler : IRequestHandler<GetPostSummaries, ApplicationResponse<IReadOnlyList<PostSummaryDTO>>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GetPostSummariesHandler(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
        }
      
        public async Task<ApplicationResponse<IReadOnlyList<PostSummaryDTO>>> Handle(GetPostSummaries request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClient.Default);
            var uriRequest = $"{PostConstants.GetPostSummaries.EndPoint}";
            var response = await httpClient.GetAsync(uriRequest).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                //To do implement caching
                var result = await response.ConvertStreamToTAsync<ApplicationResponse<IReadOnlyList<PostSummaryDTO>>>();
                if (result is not null && result.IsSuccess)
                {
                    return result;
                }

                return ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Fail("No data retrieved.");
            }

            return ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Fail(response.StatusCode.ToString());
        }
    }
}
