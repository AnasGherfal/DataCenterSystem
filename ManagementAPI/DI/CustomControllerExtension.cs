using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shared.Filters;

namespace ManagementAPI.DI;

public static class CustomControllerExtension
{
    public static IServiceCollection AddCustomControllers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllers(o =>
        {
            o.Filters.Add(new ValidateModelStateFilter());
        });
        serviceCollection.AddValidatorsFromAssemblyContaining<Program>();
        serviceCollection.AddFluentValidationAutoValidation(options =>
        {
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
            options.DisableDataAnnotationsValidation = true;
        });
        serviceCollection.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return serviceCollection;
    }
}