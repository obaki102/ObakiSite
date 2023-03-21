using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Features.ChatGPT.Services;
using OpenAI.GPT3.Extensions;

namespace ObakiSite.Application.Shared.Extensions
{
    public static class ChatGptDependencies
    {
        public static IServiceCollection AddChatGptService(this IServiceCollection services, string ApiKey)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddOpenAIService(settings => { settings.ApiKey = ApiKey; });
            services.TryAddScoped<IChatGPTService, ChatGPTService>();
            return services;
        }
    }
}
