using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ManagementAPI.DI;

public static class PersistenceExtension
{
    public static void AddPersistence(this IServiceCollection serviceCollection, bool useInMemoryDb, string connectionString)
    {
        // serviceCollection.AddDbContext<DataCenterContext>(c =>
        // {
        //     if (useInMemoryDb)
        //     {
        //         c.UseInMemoryDatabase("InMemory_Store");   
        //     }
        //     else
        //     {
        //         c.UseSqlServer(connectionString);
        //     }
        // });
    }
    
    
    public static async void UsePersistence(this WebApplication app, bool useInMemoryDb)
    {
        // if (useInMemoryDb) return;
        // using var scope = app.Services.CreateScope();
        // await using var dbContext = scope.ServiceProvider.GetRequiredService<DataCenterContext>();
        // await dbContext.Database.MigrateAsync();
    }
}