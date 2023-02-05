using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.Extensions;
using System.Text.Json;

namespace ObakiSite.Application.Features.Posts.Commands
{
    public  record CreatePost(PostDTO Post) : IRequest<Result>;

    public class CreatePostHandler : IRequestHandler<CreatePost, Result>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageCache _localStorageCache;

        public CreatePostHandler(IHttpClientFactory httpClientFactory, ILocalStorageCache localStorageCache)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageCache = localStorageCache;
        }
        public async Task<Result> Handle(CreatePost request, CancellationToken cancellationToken)
        {

            var httpClient  = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var serializedPost = JsonSerializer.Serialize(request.Post).ToJsonStringContent();
            var response = await httpClient.PostAsync(PostConstants.CreatePost.EndPoint, serializedPost).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadJson<bool>();

                if (!result)
                {
                    await _localStorageCache.ClearCacheAsync(PostConstants.GetPostSummaries.CacheDataKey);
                    return Result.Success();
                }

                return Result.Fail(new Error("CreatePostHandlerError", "Unable to save post."));
            }

            return Result.Fail(Error.HttpError(response.StatusCode.ToString()));
        }
    }

}
