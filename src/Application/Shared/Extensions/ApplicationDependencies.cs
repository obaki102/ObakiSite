using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Obaki.LocalStorageCache;
using ObakiSite.Application.Extensions;
using ObakiSite.Application.Shared.Behaviours.Validation;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.Settings;
using Polly;
using Polly.Contrib.WaitAndRetry;
using System.Configuration;
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
        
        public static IServiceCollection AddApiDependenciesWithCosmos(this IServiceCollection services, IConfiguration config)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

           var settings = config.GetSection(DefaultConstants.WebApiSettings).Get<WebApiSettings>();

            if(settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            services.AddHttpAnimeListService(settings.AnimelistClientId);
            services.AddEmailService(options =>
            {
                options.AppPassword = settings.AppPassword;
            });

            services.AddPostCosmosService(settings.CosmosEndPoint, settings.CosmosAccessKey);
            services.AddScopedAuthService(options =>
            {
                options.TokenKey = settings.TokenKey;
            }, settings.CosmosEndPoint, settings.CosmosAccessKey);
            return services;
        }

        public static IServiceCollection AddApiDependenciesWithFirebase(this IServiceCollection services, string animeListClientId,
            string emailAppPassword, string projectId, string serviceAccount)
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

            services.AddPostFirebaseService(projectId, serviceAccount);
            return services;
        }

        public static IServiceCollection AddAzureFunctionsDependenciesWithCosmos(this IServiceCollection services, string animeListClientId,
          string emailAppPassword, string cosmosEndPoint, string cosmosAccessKey, string tokenKey)
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
            services.AddScopedAuthService(options =>
            {
                options.TokenKey = tokenKey;
            }, cosmosEndPoint, cosmosAccessKey);

            services.AddPostCosmosService(cosmosEndPoint, cosmosAccessKey);
           
            return services;
        }
    }
}
