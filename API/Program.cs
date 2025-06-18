using API;
using Application;
using Application.Common.Config;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
var builder = WebApplication.CreateBuilder(args);
var config = new ConfigSettings();
builder.Configuration.GetSection(nameof(ConfigSettings)).Bind(config);
builder.Services.TryAddSingleton(config);

builder.Services
    .AddApplicationServices(config.AuthSettings.SecretKey)
    .AddInfrastructureServices(config.DatabaseOptionSettings)
    .AddEndpointsApiExplorer()
    .AddMiniProfiler();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//register swagger
if (config.SwaggerSettings.IsEnabled)
{
    builder.Services.AddSwagger(config);
}

var app = builder.Build();
app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (config.SwaggerSettings.IsEnabled)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.EnablePersistAuthorization();
        c.InjectStylesheet(config.SwaggerSettings.Theme);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
