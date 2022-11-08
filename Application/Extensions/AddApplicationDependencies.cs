using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Features.Animelist.Services;
using ObakiSite.Shared.Constants;
using System.Reflection;

namespace ObakiSite.Application.Extensions
{
    public static class AddApplicationDependencies
    {
        public static IServiceCollection AddHttpAnimeListService(this IServiceCollection services, Uri baseUrl, string defaultHeader)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddHttpClient(HttpNameClient.AnimeList, client =>
            {
                client.BaseAddress = baseUrl;
                client.DefaultRequestHeaders.Add(AnimeList.XmalClientId, defaultHeader);
            });
            services.TryAddSingleton<IAnimeListService, AnimeListService>();
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

        public static IServiceCollection AddAppDependencies(this IServiceCollection services, Uri baseUrl)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            //3rd Party
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //HttpNamedClient
            services.AddHttpClient(HttpNameClient.Default, client =>
            {
                client.BaseAddress = baseUrl;

            });
            return services;
        }
    }
}
