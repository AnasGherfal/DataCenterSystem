using FluentValidation;

namespace ManagementAPI.Dtos.Subscriptions
{
    public class RenewSubscriptionDto
    {
        public DateTime RenewEndDate { get; set; }
    }
    public class RenewSubscriptionDtoValidator : AbstractValidator<RenewSubscriptionDto>
    {
        [Obsolete]
        public RenewSubscriptionDtoValidator() {
            CascadeMode = CascadeMode.Stop;
            //var minimumExpiration = DateTime.UtcNow.AddYears(1);
            RuleFor(a => a.RenewEndDate).NotEmpty().WithMessage("Renew end date must be not empty");
        
        }
    }
}
