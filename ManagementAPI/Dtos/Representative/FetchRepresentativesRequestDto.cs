﻿using FluentValidation;
using ManagementAPI.Dtos.Customer;

namespace ManagementAPI.Dtos.Representative;

public record FetchRepresentativeRequestDto(int PageNumber=1, int PageSize=10);

public class FetchRepresentativeRequestDtoValidator : AbstractValidator<FetchRepresentativeRequestDto>
{
    [Obsolete]
    public FetchRepresentativeRequestDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;

        
        // PageNumber: Must be number, not less than 1.

        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);

        // PageSize: Must be number, not less than 5 & not bigger than 50

        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(5)
                                .LessThanOrEqualTo(50);

    }

}
