using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Features.Posts.Services;
namespace ObakiSite.Application.Extensions
{
    public static class PostDependencies
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
                       nameof(PostContext));
               });
            services.TryAddScoped<IPostService, PostService>();
            return services;
        }
    }
}
