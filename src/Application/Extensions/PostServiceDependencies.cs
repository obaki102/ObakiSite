﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Features.Posts.Services;
using ObakiSite.Shared.Constants;

namespace ObakiSite.Application.Extensions
{
    public static class PostServiceDependencies
    {
        public static IServiceCollection AddPostService(this IServiceCollection services, string endPoint, string accessKey)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddDbContextFactory<PostContext>(
             (DbContextOptionsBuilder opts) =>
               {
                   opts.UseCosmos(
                       endPoint,
                       accessKey,
                       CosmosDBConstants.Database);
               });
            services.TryAddScoped<IPostService, PostService>();
            return services;
        }
    }
}
