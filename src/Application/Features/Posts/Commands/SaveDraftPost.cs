﻿using MediatR;
using ObakiSite.Application.Features.LocalStorageCache.Services;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Posts.Commands
{
    public record SaveDraftPost(PostDTO Post) : IRequest<ApplicationResponse>;

    public class SaveDraftHandler : IRequestHandler<SaveDraftPost, ApplicationResponse>
    {
        private readonly ILocalStorageCache<PostDTO> _localStorageCache;
        public SaveDraftHandler(ILocalStorageCache<PostDTO> localStorageCache)
        {
            _localStorageCache = localStorageCache;
            _localStorageCache.Options = new LocalStorageCacheOptions
            {
                DataKey = PostConstants.CacheDataKey
            };
        }
        public async Task<ApplicationResponse> Handle(SaveDraftPost request, CancellationToken cancellationToken)
        {
            try
            {
                await _localStorageCache.SetData(request.Post);
                return ApplicationResponse.Success();
            }
            catch (Exception ex)
            {
                return ApplicationResponse.Fail(ex.Message);
            }
        }
    }

}
