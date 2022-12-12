using AutoMapper;
using MediatR;
using ObakiSite.Application.Extensions;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.DTO;
using ObakiSite.Shared.DTO.Response;
using System.Text.Json;

namespace ObakiSite.Application.Features.Posts.Queries
{
    public record GetPostById(string Id) : IRequest<ApplicationResponse<PostDTO>>;

    public class GetPostByIdHandler : IRequestHandler<GetPostById, ApplicationResponse<PostDTO>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GetPostByIdHandler(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApplicationResponse<PostDTO>> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClient.Default);
            var uriRequest = $"{PostConstants.GetPostById.EndPoint}{request.Id}";
            var response = await httpClient.GetAsync(uriRequest).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ConvertStreamToTAsync<ApplicationResponse<PostDTO>>();

                if (result is not null && result.IsSuccess)
                {
                    return result;
                }

                return ApplicationResponse<PostDTO>.Fail("No data retrieved.");
            }

            return ApplicationResponse<PostDTO>.Fail(response.StatusCode.ToString());
        }
    }
}
    
