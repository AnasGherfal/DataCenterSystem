using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.InvoiceManagement.CreateInvoice;

public class CreateInvoiceCommandValidator: AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        RuleFor(x=>x.CustomerId)    
            .IsGuid();
        RuleFor(x => x.IncludeVisitsFrom)
            .NotNull();
        RuleFor(x => x.IncludeVisitsTo)
            .NotNull()
            .GreaterThan(x => x.IncludeVisitsFrom);
    }
}