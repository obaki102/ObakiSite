using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using ObakiSite.Application.Extensions;
using ObakiSite.Shared.Constants;
using System;

[assembly: FunctionsStartup(typeof(ObakiSite.Api.Program))]

namespace ObakiSite.Api
{
    public class Program : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingletonAnimeListService(options =>
            {
                options.BaseAddress = new Uri("https://api.myanimelist.net/");
                options.DefaultRequestHeader =  Environment.GetEnvironmentVariable(AnimeList.AnimelistClientId);
            });
            
            
        }
    }
}
