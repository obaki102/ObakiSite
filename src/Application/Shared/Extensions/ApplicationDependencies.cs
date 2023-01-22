using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Extensions;
using ObakiSite.Application.Shared.Behaviours.Validation;
using ObakiSite.Application.Shared.Constants;
using Polly;
using Polly.Contrib.WaitAndRetry;
using System.Reflection;

namespace ObakiSite.Application.Shared.Extensions
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
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Middleware
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //HttpNamedClient
            services.AddHttpClient(HttpNameClientConstants.Default, client =>
            {
                client.BaseAddress = baseUrl;

            })
            .AddTransientHttpErrorPolicy(policyBuilder =>
             policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 3), (exception, timeSpan, retryCount, context) =>
             {
                 Console.WriteLine(exception);
             })); ;
            services.AddLocalStorageCacheAsSingleton();
            return services;
        }

        public static IServiceCollection AddApiDependenciesWithCosmos(this IServiceCollection services, string animeListClientId,
            string emailAppPassword, string cosmosDBEndpoint, string cosmosDbAccessKey)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddHttpAnimeListService(animeListClientId);

            services.AddEmailService(options =>
            {
                options.AppPassword = emailAppPassword;
            });

            services.AddPostCosmosService(cosmosDBEndpoint, cosmosDbAccessKey);
            return services;
        }

        public static IServiceCollection AddApiDependenciesWithFirebase(this IServiceCollection services, string animeListClientId,
            string emailAppPassword, string projectId, string serviceAccount)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddHttpAnimeListService(animeListClientId);

            services.AddEmailService(options =>
            {
                options.AppPassword = emailAppPassword;
            });

            services.AddPostFirebaseService(projectId, serviceAccount);
            return services;
        }
    }
}
