using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.ServiceManagement.UpdateService;

public class UpdateServiceCommandValidator: AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
        RuleFor(a => a.Name)
            .NotEmpty();
        RuleFor(a => a.AmountOfPower)
            .NotEmpty();
        RuleFor(a => a.AcpPort)
            .NotEmpty();
        RuleFor(a => a.Dns)
            .NotEmpty();
        RuleFor(a => a.MonthlyVisits)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(a => a.Price)
            .NotEmpty()
            .GreaterThan(0);

    }
}