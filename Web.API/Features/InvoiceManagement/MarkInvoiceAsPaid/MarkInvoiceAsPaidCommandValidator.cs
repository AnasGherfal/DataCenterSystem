using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.InvoiceManagement.MarkInvoiceAsPaid;

public class MarkInvoiceAsPaidCommandValidator: AbstractValidator<MarkInvoiceAsPaidCommand>
{
    public MarkInvoiceAsPaidCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
        
        RuleFor(c => c.AdminPassword)
            .NotEmpty();
    }
}