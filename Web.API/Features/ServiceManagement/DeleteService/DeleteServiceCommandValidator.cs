using Core.Validators;
using FluentValidation;

namespace Web.API.Features.ServiceManagement.DeleteService;

public class DeleteServiceCommandValidator: AbstractValidator<DeleteServiceCommand>
{
    public DeleteServiceCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}