using AutoMapper;
using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Extensions;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Posts.Commands
{
    public record DeletePost(string Id) : IRequest<ApplicationResponse>;

    public class DeletePostHandler : IRequestHandler<DeletePost, ApplicationResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageCache _localStorageCache;
        public DeletePostHandler(IHttpClientFactory httpClientFactory, ILocalStorageCache localStorageCache)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageCache = localStorageCache;
        }
        public async Task<ApplicationResponse> Handle(DeletePost request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var uriRequest = $"{PostConstants.DeletePost.EndPoint}{request.Id}";
            var response = await httpClient.DeleteAsync(uriRequest).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadJson<ApplicationResponse>();

                if (result is not null)
                {
                    await _localStorageCache.ClearCacheAsync(PostConstants.GetPostSummaries.CacheDataKey);
                    return result;
                }

                return ApplicationResponse.Fail("No data retrieved.");
            }

            return ApplicationResponse.Fail(response.StatusCode.ToString());
        }
    }


}
