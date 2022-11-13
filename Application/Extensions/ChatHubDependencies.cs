using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Features.Chat.Services;

namespace ObakiSite.Application.Extensions
{
    public static class ChatHubDependencies
    {
        public static IServiceCollection AddScopedChatHubClient(this IServiceCollection services, Action<ChatHubClientOptions> options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.TryAddScoped<IChatHubClient, ChatHubClient>();
            services.Configure(options);
            return services;
        }
    }
}
