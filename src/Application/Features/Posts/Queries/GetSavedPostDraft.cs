using MediatR;
using ObakiSite.Application.Features.LocalStorageCache.Services;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;


namespace ObakiSite.Application.Features.Posts.Queries
{
    public record GetSavedPostDraft : IRequest<ApplicationResponse<PostDTO>>;

    public class GetSavedPostDraftHandler : IRequestHandler<GetSavedPostDraft, ApplicationResponse<PostDTO>>
    {
        private readonly ILocalStorageCache<PostDTO> _localStorageCache;
        public GetSavedPostDraftHandler(ILocalStorageCache<PostDTO> localStorageCache)
        {
            _localStorageCache = localStorageCache;
            _localStorageCache.Options = new LocalStorageCacheOptions
            {
                DataKey = PostConstants.CacheDataKey
            };
        }
        public async Task<ApplicationResponse<PostDTO>> Handle(GetSavedPostDraft request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _localStorageCache.GetCacheData();

                if(result is not null)
                {
                    return result;
                }

                return ApplicationResponse<PostDTO>.Fail("No data retrieved.");
            }
            catch (Exception ex)
            {

                return ApplicationResponse<PostDTO>.Fail(ex.Message);
            }
        }
    }
}
