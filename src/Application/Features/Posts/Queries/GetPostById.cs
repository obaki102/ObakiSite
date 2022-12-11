using AutoMapper;
using MediatR;
using ObakiSite.Application.Domain.Entities;
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
        private readonly IMapper _mapper;
        public GetPostByIdHandler(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }
        public async Task<ApplicationResponse<PostDTO>> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClient.Default);
            var uriRequest = $"{PostConstants.GetPostById.EndPoint}{request.Id}";
            var response = await httpClient.GetAsync(uriRequest).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<ApplicationResponse<Post>>(content).ConfigureAwait(false);
                if (result is null)
                {
                    return ApplicationResponse<PostDTO>.Fail("No data.");
                }
                var data = _mapper.Map<PostDTO>(result.Data);
                return ApplicationResponse<PostDTO>.Success(data);
            }
            return ApplicationResponse<PostDTO>.Fail(response.StatusCode.ToString());
        }
    }
}
    
