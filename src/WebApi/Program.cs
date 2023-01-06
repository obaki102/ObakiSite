using ObakiSite.Application.Extensions;
using ObakiSite.Application.Features.Email.Constants;
using ObakiSite.Application.Infra.Data.Firebase;
using Polly;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var firebaseSettingsVar = FirebaseSettings.GetFireBaseSettings(builder.Configuration);
var firebaseSettings = JsonSerializer.Serialize(firebaseSettingsVar);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://localhost:5011", "https://www.joshuajpiluden.site", "https://victorious-sea-07588b900.2.azurestaticapps.net")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiDependenciesWithFirebase(builder.Configuration.GetSection("AnimelistClientId").Value??string.Empty,
                       builder.Configuration.GetSection(EmailConstants.AppPassword).Value??string.Empty,
                        firebaseSettingsVar.ProjectId, firebaseSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseForwardedHeaders();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors();
app.Run();
