using ObakiSite.Application.Extensions;
using ObakiSite.Application.Features.Email.Constants;
using ObakiSite.Application.Infra.Data.Firebase;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var firebaseSettingsVar = FirebaseSettings.GetFireBaseSettings(builder.Configuration);
var firebaseSettings = JsonSerializer.Serialize(firebaseSettingsVar);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiDependenciesWithFirebase(builder.Configuration.GetSection("AnimelistClientId").Value,
                       builder.Configuration.GetSection(EmailConstants.AppPassword).Value,
                        firebaseSettingsVar.ProjectId, firebaseSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
