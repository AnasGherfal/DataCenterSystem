using ManagementAPI.Services;
using Microsoft.Extensions.Configuration;

namespace ManagementAPI.DI;

public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton(config);
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IRepresentativeService, RepresentativeService>();
        services.AddScoped<ICustomerFileService, CustomerFileService>();
        services.AddScoped<IVisitService, VisitService>();
        services.AddScoped<IVisitTimeShiftService, VisitTimeShiftService>();
        services.AddScoped<IFileUploadService, FileUploadService>();
        services.AddScoped<ServiceServices>();
        services.AddScoped<SubscriptionService>();
        services.AddScoped<UserService>();


    }
}