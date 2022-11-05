using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ObakiSite.Api.Services.AnimeList;
using ObakiSite.Shared.Constants;
using System;

[assembly: FunctionsStartup(typeof(ObakiSite.Api.Program))]

namespace ObakiSite.Api
{
    public class Program : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient("AnimeList", options =>
            {
                options.BaseAddress = new Uri("https://api.myanimelist.net/");
                options.DefaultRequestHeaders.Add(AnimeList.XmalClientId, Environment.GetEnvironmentVariable(AnimeList.AnimelistClientId));
            });
            builder.Services.AddSingleton<IAnimeListService,AnimeListService>();
        }
    }
}
