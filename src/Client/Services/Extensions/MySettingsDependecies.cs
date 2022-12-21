using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Client.Services.Settings;
using System.Reflection;

namespace ObakiSite.Client.Services.Extensions
{
    public static class MySettingsDependecies
    {
        public static IServiceCollection AddMySettings(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.TryAddScoped<IMySettings, MySettings>();
            return services;
        }
    }
}
