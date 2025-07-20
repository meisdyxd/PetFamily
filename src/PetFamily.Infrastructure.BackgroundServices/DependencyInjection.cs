using Microsoft.Extensions.DependencyInjection;
using PetFamily.Infrastructure.BackgroundServices.DeleteExpiredEntities;

namespace PetFamily.Infrastructure.BackgroundServices;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<DeleteExpiredEntitiesService>();
        
        return services;
    }
}
