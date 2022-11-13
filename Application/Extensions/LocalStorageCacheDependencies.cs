using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using ObakiSite.Application.Features.LocalStorageCache.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            services.AddSingleton(typeof(ILocalStorageCache<>), typeof(LocalStorageCache<>));
            return services;
        }
    }
}
