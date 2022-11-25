﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ObakiSite.Shared.Constants;
using Polly;
using Polly.Contrib.WaitAndRetry;
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

            })
            .AddTransientHttpErrorPolicy(policyBuilder =>
             policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 3), (exception, timeSpan, retryCount, context) =>
             {
                // todo: log exception and retries.
             }));;

            services.AddLocalStorageCahce();
            return services;
        }
    }
}
