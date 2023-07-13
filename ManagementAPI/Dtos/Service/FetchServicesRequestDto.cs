﻿using FluentValidation;

namespace ManagementAPI.Dtos.Service;

public class FetchServicesRequestDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class FetchServicesRequestDtoValidator : AbstractValidator<FetchServicesRequestDto>
{
    [Obsolete("FetchServicesRequest")]
    public FetchServicesRequestDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;


        RuleFor(a => a.PageNumber).GreaterThan(0).WithMessage("enter number graeter 0");
        RuleFor(a => a.PageSize).GreaterThan(0).LessThanOrEqualTo(50).WithMessage("number of page size must be between 1 to 50");


    }


}