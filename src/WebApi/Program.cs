using ObakiSite.Application.Features.Email.Constants;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);
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
builder.Services.AddApiDependenciesWithCosmos(builder.Configuration.GetSection("AnimelistClientId").Value ?? string.Empty,
                       builder.Configuration.GetSection(EmailConstants.AppPassword).Value ?? string.Empty,
                       builder.Configuration.GetSection(CosmosDBConstants.EndPoint).Value ?? string.Empty,
                       builder.Configuration.GetSection(CosmosDBConstants.AccessKey).Value ?? string.Empty
                       );
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
