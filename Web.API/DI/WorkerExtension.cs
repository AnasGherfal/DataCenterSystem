using Web.API.Workers;

namespace Web.API.DI;

public static class WorkerExtension
{
    public static void AddWorker(this IServiceCollection services)
    {
        services.AddHostedService<NotifyWorker>();
    }
}