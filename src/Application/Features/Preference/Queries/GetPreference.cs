using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Features.DefaultSettings.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Preference.Queries
{
    public record GetPreferences : IRequest<ApplicationResponse<Preferences>>;

    public class GetPreferencesHandler : IRequestHandler<GetPreferences, ApplicationResponse<Preferences>>
    {
        private readonly ILocalStorageCache _localStorageCache;
        public GetPreferencesHandler(ILocalStorageCache localStorageCache)
        {
            _localStorageCache = localStorageCache;
        }
        public async Task<ApplicationResponse<Preferences>> Handle(GetPreferences request, CancellationToken cancellationToken)
        {
            try
            {
                var cache = await _localStorageCache.GetCacheAsync<Preferences>(PreferenceConstants.CacheDataKey);
                return ApplicationResponse<Preferences>.Success(cache ?? new Preferences());
            }
            catch (Exception ex)
            {

                return ApplicationResponse<Preferences>.Fail(ex.Message);
            }

        }
    }
}
