namespace ManagementAPI.DI;

public static class CorsExtension
{
    public static void AddCustomCors(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("http://localhost:5173");
                });
        });
    }
}