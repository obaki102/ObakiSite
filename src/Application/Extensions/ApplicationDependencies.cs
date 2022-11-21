using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ObakiSite.Shared.Constants;
using System.Reflection;

namespace ObakiSite.Application.Extensions
{
    public static class ApplicationDependencies
    {
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
            services.AddLocalStorageCahce();
            return services;
        }
    }
}
