using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetFamily.Infrastructure.Persistence;

namespace PetFamily.Infrastructure.BackgroundServices.DeleteExpiredEntities;
public class DeleteExpiredEntitiesService : BackgroundService
{
    private readonly ILogger<DeleteExpiredEntitiesService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly int _lifetimeDays;
    private const int RepeatTimeHours = 24;

    public DeleteExpiredEntitiesService(
        ILogger<DeleteExpiredEntitiesService> logger,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        var options = configuration.GetSection(DeleteExpiredEntitiesOptons.SectionName);
        _lifetimeDays = options.GetValue<int>("LifeTimeDays");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var expiredDate = DateTime.UtcNow - TimeSpan.FromDays(_lifetimeDays);
            _logger.LogInformation("Очистка волонтёров с истекшей датой существования");

            await using var scope = _serviceProvider.CreateAsyncScope();
            var _dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await _dbContext.Volunteers
                .Where(v => v.IsDeleted && v.DeletionDate <= expiredDate)
                .ExecuteDeleteAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromHours(RepeatTimeHours), stoppingToken);
        }
    }
}
