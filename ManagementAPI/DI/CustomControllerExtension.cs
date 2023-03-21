using FluentValidation;
using FluentValidation.AspNetCore;
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
        return serviceCollection;
    }
}