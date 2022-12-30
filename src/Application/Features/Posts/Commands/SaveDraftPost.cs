using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Posts.Commands
{
    public record SaveDraftPost(PostDTO Post) : IRequest<ApplicationResponse>;

    public class SaveDraftHandler : IRequestHandler<SaveDraftPost, ApplicationResponse>
    {
        private readonly ILocalStorageCache _localStorageCache;
        public SaveDraftHandler(ILocalStorageCache localStorageCache)
        {
            _localStorageCache = localStorageCache;
        }
        public async Task<ApplicationResponse> Handle(SaveDraftPost request, CancellationToken cancellationToken)
        {
            try
            {
                await _localStorageCache.SetCacheAsync(PostConstants.CacheDataKey,request.Post);
                return ApplicationResponse.Success();
            }
            catch (Exception ex)
            {
                return ApplicationResponse.Fail(ex.Message);
            }
        }
    }
}
