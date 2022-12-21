using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ObakiSite.Application.Extensions;
using ObakiSite.Application.Features.Animelist.Constants;
using ObakiSite.Application.Features.Email.Constants;
using ObakiSite.Shared.Constants;
namespace ObakiSite.Api
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddApiDependencies(Environment.GetEnvironmentVariable(AnimelistConstants.AnimelistClientId),
                        Environment.GetEnvironmentVariable(EmailConstants.AppPassword),
                        Environment.GetEnvironmentVariable(CosmosDBConstants.EndPoint),
                        Environment.GetEnvironmentVariable(CosmosDBConstants.AccessKey));
                })

                .Build();
            await host.RunAsync();
        }
    }
}

