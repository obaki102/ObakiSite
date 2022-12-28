using MediatR;
using Obaki.LocalStorageCache;
using Obaki.LocalStorageCache.Extensions;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;


namespace ObakiSite.Application.Features.Posts.Queries
{
    public record GetSavedPostDraft : IRequest<ApplicationResponse<PostDTO>>;

    public class GetSavedPostDraftHandler : IRequestHandler<GetSavedPostDraft, ApplicationResponse<PostDTO>>
    {
        private readonly ILocalStorageCache _localStorageCache;
        public GetSavedPostDraftHandler(ILocalStorageCache localStorageCache)
        {
            _localStorageCache = localStorageCache;
        }
        public async Task<ApplicationResponse<PostDTO>> Handle(GetSavedPostDraft request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _localStorageCache.GetCacheAsync<PostDTO>(PostConstants.CacheDataKey);
                if(result is not null)
                {
                    return  ApplicationResponse<PostDTO>.Success(result);
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
