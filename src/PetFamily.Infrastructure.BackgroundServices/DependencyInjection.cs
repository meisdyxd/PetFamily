using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Infrastructure.BackgroundServices.DeleteExpiredEntities;
using PetFamily.Infrastructure.BackgroundServices.DeleteTrashMinio;
using PetFamily.Infrastructure.BackgroundServices.Options;

namespace PetFamily.Infrastructure.BackgroundServices;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDeleteExpiredEntitiesService(configuration)
            .AddDeleteTrashMinioService();
        
        return services;
    }

    private static IServiceCollection AddDeleteExpiredEntitiesService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDeleteExpiredEntitiesOptions(configuration)
            .AddHostedService<DeleteExpiredEntitiesService>();

        return services;
    }

    private static IServiceCollection AddDeleteExpiredEntitiesOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DeleteExpiredEntitiesOptons>()
            .Bind(configuration.GetSection(DeleteExpiredEntitiesOptons.SectionName))
            .ValidateDataAnnotations();

        return services;
    }

    private static IServiceCollection AddDeleteTrashMinioService(this IServiceCollection services)
    {
        services.AddHostedService<DeleteTrashMinioService>();

        return services;
    }
}
