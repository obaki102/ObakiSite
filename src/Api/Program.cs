using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ObakiSite.Application.Extensions;
using ObakiSite.Application.Features.Animelist.Constants;
using ObakiSite.Application.Features.Email.Constants;
using ObakiSite.Application.Infra.Data.Firebase;

namespace ObakiSite.Api
{
    public class Program
    {
        static async Task Main()
        {
            //todo: Explore azure vault.
            var fireBaseVar = FirebaseSettings.GetFireBaseSettings();
            var firebaseSettings = JsonSerializer.Serialize(fireBaseVar);
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddApiDependenciesWithFirebase(Environment.GetEnvironmentVariable(AnimelistConstants.AnimelistClientId),
                        Environment.GetEnvironmentVariable(EmailConstants.AppPassword),
                        fireBaseVar.ProjectId,firebaseSettings);
                })

                .Build();
            await host.RunAsync();
        }
    }
}

