using ManagementAPI.Services;

namespace ManagementAPI.DI;

public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ServiceServices>();
    }
}