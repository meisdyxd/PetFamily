using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFamily.Application.Channels;
using PetFamily.Application.Channels.Models;
using PetFamily.Application.Minio;
using PetFamily.Infrastructure.BackgroundServices.Options;

namespace PetFamily.Infrastructure.BackgroundServices.DeleteTrashMinio;
public class DeleteTrashMinioService : BackgroundService
{
    private readonly ILogger<DeleteTrashMinioService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly DeleteTrashMinioOptions _options;

    public DeleteTrashMinioService(
        ILogger<DeleteTrashMinioService> logger,
        IServiceProvider serviceProvider,
        IOptionsMonitor<DeleteTrashMinioOptions> options)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _options = options.CurrentValue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var filesProvider = scope.ServiceProvider.GetRequiredService<IFilesProvider>();
            var channel = scope.ServiceProvider.GetRequiredService<IMessageQueue<IEnumerable<FileMetadata>>>();
            _logger.LogInformation("Очистка мусора с хранилища Minio");
            try
            {
                var fileMetadatas = await channel.ReadAsync(stoppingToken);
                foreach (var fileMetadata in fileMetadatas)
                    await filesProvider.DeleteFileAsync(fileMetadata.Bucket, fileMetadata.Filename, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Остановка работы фонового процесса");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка работы фонового процесса по удалению мусора Minio. Message: {message}", ex.Message);
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                continue;
            }
            
            await Task.Delay(TimeSpan.FromHours(_options.RepeatTimeHours), stoppingToken);
        }
    }
}
