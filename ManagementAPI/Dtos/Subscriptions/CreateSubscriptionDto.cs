using FluentValidation;

namespace ManagementAPI.Dtos.Subscriptions;

public class CreateSubscriptionDto
{
    public int ServiceId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid?  SubscriptionFileId { get; set; }

    public decimal? TotalPrice { get; set; }
}
public class CreateSubscriptionDtoValidator : AbstractValidator<CreateSubscriptionDto>
{
    public CreateSubscriptionDtoValidator() {
        CascadeMode = CascadeMode.Stop;

        RuleFor(a=>a.ServiceId).NotEmpty().WithMessage("service id must be not empty")
            .GreaterThan(0).WithMessage("id must be greater than 0");

        RuleFor(a => a.CustomerId).NotEmpty().WithMessage("customer id must be not empty")
            .GreaterThan(0).WithMessage("id must be greater than 0");

        RuleFor(a => a.StartDate).NotEmpty().WithMessage("startdate must be not empty");

        RuleFor(a => a.EndDate).NotEmpty().WithMessage("end date must be not empty")
            .GreaterThan(a=>a.StartDate).WithMessage("End date must be grater than start date");









    }
}
