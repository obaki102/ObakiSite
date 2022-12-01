using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ObakiSite.Application.Extensions;
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
                    services.AddHttpAnimeListService(new Uri("https://api.myanimelist.net/"),
                Environment.GetEnvironmentVariable(AnimeList.AnimelistClientId));

                    services.AddEmailService(options =>
                    {
                        options.AppPassword = Environment.GetEnvironmentVariable(EmailConstants.AppPassword);
                    });
                })

                .Build();
            await host.RunAsync();
        }
    }
}

