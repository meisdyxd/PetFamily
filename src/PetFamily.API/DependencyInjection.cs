using PetFamily.Application;
using PetFamily.Application.VolunteerModule;
using PetFamily.Application.VolunteerModule.UseCases;
using PetFamily.Infrastructure.Persistence;
using PetFamily.Infrastructure.Persistence.VolunteerModule;

namespace PetFamily.API;

public static class DependencyInjection
{
    public static IServiceCollection AddLayers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddApplication();

        return services;
    }

    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();

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
