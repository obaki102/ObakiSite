using AutoMapper;
using MediatR;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.Extensions;

namespace ObakiSite.Application.Features.Posts.Queries
{
    public record GetPostById(string Id) : IRequest<Result<PostDTO>>;

    public class GetPostByIdHandler : IRequestHandler<GetPostById, Result<PostDTO>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GetPostByIdHandler(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Result<PostDTO>> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var uriRequest = $"{PostConstants.GetPostById.EndPoint}{request.Id}";
            var response = await httpClient.GetAsync(uriRequest).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadJson<PostDTO>();

                if (result is not null)
                {
                    return result;
                }

                return Result.Fail<PostDTO>(new Error("GetPostByIdHandlerError", "No data retrieved."));
            }

            return Result.Fail<PostDTO>(Error.HttpError(response.StatusCode.ToString()));
        }
    }
}
    
