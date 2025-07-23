using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFamily.Infrastructure.Persistence;

namespace PetFamily.Infrastructure.BackgroundServices.DeleteExpiredEntities;
public class DeleteExpiredEntitiesService : BackgroundService
{
    private readonly ILogger<DeleteExpiredEntitiesService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly DeleteExpiredEntitiesOptons _options;

    public DeleteExpiredEntitiesService(
        ILogger<DeleteExpiredEntitiesService> logger,
        IServiceProvider serviceProvider,
        IOptionsMonitor<DeleteExpiredEntitiesOptons> options)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _options = options.CurrentValue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var expiredDate = DateTime.UtcNow - TimeSpan.FromDays(_options.LifeTimeDays);
            _logger.LogInformation("Очистка волонтёров с истекшей датой существования");

            await using var scope = _serviceProvider.CreateAsyncScope();
            var _dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await _dbContext.Volunteers
                .Where(v => v.IsDeleted && v.DeletionDate <= expiredDate)
                .ExecuteDeleteAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromHours(_options.RepeatTimeHours), stoppingToken);
        }
    }
}
