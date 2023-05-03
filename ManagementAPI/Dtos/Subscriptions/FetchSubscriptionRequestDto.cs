using FluentValidation;

namespace ManagementAPI.Dtos.Subscriptions
{
    public class FetchSubscriptionRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }

    public class FetchSubscriptionRequestDtoValidator : AbstractValidator<FetchSubscriptionRequestDto>
    {
        public FetchSubscriptionRequestDtoValidator()
        {
            CascadeMode = CascadeMode.Stop;


            RuleFor(a => a.PageNumber).GreaterThan(0).WithMessage("enter number graeter 0");
            RuleFor(a => a.PageSize).GreaterThan(0).LessThan(51).WithMessage("number of page size must be between 1 to 50");
        }
    }

}
