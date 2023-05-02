using FluentValidation;
using ManagementAPI.Dtos.Customer;

namespace ManagementAPI.Dtos.Representive;

public class FetchRepresentiveRequestDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 25;

}

public class FetchRepresentiveRequestDtoValidator : AbstractValidator<FetchRepresentiveRequestDto>
{
    public FetchRepresentiveRequestDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;

        
        // PageNumber: Must be number, not less than 1.

        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);

        // PageSize: Must be number, not less than 5 & not bigger than 50

        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(5)
                                .LessThanOrEqualTo(50);

    }

}
