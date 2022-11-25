using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Features.Chat.Services;

namespace ObakiSite.Application.Extensions
{
    public static class ChatHubDependencies
    {
        public static IServiceCollection AddChatHubClient(this IServiceCollection services, Action<ChatHubClientOptions> options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            //In blazor wasm scoped lifetime behaves the same way as singleton.  
            services.TryAddSingleton<IChatHubClient, ChatHubClient>();
            services.Configure(options);
            return services;
        }
    }
}
