using FluentValidation;

namespace Web.API.Features.ServiceManagement.CreateService;

public class CreateServiceCommandValidator: AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
        RuleFor(x=>x.AmountOfPower)    
            .NotEmpty();
        RuleFor(x => x.AcpPort)
            .NotEmpty();
        RuleFor(x => x.Dns)
            .NotEmpty();
        RuleFor(x => x.MonthlyVisits)
            .NotNull()
            .GreaterThan(0); ;
        RuleFor(x => x.Price)
            .NotNull()
            .GreaterThan(0);
    }
}