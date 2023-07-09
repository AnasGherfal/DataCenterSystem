using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Web.API.DI;

public static class SwaggerExtension
{
    public const string ConsumerV1 = "consumer-v1";
    public const string ManagementV1 = "administrator-v1";

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        var info = new OpenApiInfo()
        {
            Title = "",
            Version = "",
            Description = "An API to perform operations",
        };
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(ConsumerV1, new OpenApiInfo()
            {
                Title = "Consumer API",
                Version = "v1.0",
            });
            c.SwaggerDoc(ManagementV1, new OpenApiInfo()
            {
                Title = "Management API",
                Version = "v1.0",
            });
            c.CustomSchemaIds(x =>
            {
                var value = "";
                value += $"{x.Namespace}.{x.Name}";
                while (x.GenericTypeArguments.Length > 0)
                {
                    x = x.GenericTypeArguments[0];
                    value += $"{x.Namespace}.{x.Name}";
                }
                return value;
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void UseSwagger(this IApplicationBuilder app, bool isDevelopment)
    {
        if (!isDevelopment) return;
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/{ManagementV1}/swagger.json", "Management API [V1]");
            c.SwaggerEndpoint($"/swagger/{ConsumerV1}/swagger.json", "Consumer API [V1]");
        });
    }
}