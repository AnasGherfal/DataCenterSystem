namespace ManagementAPI.DI;

public static class SwaggerExtension
{
    public static void AddSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
    }
    
    
    public static void UseSwagger(this IApplicationBuilder app, bool isDevelopment)
    {
        if (!isDevelopment) return;
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}