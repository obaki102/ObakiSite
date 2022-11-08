﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Features.Animelist.Services;

namespace ObakiSite.Application.Extensions
{
    public static class AddApplicationDependencies
    {


        public static IServiceCollection AddSingletonAnimeListService(this IServiceCollection services, Action<AnimeListOptions> options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddHttpClient<IAnimeListService, AnimeListService>();
            services.TryAddSingleton<IAnimeListService, AnimeListService>();
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
    }
}
