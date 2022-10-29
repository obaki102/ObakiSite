using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ObakiSite.Client;
using MudBlazor.Services;
using ObakiSite.Client.Services.Animelist;
using ObakiSite.Client.Services.AnimeList;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<IAnimeListService, AnimeListService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
