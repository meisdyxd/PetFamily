using Microsoft.EntityFrameworkCore;
using PetFamily.API.Middlewares;
using PetFamily.Application;
using PetFamily.Application.SpeciesModule;
using PetFamily.Application.VolunteerModule;
using PetFamily.Infrastructure;
using PetFamily.Infrastructure.BackgroundServices;
using PetFamily.Infrastructure.Minio;
using PetFamily.Infrastructure.Persistence;
using PetFamily.Infrastructure.Persistence.Contexts;
using PetFamily.Infrastructure.Persistence.SpeciesModule;
using PetFamily.Infrastructure.Persistence.VolunteerModule;
using Serilog;

namespace PetFamily.API;

public static class DependencyInjection
{
    public static IServiceCollection AddLayers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddApplication();
        services.AddBackgroundServices(configuration);
        services.AddMinio(configuration);
        services.AddInfrastructure();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();

        return services;
    }

    private static WebApplication UseConfigureSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }

    public static async Task ApplyMigrations(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }

    public static WebApplication ConfigureMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseConfigureSwagger();

        app.UseHttpsRedirection();

        app.UseSerilogRequestLogging();

        app.UseAuthorization();

        return app;
    }
}
