using PetFamily.Infrastructure.Persistence;

namespace PetFamily.API;

public static class DependencyInjection
{
    public static IServiceCollection AddLayers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();

        return services;
    }

    public static IApplicationBuilder UseConfigureSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
