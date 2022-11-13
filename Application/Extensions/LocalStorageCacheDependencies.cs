using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using ObakiSite.Application.Features.LocalStorageCache.Services;

namespace ObakiSite.Application.Extensions
{
    public static class LocalStorageCacheDependencies
    {
        public static IServiceCollection AddLocalStorageCahce(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddBlazoredLocalStorage();
            services.AddScoped(typeof(ILocalStorageCache<>), typeof(LocalStorageCache<>));
            return services;
        }
    }
}
