
using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Posts.Commands
{
    public record ClearDraftPost : IRequest<ApplicationResponse>;

    public class ClearDraftPosthandler : IRequestHandler<ClearDraftPost, ApplicationResponse>
    {
        private readonly ILocalStorageCache _localStorageCache;
        public ClearDraftPosthandler(ILocalStorageCache localStorageCache)
        {
            _localStorageCache = localStorageCache;
        }
        public async Task<ApplicationResponse> Handle(ClearDraftPost request, CancellationToken cancellationToken)
        {
            try
            {
                await _localStorageCache.ClearCacheAsync(PostConstants.GetPostSummaries.CacheDataKey);
                return ApplicationResponse.Success();
            }
            catch (Exception ex)
            {
                return ApplicationResponse.Fail(ex.Message);
            }
        }
    }



}
