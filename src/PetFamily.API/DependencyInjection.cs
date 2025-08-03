using Microsoft.EntityFrameworkCore;
using PetFamily.API.Middlewares;
using PetFamily.Application;
using PetFamily.Application.SpeciesModule;
using PetFamily.Application.VolunteerModule;
using PetFamily.Application.VolunteerModule.UseCases.AddPetToVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.AddPhotosToPet;
using PetFamily.Application.VolunteerModule.UseCases.CreateVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.DeletePhotosPet;
using PetFamily.Application.VolunteerModule.UseCases.ForceDeleteVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.MovePetPosition;
using PetFamily.Application.VolunteerModule.UseCases.RestoreVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.SoftDeleteVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.UpdateMainInfoVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.UpdateRequisitsVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.UpdateSocialNetworksVolunteer;
using PetFamily.Infrastructure.BackgroundServices;
using PetFamily.Infrastructure.Minio;
using PetFamily.Infrastructure.Persistence;
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

        return services;
    }

    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateMainInfoHandler>();
        services.AddScoped<UpdateRequisitsHandler>();
        services.AddScoped<UpdateSocialNetworksHandler>();
        services.AddScoped<SoftDeleteHandler>();
        services.AddScoped<ForceDeleteHandler>();
        services.AddScoped<RestoreHandler>();
        services.AddScoped<AddPetHandler>();
        services.AddScoped<AddPhotosToPetHandler>();
        services.AddScoped<DeletePhotosPetHandler>();
        services.AddScoped<MovePetPositionHandler>();

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

    public static WebApplication UseConfigureSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }

    public async static Task ApplyMigrations(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

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
