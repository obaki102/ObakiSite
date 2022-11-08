using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Features.Animelist.Services;
using System.Reflection;

namespace ObakiSite.Application.Extensions
{
    public static class AddApplicationDependencies
    {
        public static IServiceCollection AddHttpAnimeListService(this IServiceCollection services, Action<AnimeListOptions> options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddHttpClient<IAnimeListService, AnimeListService>();
            services.Configure(options);
            return services;
        }
        public static IServiceCollection AddSingletonAnimeListService(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.TryAddSingleton<IAnimeListService, AnimeListService>();
            return services;
        }

        public static IServiceCollection AddAppDependencies(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            //3rd Party
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
