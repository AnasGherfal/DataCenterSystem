using FluentValidation;

namespace ManagementAPI.Dtos.Customer;

public record FetchCustomersRequestDto(string? CustomerName, int PageNumber = 1, int PageSize = 10);

public class FetchCustomersRequestDtoValidator : AbstractValidator<FetchCustomersRequestDto>
{
    [Obsolete]
    public FetchCustomersRequestDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;


       // PageNumber: Must be number, not less than 1.

        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);

        // PageSize: Must be number, not less than 5 & not bigger than 50

        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(5)
                                .LessThanOrEqualTo(50);

    }
  
}