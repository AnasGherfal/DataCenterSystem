using FluentValidation;
using Infrastructure;
using ManagementAPI.Services;
using Microsoft.EntityFrameworkCore;


namespace ManagementAPI.DI;

public static class DISetup
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddCustomControllers();
        services.AddSwagger();
        services.AddDbContext<DataCenterContext>(c => c.UseInMemoryDatabase(databaseName:"DataCenter"));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<ICustomerService, CustomerService>();
        return services;
    }

}