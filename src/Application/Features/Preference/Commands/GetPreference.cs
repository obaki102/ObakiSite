using MediatR;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Features.DefaultSettings.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Preference.Commands
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
                var cache = await _localStorageCache.GetOrCreateCacheAsync(
                 PreferenceConstants.CacheDataKey,
                 TimeSpan.FromHours(6),
                   () =>
                   {
                       return ValueTask.FromResult(ApplicationResponse<Preferences>.Success(new Preferences()));
                   });

                return cache ?? ApplicationResponse<Preferences>.Fail("No cache data retrieved.");
            }
            catch (Exception ex)
            {

                return ApplicationResponse<Preferences>.Fail(ex.Message);
            }

        }
    }
}
