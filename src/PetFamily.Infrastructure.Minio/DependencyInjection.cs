using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Infrastructure.Minio.Options;
using Minio;
using PetFamily.Infrastructure.Minio.Provider;
using PetFamily.Application.Minio;

namespace PetFamily.Infrastructure.Minio;

public static class DependencyInjection
{
    public static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
    {
        var config = configuration.GetSection(MinioOptions.SectionName);
        var options = config.Get<MinioOptions>();
        services.AddMinioOptions(config);
        services.AddMinio(configureClient => configureClient
            .WithEndpoint(options!.Endpoint)
            .WithCredentials(options.AccessToken, options.SecretKey)
            .WithSSL(options.SSL)
            .Build());

        services.AddScoped<IFilesProvider, MinioProvider>();

        return services;
    }

    private static IServiceCollection AddMinioOptions(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<MinioOptions>()
            .Bind(config)
            .ValidateDataAnnotations();

        return services;
    }
}
