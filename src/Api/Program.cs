using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ObakiSite.Application.Extensions;
using ObakiSite.Application.Features.Animelist.Constants;
using ObakiSite.Application.Features.Email.Constants;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;

namespace ObakiSite.Api
{
    public class Program
    {
        static async Task Main()
        {
            //todo: Explore azure vault.
            var fireBaseVar = new FirebaseSettings
            {
                Type = Environment.GetEnvironmentVariable(FirebaseConstants.GoogleServiceAccount.ServiceAccount),
                AuthProvider = Environment.GetEnvironmentVariable(FirebaseConstants.GoogleServiceAccount.AuthProvider),
                AuthUri = Environment.GetEnvironmentVariable(FirebaseConstants.GoogleServiceAccount.AuthUri),
                ClientCertUrl = Environment.GetEnvironmentVariable(FirebaseConstants.GoogleServiceAccount.ClientCertUrl),
                ClientEmail = Environment.GetEnvironmentVariable(FirebaseConstants.GoogleServiceAccount.ClientEmail),
                ClientId = Environment.GetEnvironmentVariable(FirebaseConstants.GoogleServiceAccount.ClientId),
                PrivateKey = Environment.GetEnvironmentVariable(FirebaseConstants.GoogleServiceAccount.PrivateKey),
                PrivateKeyId = Environment.GetEnvironmentVariable(FirebaseConstants.GoogleServiceAccount.PrivateKeyId),
                ProjectId = Environment.GetEnvironmentVariable(FirebaseConstants.GoogleServiceAccount.ProjectId),
                TokenUri = Environment.GetEnvironmentVariable(FirebaseConstants.GoogleServiceAccount.TokenUri)
            };
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

