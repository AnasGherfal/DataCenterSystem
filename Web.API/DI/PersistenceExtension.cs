using Core.Options;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Web.API.DI;

public static class PersistenceExtension
{
    public static void AddPersistence(this IServiceCollection services, IConfigurationSection section)
    {
        var connectionString = section.GetValue<string>(nameof(PersistenceOption.ConnectionString));
        services.AddDbContext<AppDbContext>(o =>
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
        await using var dbConte = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (dbConte.Database.IsSqlServer())
        {
            await dbConte.Database.MigrateAsync();
        }
        dbConte.InitAdmin();
    }
}