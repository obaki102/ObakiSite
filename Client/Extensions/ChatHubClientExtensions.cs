using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Client.Services.ChatHubClient;

namespace ObakiSite.Client.Extensions
{
    public static class ChatHubClientExtensions
    {
        public static IServiceCollection AddTransientChatHubClient(this IServiceCollection services, Action<ChatHubClientOptions> options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddTransient<IChatHubClient, ChatHubClient>();
            services.Configure(options);
            return services;
        }

        public static IServiceCollection AddScopedChatHubClient(this IServiceCollection services, Action<ChatHubClientOptions> options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.Configure(options);
            services.TryAddScoped<IChatHubClient, ChatHubClient>();

            return services;
        }

        public static IServiceCollection AddSingletonChatHubClient(this IServiceCollection services, Action<ChatHubClientOptions> options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<IChatHubClient, ChatHubClient>();
            services.Configure(options);
            return services;
        }
        public static IServiceCollection AddSingletonChatHubClient(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<IChatHubClient, ChatHubClient>();
            return services;
        }
    }
}
