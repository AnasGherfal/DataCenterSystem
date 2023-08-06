using FluentValidation;
using Shared.Validators;

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