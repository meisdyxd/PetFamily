﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Infrastructure.BackgroundServices.DeleteExpiredEntities;

namespace PetFamily.Infrastructure.BackgroundServices;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDeleteExpiredEntitiesService(configuration);
        
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
}
