
using MediatR;
using ObakiSite.Application.Features.LocalStorageCache.Services;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.DTO;
using ObakiSite.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Posts.Commands
{
    public record ClearDraftPost : IRequest<ApplicationResponse>;

    public class ClearDraftPosthandler : IRequestHandler<ClearDraftPost, ApplicationResponse>
    {
        private readonly ILocalStorageCache<PostDTO> _localStorageCache;
        public ClearDraftPosthandler(ILocalStorageCache<PostDTO> localStorageCache)
        {
            _localStorageCache = localStorageCache;
            _localStorageCache.Options = new LocalStorageCacheOptions
            {
                CreationDateKey = PostConstants.CacheDataCreateDateKey,
                DataKey = PostConstants.CacheDataKey,
                NumberOfHrsToRefreshCache = 6
            };
        }
        public async Task<ApplicationResponse> Handle(ClearDraftPost request, CancellationToken cancellationToken)
        {
            try
            {
                await _localStorageCache.ClearCache();
                return ApplicationResponse.Success();
            }
            catch (Exception ex)
            {
                return ApplicationResponse.Fail(ex.Message);
            }
        }
    }



}
