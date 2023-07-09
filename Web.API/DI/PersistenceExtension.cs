using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Web.API.Options;

namespace Web.API.DI;

public static class PersistenceExtension
{
    public static void AddPersistence(this IServiceCollection services, IConfigurationSection section)
    {
        var connectionString = section.GetValue<string>(nameof(PersistenceOption.ConnectionString));
        services.AddDbContext<DataCenterContext>(o =>
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                o.UseInMemoryDatabase("IN_MEMORY_TEST");
                o.EnableDetailedErrors();
            }
            else
            {
                o.UseSqlServer(connectionString);
            }
        });
    }
    
    
    
    public static async void UsePersistence(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await using var commandDb = scope.ServiceProvider.GetRequiredService<DataCenterContext>();
        if (commandDb.Database.IsSqlServer())
        {
            await commandDb.Database.MigrateAsync();   
        }
    }
}