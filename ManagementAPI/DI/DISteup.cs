﻿using FluentValidation;
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
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("http://localhost:5173");
                });
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<ICustomerService, CustomerService>();
        return services;
    }

}