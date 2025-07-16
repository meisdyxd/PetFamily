using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PetFamily.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Database");
        services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.UseNpgsql(connection);
            o.UseLoggerFactory(CreateILoggerFactory());
        });

        return services;
    }

    private static ILoggerFactory CreateILoggerFactory() =>
        LoggerFactory.Create(b => { b.AddConsole(); });
}
