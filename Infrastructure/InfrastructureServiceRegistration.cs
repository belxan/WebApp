using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Config;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, DatabaseOptionSettings settings)
    {
        services.AddDbContext<AppDbContext>(dbContextOptionBuilder =>
        {

            dbContextOptionBuilder.UseNpgsql(settings.ConnectionStrings.AppDb, action =>
            {
                action.EnableRetryOnFailure(settings.MaxRetryCount);

                action.CommandTimeout(settings.CommandTimeout);
            });

            dbContextOptionBuilder.EnableDetailedErrors(settings.EnableDetailedErrors);
        });

        // Register Unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
