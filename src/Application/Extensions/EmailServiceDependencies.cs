using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Features.Email.Services;

namespace ObakiSite.Application.Extensions
{
    public static class EmailServiceDependencies
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services, Action<EmailServiceOptions> options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            //In blazor wasm scoped lifetime behaves the same way as singleton.  
            services.TryAddSingleton<IEmailService, EmailService>();
            services.Configure(options);
            return services;
        }
    }
}

