using MediatR;
using NCrontab;
using Web.API.Features.BackgroundJobs.AlertToBeExpiredSubscriptions;

namespace Web.API.Workers;

public class NotifyWorker : BackgroundService
{
    private static string Schedule => "0 0 0 * * *"; // every day at midnight
    private readonly DateTime _nextRun;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<NotifyWorker> _logger;

    public NotifyWorker(IServiceScopeFactory scopeFactory, ILogger<NotifyWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        var schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions
        {
            IncludingSeconds = true
        });
        _nextRun = schedule.GetNextOccurrence(DateTime.UtcNow);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        do
        {
            var now = DateTime.UtcNow;
            if (now > _nextRun)
            {
                _logger.LogInformation("Notify Worker Service started");
                using var scope = _scopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new AlertToBeExpiredSubscriptionsCommand(), stoppingToken);
            }
            await Task.Delay(5000, stoppingToken);
        }
        while (!stoppingToken.IsCancellationRequested);
    }
}