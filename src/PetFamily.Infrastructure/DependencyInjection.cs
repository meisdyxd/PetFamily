using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Channels;
using PetFamily.Application.Channels.Models;
using PetFamily.Infrastructure.Channels;

namespace PetFamily.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddChannels();

        return services;
    }

    public static IServiceCollection AddChannels(this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueue<IEnumerable<FileMetadata>>, InMemoryMessageQueue<IEnumerable<FileMetadata>>>();

        return services;
    }
}
