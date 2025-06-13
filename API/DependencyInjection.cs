﻿namespace API;
using Application.Common.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StackExchange.Profiling;
using StackExchange.Profiling.SqlFormatters;
public static class DependencyInjection
{
    public static void AddMiniProfiler(this IServiceCollection services)
    {
        services.AddMiniProfiler(options =>
        {
            options.RouteBasePath = "/mini-profiler";
            options.ColorScheme = ColorScheme.Dark;
            options.SqlFormatter = new InlineFormatter();
        }).AddEntityFramework();
    }
    public static void AddSwagger(this IServiceCollection services, ConfigSettings configSettings)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(configSettings.SwaggerSettings.Version,
                new OpenApiInfo { Title = configSettings.SwaggerSettings.Title, Version = configSettings.SwaggerSettings.Version });

            c.AddSecurityDefinition(configSettings.AuthSettings.TokenPrefix, new OpenApiSecurityScheme
            {
                Name = configSettings.AuthSettings.HeaderName,
                Type = SecuritySchemeType.ApiKey,
                Scheme = configSettings.AuthSettings.TokenPrefix,
                BearerFormat = configSettings.AuthSettings.Type,
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = configSettings.AuthSettings.TokenPrefix
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}
