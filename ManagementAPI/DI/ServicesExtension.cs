using ManagementAPI.Services;

namespace ManagementAPI.DI;

public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IRepresentiveService, RepresentiveService>();
        services.AddScoped<ICustomerFileService, CustomerFileService>();
        services.AddScoped<IVisitService, VisitService>();
        services.AddScoped<IVisitTimeShiftService, VisitTimeShiftService>();
        services.AddScoped<ICompanionService, CompanionService>();
        services.AddScoped<ServiceServices>();
    }
}