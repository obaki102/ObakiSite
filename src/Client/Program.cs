using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ObakiSite.Client;
using MudBlazor.Services;
using ObakiSite.Client.Services.Components.Badge;
using ObakiSite.Application.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var baseUrl = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAppDependencies(baseUrl);
builder.Services.AddScoped<IBadgeUpdater, BadgeUpdater>();
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

builder.Services.AddMudServices();
await builder.Build().RunAsync();
