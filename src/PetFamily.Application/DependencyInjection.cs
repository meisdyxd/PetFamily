using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Interfaces;

namespace PetFamily.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddCommandsHandler()
            .AddQueriesHandler()
            .ConfigureServices();

        return services;
    }

    private static IServiceCollection AddCommandsHandler(this IServiceCollection services)
    {
        services.Scan(scan
            => scan
                .FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(classes 
                    => classes.AssignableToAny(typeof(ICommandHandler<>), typeof(ICommandHandler<,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

        return services;
    }
    
    private static IServiceCollection AddQueriesHandler(this IServiceCollection services)
    {
        services.Scan(scan
            => scan
                .FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(classes 
                    => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
