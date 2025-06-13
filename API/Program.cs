using Application;
using Application.Common.Config;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
var builder = WebApplication.CreateBuilder(args);
var config = new ConfigSettings();
builder.Configuration.GetSection(nameof(ConfigSettings)).Bind(config);
builder.Services.TryAddSingleton(config);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(config.DatabaseOptionSettings)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddMiniProfiler();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
