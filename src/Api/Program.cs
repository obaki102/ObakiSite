using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ObakiSite.Application.Features.Animelist.Constants;
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
                .ConfigureServices(services =>
                {
                    services.AddAzureFunctionsDependenciesWithCosmos(Environment.GetEnvironmentVariable(AnimelistConstants.AnimelistClientId),
                        Environment.GetEnvironmentVariable(EmailConstants.AppPassword),
                         Environment.GetEnvironmentVariable(CosmosDBConstants.EndPoint),
                         Environment.GetEnvironmentVariable(CosmosDBConstants.AccessKey),
                         Environment.GetEnvironmentVariable(DefaultConstants.TokenKey));
                })

                .Build();
            await host.RunAsync();
        }
    }
}

