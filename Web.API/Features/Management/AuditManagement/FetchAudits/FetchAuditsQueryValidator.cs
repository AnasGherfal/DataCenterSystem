using Core.Validators;
using FluentValidation;

namespace Web.API.Features.AuditManagement.FetchAudits;

public class FetchAuditsQueryValidator: AbstractValidator<FetchAuditsQuery>
{
    public FetchAuditsQueryValidator()
    {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(p => p.PageSize)
                .GreaterThanOrEqualTo(5);
            When(p => !string.IsNullOrWhiteSpace(p.RecordId), () =>
            {
                RuleFor(p => p.RecordId)
                    .IsGuid();
            });
    }
}
