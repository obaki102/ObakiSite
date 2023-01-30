using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Infra.Authentication;
using ObakiSite.Application.Infra.Data;
using ObakiSite.Application.Shared.Constants;

namespace ObakiSite.Application.Shared.Extensions
{
    public static class AuthServiceDependencies
    {
        public static IServiceCollection AddScopedAuthService(this IServiceCollection services, Action<AuthServiceOptions> options, string endPoint, string accessKey)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddDbContextFactory<ApplicationUserContext>(
            (DbContextOptionsBuilder opts) =>
            {
                opts.UseCosmos(
                     endPoint,
                     accessKey,
                     CosmosDBConstants.Database);
            });
            services.TryAddScoped<IAuthService, AuthService>();
            services.Configure(options);
            return services;
        }
    }
}
