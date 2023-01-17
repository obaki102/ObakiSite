using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ObakiSite.Client;
using MudBlazor.Services;
using ObakiSite.Client.Services.Components.Badge;
using ObakiSite.Application.Extensions;
using MudBlazor;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObakiSite.Application.Shared.Constants;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var baseUrl = new Uri(builder.Configuration["WEBAPI_Prefix"] ?? DefaultConstants.WebApiHost );

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.TryAddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAppDependencies(baseUrl);
builder.Services.TryAddScoped<IBadgeUpdater, BadgeUpdater>();
builder.Services.AddChatHubClient(options =>
{
    options.HubUrl = builder.Configuration["API_Prefix"] is null ? $"{builder.HostEnvironment.BaseAddress}api" : $"{builder.Configuration["API_Prefix"]}/api";
});

if (builder.HostEnvironment.Environment == "Development")
{
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
}
else
{
    builder.Logging.SetMinimumLevel(LogLevel.None);
}

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomStart;
    config.SnackbarConfiguration.VisibleStateDuration = 2;
});
await builder.Build().RunAsync();
