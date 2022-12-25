using Google.Cloud.Firestore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Features.Posts.Services;
using ObakiSite.Application.Infra.Data;
using ObakiSite.Application.Shared.Constants;

namespace ObakiSite.Application.Extensions
{
    public static class PostServiceDependencies
    {
        public static IServiceCollection AddPostCosmosService(this IServiceCollection services, string endPoint, string accessKey)
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
            services.TryAddScoped<IPostService, PostCosmosService>();
            return services;
        }

        public static IServiceCollection AddPostFirebaseService(this IServiceCollection services, string projectId, string serviceAccount)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddSingleton(_ => new FirestoreProvider(
              new FirestoreDbBuilder
              {
                  ProjectId = projectId,
                  JsonCredentials = serviceAccount
              }.Build()
            ));
            services.TryAddScoped<IPostService, PostFirebaseService>();
            return services;
        }
    }
}
