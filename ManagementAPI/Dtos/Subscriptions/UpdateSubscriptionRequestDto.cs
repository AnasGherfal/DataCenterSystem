using FluentValidation;
using Infrastructure.Constants;
using Common.Helpers;

namespace ManagementAPI.Dtos.Subscriptions
{
    public class UpdateSubscriptionRequestDto
    {
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? TotalPrice { get; set; }
    }
    public class UpdateSubscriptionRequestDtoValidator:AbstractValidator<UpdateSubscriptionRequestDto>
    {
        [Obsolete("UpdateSubscriptionRequestDto")]
        public UpdateSubscriptionRequestDtoValidator() 
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(a => a.ServiceId)
               .NotEmpty().WithMessage("service id must be not empty");
            RuleFor(a => a.CustomerId)
                .NotEmpty().WithMessage("customer id must be not empty");
            //REVIEW: Variable is being treated like a string - instead confirm its a date first
            RuleFor(a => a.StartDate)
                .NotEmpty().WithMessage("startdate must be not empty")
                .Must(BeAValidDate).WithMessage("not valid datetime");
            //REVIEW: Variable is being treated like a string - instead confirm its a date first
            RuleFor(a => a.EndDate)
                .NotEmpty().WithMessage("end date must be not empty")
                .GreaterThan(a => a.StartDate).WithMessage("End date must be grater than start date")
                .Must(BeAValidDate).WithMessage("not valid datetime"); 

        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }
    }
}
