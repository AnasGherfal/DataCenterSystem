using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ManagementAPI.DI;

public static class SwaggerExtension
{
    public static void AddSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(c => c.MapType<TimeSpan>(() => new OpenApiSchema
        {
            Type = "string",
            Example = new OpenApiString("00:00:00")
        }));
        
        
    }
    
    
    public static void UseSwagger(this IApplicationBuilder app, bool isDevelopment)
    {
        if (!isDevelopment) return;
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}