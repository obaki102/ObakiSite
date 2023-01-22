using AutoMapper;
using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Extensions;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;
using ObakiSite.Application.Shared.Extensions;
using System.Text.Json;

namespace ObakiSite.Application.Features.Posts.Commands
{
    public  record CreatePost(PostDTO Post) : IRequest<ApplicationResponse>;

    public class CreatePostHandler : IRequestHandler<CreatePost, ApplicationResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageCache _localStorageCache;

        public CreatePostHandler(IHttpClientFactory httpClientFactory, ILocalStorageCache localStorageCache)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageCache = localStorageCache;
        }
        public async Task<ApplicationResponse> Handle(CreatePost request, CancellationToken cancellationToken)
        {

            var httpClient  = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var serializedPost = JsonSerializer.Serialize(request.Post).ToJsonStringContent();
            var response = await httpClient.PostAsync(PostConstants.CreatePost.EndPoint, serializedPost).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadJson<ApplicationResponse>();

                if (result is not null)
                {
                    await _localStorageCache.ClearCacheAsync(PostConstants.GetPostSummaries.CacheDataKey);
                    return result;
                }

                return ApplicationResponse.Fail("Unable to save post.");
            }

            return ApplicationResponse.Fail(response.StatusCode.ToString());
        }
    }

}
