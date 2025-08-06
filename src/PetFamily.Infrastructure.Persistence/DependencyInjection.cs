using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Interfaces;
using PetFamily.Infrastructure.Persistence.Contexts;

namespace PetFamily.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddWriteDbContext(configuration)
            .AddReadDbContext(configuration);
        

        return services;
    }

    private static IServiceCollection AddWriteDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Database");
        services.AddDbContext<WriteDbContext>(o =>
        {
            o.UseNpgsql(connection);
            o.UseLoggerFactory(CreateILoggerFactory());
        });

        return services;
    }
    
    private static IServiceCollection AddReadDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Database");
        services.AddDbContext<ReadDbContext>(o =>
        {
            o.UseNpgsql(connection);
            o.UseLoggerFactory(CreateILoggerFactory());
            o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }

    private static ILoggerFactory CreateILoggerFactory() =>
        LoggerFactory.Create(b =>
        {
            b.SetMinimumLevel(LogLevel.Warning);
            b.AddConsole(); 
        });
}
