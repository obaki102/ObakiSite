using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ObakiSite.Client;
using MudBlazor.Services;
using ObakiSite.Client.Services.Animelist;
using ObakiSite.Client.Services.AnimeList;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<IAnimeListService, AnimeListService>(options =>
{
     options.BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress);
    //options.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);

});

builder.Services.AddMudServices();
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//        policy =>
//        {
//            policy.AllowAnyOrigin();  //set the allowed origin
//        });
//});
await builder.Build().RunAsync();
