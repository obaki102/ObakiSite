using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Features.Posts.Services;
using ObakiSite.Shared.Constants;
using System.Reflection;

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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContextFactory<PostContext>(
             (DbContextOptionsBuilder opts) =>
               {
                   opts.UseCosmos(
                       endPoint,
                       accessKey,
                       CosmosDB.Database);
               });
            services.TryAddScoped<IPostService, PostService>();
            return services;
        }
    }
}
