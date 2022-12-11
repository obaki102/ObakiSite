using AutoMapper;
using MediatR;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Application.Extensions;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.DTO;
using ObakiSite.Shared.DTO.Response;
using System.Text.Json;

namespace ObakiSite.Application.Features.Posts.Commands
{
    public  record CreatePost(PostDTO post) : IRequest<ApplicationResponse>;

    public class CreatePostHandler : IRequestHandler<CreatePost, ApplicationResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        public CreatePostHandler(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }
        public async Task<ApplicationResponse> Handle(CreatePost request, CancellationToken cancellationToken)
        {
            var httpClient  = _httpClientFactory.CreateClient(HttpNameClient.Default);
            var post = _mapper.Map<Post>(request.post);
            var serializedPost = JsonSerializer.Serialize(post).ToJsonStringContent();
            var response = await httpClient.PostAsync(PostConstants.CreatePost.EndPoint, serializedPost).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<ApplicationResponse>(content).ConfigureAwait(false);

                if (result is null || !result.IsSuccess)
                {
                    return ApplicationResponse.Fail();
                }

                return result;
            }

            return ApplicationResponse.Fail(response.StatusCode.ToString());
        }
    }

}
