using MediatR;
using ObakiSite.Application.Features.DefaultSettings.Constants;
using ObakiSite.Application.Features.LocalStorageCache.Services;
using ObakiSite.Shared.DTO;
using ObakiSite.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Preference.Commands
{
    public record GetPreferences : IRequest<ApplicationResponse<Preferences>>;

    public class GetPreferencesHandler : IRequestHandler<GetPreferences, ApplicationResponse<Preferences>>
    {
        private readonly ILocalStorageCache<Preferences> _localStorageCache;
        public GetPreferencesHandler(ILocalStorageCache<Preferences> localStorageCache)
        {
            _localStorageCache = localStorageCache;
            _localStorageCache.Options = new LocalStorageCacheOptions
            {
                DataKey = PreferenceConstants.CacheDataKey
            };
        }
        public async Task<ApplicationResponse<Preferences>> Handle(GetPreferences request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _localStorageCache.GetCacheData();

                if (result is not null)
                {
                    if (!result.IsSuccess)
                    {
                        var defaulSetting = new Preferences();
                        await _localStorageCache.SetData(defaulSetting);
                        var refreshedData = await _localStorageCache.GetCacheData();
                        return refreshedData;
                    }

                    return result;
                }

                return ApplicationResponse<Preferences>.Fail("No data retrieved.");
            }
            catch (Exception ex)
            {

                return ApplicationResponse<Preferences>.Fail(ex.Message);
            }

        }
    }
}
