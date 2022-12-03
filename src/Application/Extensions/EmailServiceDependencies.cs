using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Features.Email.Services;
using ObakiSite.Shared.Constants;
using Polly.Contrib.WaitAndRetry;
using Polly;

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
            
            services.AddHttpClient(HttpNameClient.Email).AddTransientHttpErrorPolicy(policyBuilder =>
             policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 3), (exception, timeSpan, retryCount, context) =>
             {
                 // todo: log exception and retries.
             })); 
            services.TryAddSingleton<IEmailService, EmailService>();
            services.Configure(options);
            return services;
        }
    }
}

