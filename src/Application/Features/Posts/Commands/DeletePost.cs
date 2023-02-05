using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO.Response;
using ObakiSite.Application.Shared.Extensions;

namespace ObakiSite.Application.Features.Posts.Commands
{
    public record DeletePost(string Id) : IRequest<Result>;

    public class DeletePostHandler : IRequestHandler<DeletePost, Result>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageCache _localStorageCache;
        public DeletePostHandler(IHttpClientFactory httpClientFactory, ILocalStorageCache localStorageCache)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageCache = localStorageCache;
        }
        public async Task<Result> Handle(DeletePost request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var uriRequest = $"{PostConstants.DeletePost.EndPoint}{request.Id}";
            var response = await httpClient.DeleteAsync(uriRequest).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadJson<Result>();

                if (result is not null)
                {
                    await _localStorageCache.ClearCacheAsync(PostConstants.GetPostSummaries.CacheDataKey);
                    return result;
                }

                return Result.Fail(new Error("DeletePostHandlerError", "No data retrieved."));
            }

            return Result.Fail(Error.HttpError(response.StatusCode.ToString()));
        }
    }


}
