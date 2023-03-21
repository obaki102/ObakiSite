using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Hosting;
using ObakiSite.Application.Features.Animelist.Constants;
using ObakiSite.Application.Features.ChatGPT.Constants;
using ObakiSite.Application.Features.Email.Constants;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.Extensions;

namespace ObakiSite.Api
{
    public class Program
    {
        static async Task Main()
        {
            //todo: Explore azure vault.
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureOpenApi()
                .ConfigureServices(services =>
                {
                    services.AddAzureFunctionsDependenciesWithCosmos(Environment.GetEnvironmentVariable(AnimelistConstants.AnimelistClientId),
                        Environment.GetEnvironmentVariable(EmailConstants.AppPassword),
                         Environment.GetEnvironmentVariable(CosmosDBConstants.EndPoint),
                         Environment.GetEnvironmentVariable(CosmosDBConstants.AccessKey),
                         Environment.GetEnvironmentVariable(DefaultConstants.TokenKey));

                    services.AddChatGptService(Environment.GetEnvironmentVariable(ChatGptConstants.ApiKey));
                })

                .Build();
            await host.RunAsync();
        }
    }
}

