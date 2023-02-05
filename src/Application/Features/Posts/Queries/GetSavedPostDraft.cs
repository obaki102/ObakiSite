using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.DTO;

namespace ObakiSite.Application.Features.Posts.Queries
{
    public record GetSavedPostDraft : IRequest<Result<PostDTO>>;

    public class GetSavedPostDraftHandler : IRequestHandler<GetSavedPostDraft, Result<PostDTO>>
    {
        private readonly ILocalStorageCache _localStorageCache;
        public GetSavedPostDraftHandler(ILocalStorageCache localStorageCache)
        {
            _localStorageCache = localStorageCache;
        }
        public async Task<Result<PostDTO>> Handle(GetSavedPostDraft request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _localStorageCache.GetCacheAsync<PostDTO>(PostConstants.CreatePost.CacheDataKey);
                if(result is not null)
                {
                    return result;
                }

                return Result.Fail<PostDTO>(new Error("GetSavedPostDraftHandlerError", "No data retrieved."));
            }
            catch (Exception ex)
            {

                return Result.Fail<PostDTO>(new Error("GetSavedPostDraftHandlerError", ex.Message));
            }
        }
    }
}
