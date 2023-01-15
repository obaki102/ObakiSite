using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Features.DefaultSettings.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Preference.Commands
{
    public record  SetPreference(Preferences Preference) : IRequest<ApplicationResponse>;
    public class SetPreferenceHandler : IRequestHandler<SetPreference, ApplicationResponse>
    {
        private readonly ILocalStorageCache _localStorageCache;
        public SetPreferenceHandler(ILocalStorageCache localStorageCache)
        {
            _localStorageCache = localStorageCache;
        }
        public async Task<ApplicationResponse> Handle(SetPreference request, CancellationToken cancellationToken)
        {
            try
            {
                await _localStorageCache.SetCacheAsync(PreferenceConstants.CacheDataKey, request.Preference);
                return ApplicationResponse.Success();
            }
            catch (Exception ex)
            {
                return ApplicationResponse.Fail(ex.Message);
            }
        }
    }
}
