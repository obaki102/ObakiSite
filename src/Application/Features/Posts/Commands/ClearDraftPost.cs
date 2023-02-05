using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared;

namespace ObakiSite.Application.Features.Posts.Commands
{
    public record ClearDraftPost : IRequest<Result>;

    public class ClearDraftPosthandler : IRequestHandler<ClearDraftPost, Result>
    {
        private readonly ILocalStorageCache _localStorageCache;
        public ClearDraftPosthandler(ILocalStorageCache localStorageCache)
        {
            _localStorageCache = localStorageCache;
        }
        public async Task<Result> Handle(ClearDraftPost request, CancellationToken cancellationToken)
        {
            try
            {
                await _localStorageCache.ClearCacheAsync(PostConstants.CreatePost.CacheDataKey);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("ClearDraftPosthandlerError",ex.Message));
            }
        }
    }



}
