using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ObakiSite.Client;
using MudBlazor.Services;
using ObakiSite.Client.Services.Animelist;
using ObakiSite.Client.Services.AnimeList;
using ObakiSite.Client.Extensions;
using ObakiSite.Shared.Constants;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<IAnimeListService, AnimeListService>(options =>
{
     options.BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress);
    
});

builder.Services.AddScopedChatHubClient(options =>
{
    options.HubUrl = builder.Configuration.GetSection(SignalR.AzureFunctionHubUrl).Value;
});

builder.Services.AddMudServices();
await builder.Build().RunAsync();
