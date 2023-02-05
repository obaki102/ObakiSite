using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.DTO;

namespace ObakiSite.Application.Features.Posts.Commands
{
    public record SaveDraftPost(PostDTO Post) : IRequest<Result>;

    public class SaveDraftHandler : IRequestHandler<SaveDraftPost, Result>
    {
        private readonly ILocalStorageCache _localStorageCache;
        public SaveDraftHandler(ILocalStorageCache localStorageCache)
        {
            _localStorageCache = localStorageCache;
        }
        public async Task<Result> Handle(SaveDraftPost request, CancellationToken cancellationToken)
        {
            try
            {
                await _localStorageCache.SetCacheAsync(PostConstants.CreatePost.CacheDataKey,request.Post);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(Error.HttpError(ex.Message));
            }
        }
    }
}

