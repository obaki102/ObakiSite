using AutoMapper;
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
    public record UpdatePost(PostDTO Post) : IRequest<Result>;

    public class UpdatePostHandler : IRequestHandler<UpdatePost, Result>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageCache _localStorageCache;

        public UpdatePostHandler(IHttpClientFactory httpClientFactory, ILocalStorageCache localStorageCache)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageCache = localStorageCache;
        }
        public async Task<Result> Handle(UpdatePost request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var serializedPost = JsonSerializer.Serialize(request.Post).ToJsonStringContent();
            var response = await httpClient.PutAsync(PostConstants.UpdatePost.EndPoint, serializedPost).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadJson<bool>();

                if (result)
                {
                    await _localStorageCache.ClearCacheAsync(PostConstants.GetPostSummaries.CacheDataKey);
                    return Result.Success();
                }

                return Result.Fail(new Error("UpdatePostHandlerError", "Unable to update post."));
            }

            return Result.Fail(Error.HttpError(response.StatusCode.ToString()));
        }
    }
}

        
   
