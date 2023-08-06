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
        RuleFor(a => a.Name).NotEmpty().WithMessage("name must be not null");
        RuleFor(a => a.AmountOfPower).NotEmpty().WithMessage("AmountOfPower must be not null");
        RuleFor(a => a.AcpPort).NotEmpty().WithMessage("AcpPort must be not null");
        RuleFor(a => a.Dns).NotEmpty().WithMessage("Dns must be not null");
        RuleFor(a => a.MonthlyVisits)
            .NotEmpty().WithMessage("monthlyvisit must be not null")
            .GreaterThan(0).WithMessage("must be number more than 0");

        RuleFor(a => a.Price)
            .NotEmpty().WithMessage("price must be not null")
            .GreaterThan(0).WithMessage("must be number more than 0");

    }
}