using FluentValidation;
using FluentValidation.Validators;
namespace Shared.Validators;
public class GuidValidator<T> : PropertyValidator<T, string?> {
    public override bool IsValid(ValidationContext<T> context, string? flags) 
    {
        try
        {
            var result = Guid.Parse(flags ?? string.Empty);
            return result != Guid.Empty;
        }
        catch
        {
            return false;
        }
    }
    public override string Name => "Id";
    protected override string GetDefaultMessageTemplate(string errorCode) => "{PropertyName} must be Id.";
}

public static class GuidValidatorExtension {
    public static IRuleBuilderOptions<T, string?> IsGuid<T>(this IRuleBuilder<T, string?> ruleBuilder) 
        => ruleBuilder.SetValidator(new GuidValidator<T>());
}